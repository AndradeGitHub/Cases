using audatex.br.centralconsumer.application;
using audatex.br.centralconsumer.infrastructure.log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace audatex.br.centralconsumer.ws
{
    partial class CentralConsumer : ServiceBase
    {
        private static readonly string hostName = ConfigurationManager.AppSettings["HostName"];
        private static readonly string virtualHost = ConfigurationManager.AppSettings["VirtualHost"];
        private static readonly string userName = ConfigurationManager.AppSettings["UserName"];
        private static readonly string password = ConfigurationManager.AppSettings["Password"];        

        private Dictionary<string, string> _rabbitMQConn;
        private static System.Timers.Timer _aTimer;

        public CentralConsumer()
        {
            Log.RecordInfo("# INÍCIO - RECUPERANDO CONFIGURAÇÕES DE FILA");
            _rabbitMQConn = new Dictionary<string, string>();
            _rabbitMQConn.Add("HostName", hostName);
            _rabbitMQConn.Add("UserName", userName);
            _rabbitMQConn.Add("Password", password);
            _rabbitMQConn.Add("VirtualHost", virtualHost);
            Log.RecordInfo("# FIM - RECUPERANDO CONFIGURAÇÕES DE FILA");

            //_aTimer = new System.Timers.Timer(Convert.ToInt32(ConfigurationManager.AppSettings["Intervalo"]));
            //_aTimer.Enabled = true;
            //_aTimer.Elapsed += new System.Timers.ElapsedEventHandler(EnvioPedido);

            EnvioPedido(null, null);

            InitializeComponent();
        }
        public void Start()
        {
            // _aTimer.Start();
        }
        //Custom method to Stop the timer
        public new void Stop()
        {
            // _aTimer.Stop();
        }
        protected override void OnStart(string[] args)
        {
            Log.RecordInfo("# INICIO DO PROCESSAMENTO");
            Log.RecordInfo("****************************************");
            Log.RecordInfo("****************************************");
            Log.RecordInfo(string.Empty);

            this.Start();
        }
        protected override void OnStop()
        {
            Log.RecordInfo(string.Empty);
            Log.RecordInfo("****************************************");
            Log.RecordInfo("****************************************");
            Log.RecordInfo("# Fim DO PROCESSAMENTO");

            this.Stop();
        }
        void EnvioPedido(object sender, System.Timers.ElapsedEventArgs e)
        {
            Log.RecordInfo("# INÍCIO - ENCAMINHANDO PEDIDO A CENTRAL");
            PedidoFacade pedidoFacade = new PedidoFacade(_rabbitMQConn);
            pedidoFacade.EncaminhaPedidoACentral();
            Log.RecordInfo(string.Empty);
            Log.RecordInfo("# FIM - ENCAMINHANDO PEDIDO A CENTRAL");

            Log.RecordInfo("# INÍCIO - CONFIRMANDO PEDIDO A CENTRAL");
            ConfirmacaoCentralFacade confirmacaoCentralFacade = new ConfirmacaoCentralFacade(_rabbitMQConn);
            confirmacaoCentralFacade.EncaminhaConfirmacaoACentral();
            Log.RecordInfo(string.Empty);
            Log.RecordInfo("# FIM - CONFIRMANDO PEDIDO A CENTRAL");

            Log.RecordInfo("# INÍCIO - CONSUMINDO MENSAGEM");
            ConfirmacaoSeguradoraFacade confirmacaoSeguradoraFacade = new ConfirmacaoSeguradoraFacade(_rabbitMQConn);
            confirmacaoSeguradoraFacade.EnviarConfirmacaoDePedidosSeguradora();
            Log.RecordInfo(string.Empty);
            Log.RecordInfo("# FIM - CONSUMINDO MENSAGEM");
        }
    }
}
