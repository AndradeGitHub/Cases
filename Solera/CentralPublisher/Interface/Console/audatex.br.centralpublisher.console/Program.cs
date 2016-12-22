using System;
using System.Configuration;
using System.Collections.Generic;

using log4net;

using audatex.br.centralpublisher.application;
using audatex.br.centralpublisher.infrastructure.log;

namespace audatex.br.centralpublisher.console
{
    class Program
    {
        private static readonly string hostName = ConfigurationManager.AppSettings["HostName"];
        private static readonly string virtualHost = ConfigurationManager.AppSettings["VirtualHost"];
        private static readonly string userName = ConfigurationManager.AppSettings["UserName"];
        private static readonly string password = ConfigurationManager.AppSettings["Password"];

        static void Main(string[] args)
        {
            try
            {
                Log.RecordInfo("# INICIO DO PROCESSAMENTO");
                Log.RecordInfo("****************************************");
                Log.RecordInfo("****************************************");
                Log.RecordInfo(string.Empty);

                Log.RecordInfo("# INÍCIO - RECUPERANDO CONFIGURAÇÕES DE FILA");
                var rabbitMQConn = new Dictionary<string, string>();
                rabbitMQConn.Add("HostName", hostName);
                rabbitMQConn.Add("UserName", userName);
                rabbitMQConn.Add("Password", password);
                rabbitMQConn.Add("VirtualHost", virtualHost);
                Log.RecordInfo("# FIM - RECUPERANDO CONFIGURAÇÕES DE FILA");

                Log.RecordInfo("# INÍCIO - ENCAMINHANDO PEDIDO A AXPEDIDO");
                PedidoFacade pedidoFacade = new PedidoFacade(rabbitMQConn);
                pedidoFacade.EncaminhaPedidoAxPedido();
                Log.RecordInfo(string.Empty);
                Log.RecordInfo("# FIM - ENCAMINHANDO PEDIDO A AXPEDIDO");

                Log.RecordInfo("# INÍCIO - CONFIRMANDO PROCESSAMENTO DO PEDIDO PELA CENTRAL AO AXPEDIDO");
                PedidoRetorno pedidoRetornoFacade = new PedidoRetorno(rabbitMQConn);
                pedidoRetornoFacade.EncaminhaRetornoAxPedido();
                Log.RecordInfo(string.Empty);
                Log.RecordInfo("# FIM - CONFIRMANDO PROCESSAMENTO DO PEDIDO PELA CENTRAL AO AXPEDIDO");
            }
            catch (Exception ex)
            {
                Log.RecordInfo(string.Empty);
                Log.RecordError(ex);
            }
            finally
            {
                Log.RecordInfo(string.Empty);
                Log.RecordInfo("****************************************");
                Log.RecordInfo("****************************************");
                Log.RecordInfo("# FIM DO PROCESSAMENTO");
                Log.RecordInfo(string.Empty);
                Log.RecordInfo(string.Empty);

                Console.Read();
            }
        }
    }
}
