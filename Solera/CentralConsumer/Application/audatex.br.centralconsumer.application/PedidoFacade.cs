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
    public class PedidoFacade
    {        
        private static dynamic _mapeadorDomain;
        private static dynamic _repositoryPedidoStatus;
        private static dynamic _repositorySeguradora;
        private static dynamic _repositoryQueue;

        private static IUnitOfWork _unitOfWork;
        private static IDictionary<string, string> _rabbitMQConn;

        public PedidoFacade(IDictionary<string, string> rabbitMQConn)
        {             
            _rabbitMQConn = rabbitMQConn;
            _unitOfWork = new UnitOfWork();

            _mapeadorDomain = DomainFactory.CreateDomain<Mapeador>();
            _repositoryPedidoStatus = RepositoryFactory.CreateRepository<PedidoStatusModel, PedidoStatusRepository>(_unitOfWork);
            _repositorySeguradora = RepositoryFactory.CreateRepository<SeguradoraModel, SeguradoraRepository>(_unitOfWork);
            _repositoryQueue = RepositoryFactory.CreateRepository<QueueModel, QueueRepository>(_unitOfWork);           
        }

        public void EncaminhaPedidoACentral()
        {
            try
            {
                QueueModel queueModel = new QueueModel();
                queueModel.Operacao = (int)EnumQueueOperacao.Pedido;

                var lstQueue = _repositoryQueue.GetQueue(queueModel);

                var lstPedido = ConsumerQueue(lstQueue);

                foreach (var pedido in lstPedido)
                {
                    //Convertendo string para objeto
                    var PedidoDeserializado = Converter.stringToJson(pedido);
                    var PedidoPendenteSerializado = _mapeadorDomain.MapearPpecEnviado01(PedidoDeserializado);                

                    XDocument ObjXML = Converter.ObjToXML(PedidoPendenteSerializado, "PPECENVIADO01");

                    string PPECRETORNO01 = ConsumerComponent(ObjXML);

                    //Erro Timeout / Exception
                    if (PPECRETORNO01.Equals("3") || PPECRETORNO01.Equals("4"))                
                        GravaErroRetorno(PedidoPendenteSerializado);               
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

        private void GravaRetorno(dynamic PedidoDeserializado, string PPECRETORNO01)
        {
            var jsonPPECRETORNO01 = Converter.XmlToJson(PPECRETORNO01);

            //ConfRetorno01 enviando para base auxiliar
            PedidoStatusModel PedidoStatusModel = new PedidoStatusModel();
            PedidoStatusModel.CnpjSeguradora = PedidoDeserializado.CHAMADA.CNPJ;
            PedidoStatusModel.CnpjFornecedor = jsonPPECRETORNO01.PPECRETORNO01.OFICINAS.OFICINA.CNPJOFI;
            PedidoStatusModel.CnpjOficina = jsonPPECRETORNO01.PPECRETORNO01.FORNECEDORES.FORNECEDOR.CNPJFOR;
            PedidoStatusModel.IdPedido = jsonPPECRETORNO01.PPECRETORNO01.PEDIDOS.PEDIDO.IDPEDIDO;
            PedidoStatusModel.TipoOperacao = (int)EnumTipoOperacaoModel.PecRetorno01;
            PedidoStatusModel.StatusOperacao = (int)EnumStatusOperacaoModel.Disponivel;
            PedidoStatusModel.Operacao = jsonPPECRETORNO01.PPECRETORNO01.PEDIDOS.PEDIDO.OPERACAO;

            _repositoryPedidoStatus.Add(PedidoStatusModel);

            //Atualizando dados da seguradora
            var SeguradoraCadastrada = _repositorySeguradora.GetSeguradora(PedidoDeserializado.CHAMADA.CNPJ.ToString());
            SeguradoraCadastrada.CNPJ = PedidoDeserializado.CHAMADA.CNPJ.ToString();
            SeguradoraCadastrada.Chamador = PedidoDeserializado.CHAMADA.CHAMADOR.ToString();
            SeguradoraCadastrada.Perfil = PedidoDeserializado.CHAMADA.PERFIL.ToString();
            SeguradoraCadastrada.Senha = PedidoDeserializado.CHAMADA.SENHA.ToString();
            SeguradoraCadastrada.Serie = PedidoDeserializado.CHAMADA.SERIE.ToString();
            SeguradoraCadastrada.HD = PedidoDeserializado.CHAMADA.HD.ToString();
            SeguradoraCadastrada.CPF = PedidoDeserializado.CHAMADA.CPF.ToString();
            SeguradoraCadastrada.Perito = PedidoDeserializado.CHAMADA.PERITO.ToString();
            SeguradoraCadastrada.Qtde = 1;
            SeguradoraCadastrada.CNPJDest = PedidoDeserializado.DESTINATARIO.CNPJ.ToString();
            SeguradoraCadastrada.CPFDest = PedidoDeserializado.DESTINATARIO.CPF.ToString();

            _unitOfWork.Commit();
        }

        private void GravaErroRetorno(dynamic PedidoPendenteSerializado)
        {
            PedidoStatusModel PedidoStatusModelErro = new PedidoStatusModel();
            PedidoStatusModelErro.CnpjSeguradora = PedidoPendenteSerializado.PpecEnviado01.PEDIDOS.PEDIDO.CNPJ;
            PedidoStatusModelErro.CnpjFornecedor = PedidoPendenteSerializado.PpecEnviado01.FORNECEDORES.FORNECEDOR.IDPEDIDO;
            PedidoStatusModelErro.CnpjOficina = PedidoPendenteSerializado.PpecEnviado01.OFICINAS.OFICINA.CGC;
            PedidoStatusModelErro.IdPedido = PedidoPendenteSerializado.PpecEnviado01.PEDIDOS.PEDIDO.IDPEDIDO;
            PedidoStatusModelErro.TipoOperacao = (int)EnumTipoOperacaoModel.PecRetorno01;
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