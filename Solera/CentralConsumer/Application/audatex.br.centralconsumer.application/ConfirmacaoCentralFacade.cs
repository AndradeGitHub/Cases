using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using audatex.br.centralconsumer.infrastructure.log;
using audatex.br.centralconsumer.infrastructure.invoke;
using audatex.br.centralconsumer.infrastructure.common;

using audatex.br.centralconsumer.domain;
using audatex.br.centralconsumer.domain.model;
using audatex.br.centralconsumer.domain.Map;
using audatex.br.centralconsumer.infrastructure.persistence;
using audatex.br.centralconsumer.infrastructure.persistence.interfaces;
using audatex.br.centralconsumer.domain.repository;

namespace audatex.br.centralconsumer.application
{
    public class ConfirmacaoCentralFacade
    {        
        private static dynamic _mapeadorDomain;
        private static dynamic _repositoryPedidoStatus;
        private static dynamic _repositorySeguradora;
        private static dynamic _repositoryQueue;

        private static IUnitOfWork _unitOfWork;
        private static IDictionary<string, string> _rabbitMQConn;

        public ConfirmacaoCentralFacade(IDictionary<string, string> rabbitMQConn)
        {             
            _rabbitMQConn = rabbitMQConn;
            _unitOfWork = new UnitOfWork();

            _mapeadorDomain = DomainFactory.CreateDomain<Mapeador>();
            _repositoryPedidoStatus = RepositoryFactory.CreateRepository<PedidoStatusModel, PedidoStatusRepository>(_unitOfWork);
            _repositorySeguradora = RepositoryFactory.CreateRepository<SeguradoraModel, SeguradoraRepository>(_unitOfWork);
            _repositoryQueue = RepositoryFactory.CreateRepository<QueueModel, QueueRepository>(_unitOfWork);           
        }

        public void EncaminhaConfirmacaoACentral()
        {
            try
            {
                QueueModel queueModel = new QueueModel();
                queueModel.Operacao = (int)EnumQueueOperacao.Confirmacao;

                var lstQueue = _repositoryQueue.GetQueue(queueModel);

                var lstConfirmacao = ConsumerQueue(lstQueue);

                foreach (var Confirmacao in lstConfirmacao)
                {
                    //Convertendo string para objeto
                    var PedidoDeserializado = Converter.stringToJson(Confirmacao);
                    var PedidoRecebidoSerializado = _mapeadorDomain.MapearConfPedidos01(PedidoDeserializado);

                    XDocument ObjXML = Converter.ObjToXML(PedidoRecebidoSerializado, "CONFPEDIDOS01");

                    string PPECRETORNO01 = ConsumerComponent(ObjXML);

                    //Erro Timeout / Exception
                    if (PPECRETORNO01.Equals("3") || PPECRETORNO01.Equals("4"))
                        GravaErroRetorno(PedidoRecebidoSerializado);
                    else
                        GravaRetorno(PedidoDeserializado, PPECRETORNO01);
                }
            }
            catch (Exception ex)
            {
                Log.RecordInfo(string.Empty);
                Log.RecordError(ex);
            }
        }

        private void GravaRetorno(dynamic PedidoDeserializado, string CONFRETORNO01)
        {
            var jsonCONFRETORNO01 = Converter.XmlToJson(CONFRETORNO01);

            PedidoStatusModel PedidoStatusModel = new PedidoStatusModel();
            PedidoStatusModel.CnpjSeguradora = PedidoDeserializado.CHAMADA.CNPJ.ToString();
            PedidoStatusModel.IdPedido = jsonCONFRETORNO01.CONFRETORNO01.PEDIDOS.PEDIDO.IDPEDIDO.ToString();
            PedidoStatusModel.StatusOperacao = (int)EnumStatusOperacaoModel.Disponivel;
            PedidoStatusModel.TipoOperacao = (int)EnumTipoOperacaoModel.ConfRetorno01;
            PedidoStatusModel.Operacao = jsonCONFRETORNO01.CONFRETORNO01.PEDIDOS.PEDIDO.OPERACAO;
            _repositoryPedidoStatus.Add(PedidoStatusModel);

            _unitOfWork.Commit();
        }

        private void GravaErroRetorno(dynamic PedidoRecebidoSerializado)
        {              
            PedidoStatusModel PedidoStatusModelErro = new PedidoStatusModel();
            PedidoStatusModelErro.CnpjSeguradora = PedidoRecebidoSerializado.PpecEnviado01.PEDIDOS.PEDIDO.CNPJ;
            PedidoStatusModelErro.CnpjFornecedor = PedidoRecebidoSerializado.PpecEnviado01.FORNECEDORES.FORNECEDOR.IDPEDIDO;
            PedidoStatusModelErro.CnpjOficina = PedidoRecebidoSerializado.PpecEnviado01.OFICINAS.OFICINA.CGC;
            PedidoStatusModelErro.IdPedido = PedidoRecebidoSerializado.PpecEnviado01.PEDIDOS.PEDIDO.IDPEDIDO;
            PedidoStatusModelErro.TipoOperacao = (int)EnumTipoOperacaoModel.ConfRetorno01;
            PedidoStatusModelErro.StatusOperacao = (int)EnumStatusOperacaoModel.Erro;

            _repositoryPedidoStatus.Add(PedidoStatusModelErro);

            _unitOfWork.Commit();
        }       

        private List<object> ConsumerQueue(List<QueueModel> lstQueue)
        {
            var queueInvoke = new QueueInvoke(_rabbitMQConn);
            var lstMessage = queueInvoke.BasicConsumerListener(lstQueue);

            return lstMessage;
        }

        private string ConsumerComponent(XDocument xml)
        {
            var componentInvoke = new ComponentInvoke();
            var retCentral = componentInvoke.Processar(xml);

            return retCentral;
        }
    }
}