using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;

namespace ubs.brasil.qualitycontrol.test.dominio.core
{
    [TestClass]
    public class ProcessamentoHistoricoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IOperacao<LogProcessamento> operacoesFabrica;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        //[TestCategory("PROCESSAMENTO HISTORICO"), TestMethod]
        //public void DeveConsultarProcessamentoHistorico()
        //{
        //    List<LogProcessamento> lstProcessamento = new List<LogProcessamento>();

        //    LogProcessamento processamento = new LogProcessamento();            
        //    processamento.dtProcessada = Convert.ToDateTime("25-06-2013");            

        //    lstProcessamento.Add(processamento);

        //    operacoesFabrica = new ProcessamentoHistorico(_repositorioModeloQC, _repositorioModeloWM_DB);

        //    var lstRet = operacoesFabrica.SelecionarRegistro(processamento);

        //    Assert.IsTrue(lstRet.Count > 0);            
        //}
    }
}
