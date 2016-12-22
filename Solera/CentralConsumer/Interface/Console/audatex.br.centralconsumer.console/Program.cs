using System;
using System.Configuration;
using System.Collections.Generic;

using log4net;

using audatex.br.centralconsumer.application;
using audatex.br.centralconsumer.infrastructure.log;

namespace audatex.br.centralconsumer.console
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

                Log.RecordInfo("# INÍCIO - ENCAMINHANDO PEDIDO A CENTRAL");
                PedidoFacade pedidoFacade = new PedidoFacade(rabbitMQConn);                
                pedidoFacade.EncaminhaPedidoACentral();
                Log.RecordInfo(string.Empty);
                Log.RecordInfo("# FIM - ENCAMINHANDO PEDIDO A CENTRAL");

                Log.RecordInfo("# INÍCIO - CONFIRMANDO PEDIDO A CENTRAL");
                ConfirmacaoCentralFacade confirmacaoCentralFacade = new ConfirmacaoCentralFacade(rabbitMQConn);
                confirmacaoCentralFacade.EncaminhaConfirmacaoACentral();
                Log.RecordInfo(string.Empty);
                Log.RecordInfo("# FIM - CONFIRMANDO PEDIDO A CENTRAL");

                //Log.RecordInfo("# INÍCIO - CONFIRMANDO SEGURADORA");
                //ConfirmacaoSeguradoraFacade confirmacaoSeguradoraFacade = new ConfirmacaoSeguradoraFacade(rabbitMQConn);
                //confirmacaoSeguradoraFacade.EnviarConfirmacaoDePedidosSeguradora();
                //Log.RecordInfo(string.Empty);
                //Log.RecordInfo("# FIM - CONFIRMANDO SEGURADORA");
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
