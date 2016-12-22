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
    public class ConfirmacaoSeguradoraFacade
    {
        private static dynamic _mapeadorDomain;
        private static dynamic _repositoryPedidoStatus;
        private static dynamic _repositorySeguradora;
        private static dynamic _repositoryQueue;

        private static IUnitOfWork _unitOfWork;
        private static IDictionary<string, string> _rabbitMQConn;

        public ConfirmacaoSeguradoraFacade(IDictionary<string, string> rabbitMQConn)
        {
            _rabbitMQConn = rabbitMQConn;
            _unitOfWork = new UnitOfWork();

            _mapeadorDomain = DomainFactory.CreateDomain<Mapeador>();
            _repositoryPedidoStatus = RepositoryFactory.CreateRepository<PedidoStatusModel, PedidoStatusRepository>(_unitOfWork);
            _repositorySeguradora = RepositoryFactory.CreateRepository<SeguradoraModel, SeguradoraRepository>(_unitOfWork);
            _repositoryQueue = RepositoryFactory.CreateRepository<QueueModel, QueueRepository>(_unitOfWork);
        }

        public void EnviarConfirmacaoDePedidosSeguradora()
        {
            try
            {
                var EnviarConfirmacaoParaCentral = _repositoryPedidoStatus.GetConfirmacaoPedidosSeguradora();
                var cnpjseguradora = "";

                SeguradoraModel DadosSeguradora = new SeguradoraModel();


                foreach (var pedido in EnviarConfirmacaoParaCentral)
                {

                    if (cnpjseguradora != EnviarConfirmacaoParaCentral.CnpjSeguradora)
                    {
                        DadosSeguradora = _repositorySeguradora.GetSeguradora(EnviarConfirmacaoParaCentral.CnpjSeguradora);

                        cnpjseguradora = EnviarConfirmacaoParaCentral.CnpjSeguradora;
                    }

                    var PedidoPendenteSerializado = _mapeadorDomain.MapearConfPedidos03(DadosSeguradora, pedido);

                    XDocument ObjXML = Converter.StrToXML(PedidoPendenteSerializado.ToString(), "CONFPEDIDOS03");

                    string Confretorno03 = ConsumerComponent(ObjXML);

                    //Erro Timeout / Exception
                    if (Confretorno03.Equals("3") || Confretorno03.Equals("4"))
                        GravaErroRetorno(PedidoPendenteSerializado);
                    else
                        GravaRetorno(Confretorno03, cnpjseguradora);
                }
            }
            catch (Exception ex)
            {
                Log.RecordInfo(string.Empty);
                Log.RecordError(ex);
            }
        }

        private void GravaRetorno(string Confretorno03, string cnpjseguradora)
        {
            dynamic ObjJson = Converter.ObjToJson(Confretorno03);


            PedidoStatusModel PedidoStatusModel = new PedidoStatusModel();
            PedidoStatusModel.CnpjSeguradora = cnpjseguradora;
            PedidoStatusModel.IdPedido = ObjJson.CONFRETORNO03.PEDIDOS.PEDIDO.IDPEDIDO;
            PedidoStatusModel.StatusOperacao = (int)EnumStatusOperacaoModel.Disponivel;
            PedidoStatusModel.TipoOperacao = (int)EnumTipoOperacaoModel.ConfPedidos03;
            PedidoStatusModel.Operacao = ObjJson.CONFRETORNO03.PEDIDOS.PEDIDO.OPERACAO;

            _repositoryPedidoStatus.Add(PedidoStatusModel);

            _unitOfWork.Commit();
        }

        private void GravaErroRetorno(dynamic PedidoPendenteSerializado)
        {
            PedidoStatusModel PedidoStatusModelErro = new PedidoStatusModel();
            PedidoStatusModelErro.CnpjSeguradora = PedidoPendenteSerializado.PpecEnviado01.PEDIDOS.PEDIDO.CNPJ;
            PedidoStatusModelErro.CnpjFornecedor = PedidoPendenteSerializado.PpecEnviado01.FORNECEDORES.FORNECEDOR.IDPEDIDO;
            PedidoStatusModelErro.CnpjOficina = PedidoPendenteSerializado.PpecEnviado01.OFICINAS.OFICINA.CGC;
            PedidoStatusModelErro.IdPedido = PedidoPendenteSerializado.PpecEnviado01.PEDIDOS.PEDIDO.IDPEDIDO;
            PedidoStatusModelErro.TipoOperacao = (int)EnumTipoOperacaoModel.ConfPedidos03;
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