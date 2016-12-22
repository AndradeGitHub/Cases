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
    public class PedidoFacade
    {
        private static IUnitOfWork _unitOfWork;
        private static IDictionary<string, string> _rabbitMQConn;

        private static dynamic _mapeadorDomain;
        private static dynamic _repositorySeguradora;
        private static dynamic _repositoryPedidoStatus;
        private static dynamic _repositoryQueue;

        public PedidoFacade(IDictionary<string, string> rabbitMQConn)
        {
            _rabbitMQConn = rabbitMQConn;
            _unitOfWork = new UnitOfWork();

            _mapeadorDomain = DomainFactory.CreateDomain<Mapeador>();
            _repositorySeguradora = RepositoryFactory.CreateRepository<SeguradoraModel, SeguradoraRepository>(_unitOfWork);
            _repositoryPedidoStatus = RepositoryFactory.CreateRepository<PedidoStatusModel, PedidoStatusRepository>(_unitOfWork);
            _repositoryQueue = RepositoryFactory.CreateRepository<QueueModel, QueueRepository>(_unitOfWork);
        }

        public void EncaminhaPedidoAxPedido()
        {
            try
            {
                XDocument xmlPedePedidos01 = new XDocument();

                //Listar seguradoras cadastradas na tabela auxiliar seguradoras
                var seguradoras = _repositorySeguradora.GetSeguradoras();                

                foreach (var seguradora in seguradoras)
                {
                    //Listar filas cadastradas para a seguradora
                    var QueuePedido = _repositoryQueue.GetQueuePorSeguradora(EnumQueueOperacao.Pedido, seguradora);
                    var QueueConfirmacao = _repositoryQueue.GetQueuePorSeguradora(EnumQueueOperacao.Confirmacao, seguradora);

                    //Montar o objeto de solicitação de pedidos orçados na central
                    var PedePedidos01 = _mapeadorDomain.Mapear_PedePedidos01(seguradora);
                    xmlPedePedidos01 = Converter.ObjToXML(PedePedidos01, "PEDEPEDIDOS01");

                    //Solicitar pedido orçado na central                                         
                    var jsonPecRecebido01 = ConsumerComponent(xmlPedePedidos01);

                    //Erro Timeout / Exception
                    if (jsonPecRecebido01.Equals("3") || jsonPecRecebido01.Equals("4"))
                        GravaErroRetorno(PedePedidos01, seguradora);
                    else
                        GravaRetorno(PedePedidos01, jsonPecRecebido01, seguradora, QueuePedido, QueueConfirmacao);
                }
            }
            catch (Exception ex)
            {
                Log.RecordInfo(string.Empty);
                Log.RecordError(ex);
            }
        }

        private void GravaRetorno(dynamic PedePedidos01, dynamic jsonPecRecebido01, dynamic seguradora, dynamic queuePedido, dynamic queueConfirmacao)
        {
            if (jsonPecRecebido01.PPECRECEBIDO01.PEDIDOS != null)
            {
                //Gravar pedido orçado na fila;
                PublishQueue(queuePedido, jsonPecRecebido01);

                //Consumindo itens disponíveis na tabela auxiliar e em seguida montar e gravar o objeto ConfPedidos03 na fila.
                var ListaConfPedidos03 = _repositoryPedidoStatus.GetPedidoStatusDisponivelPorOperacao(EnumTipoOperacaoModel.ConfPedidos03);
                foreach (var item in ListaConfPedidos03)
                {
                    //Atualizando o status da confirmação
                    item.StatusOperacao = (int)EnumStatusOperacaoModel.Consumido;

                    //Montar o objeto ConfPedidos03 e enviar para a fila
                    var ConfPedidos03 = _mapeadorDomain.Mapear_ConfPedidos03(seguradora, item);
                    var jsonConfPedidos03 = Converter.ObjToJson(ConfPedidos03);

                    PublishQueue(queueConfirmacao, jsonConfPedidos03);
                }

                //Gravar ConfRetorno03 na tabela auxiliar para o serviço CentralConsumer consumir e enviar para a central
                PedidoStatusModel pedidoStatusModel = new PedidoStatusModel();
                pedidoStatusModel.CnpjSeguradora = seguradora.CNPJ;
                pedidoStatusModel.IdPedido = jsonPecRecebido01.PPECRECEBIDO01.PEDIDOS.PEDIDO.IDPEDIDO;
                pedidoStatusModel.Numero = jsonPecRecebido01.PPECRECEBIDO01.PEDIDOS.PEDIDO.Numero;
                pedidoStatusModel.DataAbertura = jsonPecRecebido01.PPECRECEBIDO01.PEDIDOS.PEDIDO.DataAbertura;
                pedidoStatusModel.TipoOperacao = (int)EnumTipoOperacaoModel.ConfRetorno03;
                pedidoStatusModel.StatusOperacao = (int)EnumStatusOperacaoModel.Disponivel;
                pedidoStatusModel.Operacao = jsonPecRecebido01.PPECRECEBIDO01.PEDIDOS.PEDIDO.OPERACAO;

                _repositoryPedidoStatus.Add(pedidoStatusModel);

                _unitOfWork.Commit();
            }
        }

        private void GravaErroRetorno(dynamic PedePedidos01, dynamic seguradora)
        {
            PedidoStatusModel pedidoStatusModelErro = new PedidoStatusModel();
            pedidoStatusModelErro.CnpjSeguradora = seguradora.CNPJ;
            pedidoStatusModelErro.IdPedido = PedePedidos01.PPECRECEBIDO01.PEDIDOS.PEDIDO.IDPEDIDO;
            pedidoStatusModelErro.Numero = PedePedidos01.PPECRECEBIDO01.PEDIDOS.PEDIDO.Numero;
            pedidoStatusModelErro.DataAbertura = PedePedidos01.PPECRECEBIDO01.PEDIDOS.PEDIDO.DataAbertura;
            pedidoStatusModelErro.TipoOperacao = (int)EnumTipoOperacaoModel.ConfRetorno03;
            pedidoStatusModelErro.StatusOperacao = (int)EnumStatusOperacaoModel.Erro;
            pedidoStatusModelErro.Operacao = PedePedidos01.PPECRECEBIDO01.PEDIDOS.PEDIDO.OPERACAO;

            _repositoryPedidoStatus.Add(pedidoStatusModelErro);          

            _unitOfWork.Commit();
        }

        private void PublishQueue(QueueModel Queue, dynamic Message)
        {
            var queueInvoke = new QueueInvoke(_rabbitMQConn);
            queueInvoke.PublishDirect(Queue.Exchange, Queue.Nome, Message);
        }

        private string ConsumerComponent(XDocument xml)
        {
            var componentInvoke = new ComponentInvoke();
            var retCentral = componentInvoke.Processar(xml);

            return retCentral;
        }
    }
}