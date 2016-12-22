using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Xml;

using audatex.br.centralpublisher.domain;
using audatex.br.centralpublisher.infrastructure.invoke;
using audatex.br.centralpublisher.infrastructure.log;
using audatex.br.centralpublisher.infrastructure.persistence.interfaces;
using audatex.br.centralpublisher.domain.model;
using audatex.br.centralpublisher.domain.repository;
using audatex.br.centralpublisher.infrastructure.persistence;
using audatex.br.centralpublisher.domain.map;
using audatex.br.centralpublisher.infrastructure.common;

namespace audatex.br.centralpublisher.application
{
    public class PedidoRetorno
    {
        private static IUnitOfWork _unitOfWork;
        private static IDictionary<string, string> _rabbitMQConn;

        private static dynamic _mapeadorDomain;
        private static dynamic _repositorySeguradora;
        private static dynamic _repositoryPedidoStatus;
        private static dynamic _repositoryQueue;

        public PedidoRetorno(IDictionary<string, string> rabbitMQConn)
        {
            _rabbitMQConn = rabbitMQConn;
            _unitOfWork = new UnitOfWork();

            _mapeadorDomain = DomainFactory.CreateDomain<Mapeador>();
            _repositorySeguradora = RepositoryFactory.CreateRepository<SeguradoraModel, SeguradoraRepository>(_unitOfWork);
            _repositoryPedidoStatus = RepositoryFactory.CreateRepository<PedidoStatusModel, PedidoStatusRepository>(_unitOfWork);
            _repositoryQueue = RepositoryFactory.CreateRepository<QueueModel, QueueRepository>(_unitOfWork);
        }

        public void EncaminhaRetornoAxPedido()
        {
            var seguradoras = _repositorySeguradora.GetSeguradoras();            

            foreach (var seguradora in seguradoras)
            {
                //Listar filas cadastradas para a seguradora                
                var QueueConfirmacao = _repositoryQueue.GetQueuePorSeguradora(EnumQueueOperacao.Confirmacao, seguradora);

                //Consumindo itens disponíveis na tabela auxiliar e em seguida montar e gravar o objeto ConfPedidos03 na fila.
                var ListaPecRetorno01 = _repositoryPedidoStatus.GetPedidoStatusDisponivelPorOperacao(EnumTipoOperacaoModel.PecRetorno01);
                foreach (var item in ListaPecRetorno01)
                {
                    //Atualizando o status da confirmação
                    item.StatusOperacao = (int)EnumStatusOperacaoModel.Consumido;

                    //Montar objeto PecRetorno01 e enviar para a fila
                    var PPECRETORNO01 = _mapeadorDomain.Mapear_PecRetorno01(seguradora, item);
                    var jsonPecRetorno01 = Converter.ObjToJson(PPECRETORNO01);

                    PublishQueue(QueueConfirmacao, PPECRETORNO01);
                }

                _unitOfWork.Commit();
            }
        }

        private void PublishQueue(QueueModel Queue, dynamic Message)
        {
            var queueInvoke = new QueueInvoke(_rabbitMQConn);
            queueInvoke.PublishDirect(Queue.Exchange, Queue.Nome, Message);
        }
    }
}