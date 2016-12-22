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
    public class OperacaoLogTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IOperacaoLog operacoesFabrica;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("OPERAÇÃO LOG"), TestMethod]
        public void DeveConsultarOperacaoLog()
        {            
            LogOperacao logOperacao = new LogOperacao();
            logOperacao.dtLogOperacao = Convert.ToDateTime("2013-07-17");
            logOperacao.nomeFuncionalidade = "Limite Diário";
            logOperacao.acao = "Inclusão";            

            operacoesFabrica = new OperacaoLog(_repositorioModeloQC, _repositorioModeloWM_DB);
            List<LogOperacao> lstRet = operacoesFabrica.SelecionarRegistro(logOperacao);

            Assert.IsTrue(lstRet.Count > 0);
        }

        [TestCategory("OPERAÇÃO LOG"), TestMethod]
        public void DeveConsultarOperacaoLog_REGISTRO_INEXISTENTE()
        {
            LogOperacao logOperacao = new LogOperacao();
            logOperacao.dtLogOperacao = Convert.ToDateTime("2050-07-17");
            logOperacao.nomeFuncionalidade = "Limite Diário";
            logOperacao.acao = "Inclusão";

            operacoesFabrica = new OperacaoLog(_repositorioModeloQC, _repositorioModeloWM_DB);
            List<LogOperacao> lstRet = operacoesFabrica.SelecionarRegistro(logOperacao);

            Assert.IsTrue(lstRet.Count == 0);
        }

        [TestCategory("OPERAÇÃO LOG"), TestMethod]
        public void DeveGravarOperacaoLogMOCK()
        {
            operacoesFabrica = new OperacaoLog(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);            

            Assert.AreEqual(0, operacoesFabrica.Gravar(new List<LogOperacao>()));
        }
    }
}