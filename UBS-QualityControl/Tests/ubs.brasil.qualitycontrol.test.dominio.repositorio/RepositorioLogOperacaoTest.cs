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
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.test.dominio.repositorio
{
    [TestClass]
    public class RepositorioLogOperacaoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IRepositorio<LogOperacao> repositorioFabrica;

        private LogOperacao logOperacao;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("LOG OPERAÇÃO"), TestMethod]
        public void DeveConsultarLogOperacao()
        {
            logOperacao = new LogOperacao();
            logOperacao.dtLogOperacao = Convert.ToDateTime("2013-09-03");            
            logOperacao.nomeFuncionalidade = "Limite Diário";
            logOperacao.acao = "Inclusão";

            repositorioFabrica = new RepositorioLogOperacao(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<LogOperacao> lstProc = repositorioFabrica.SelecionarRegistro(logOperacao);

            Assert.IsTrue(lstProc.Count() > 0);
        }

        [TestCategory("LOG OPERAÇÃO"), TestMethod]
        public void DeveGravarLogOperacao()
        {
            List<LogOperacao> lstLogOperacao = new List<LogOperacao>();

            LogOperacao logOperacao = new LogOperacao();
            logOperacao.dtLogOperacao = Convert.ToDateTime("2050-07-17");
            logOperacao.nomeFuncionalidade = "TESTE";
            logOperacao.acao = "TESTE";
            logOperacao.txDescricao = "TESTE";
            logOperacao.codUsuario = 0;

            lstLogOperacao.Add(logOperacao);

            repositorioFabrica = new RepositorioLogOperacao(_repositorioModeloQC, _repositorioModeloWM_DB);

            int ret = repositorioFabrica.Gravar(lstLogOperacao);

            Assert.IsTrue(ret > 0);
        }
    }
}