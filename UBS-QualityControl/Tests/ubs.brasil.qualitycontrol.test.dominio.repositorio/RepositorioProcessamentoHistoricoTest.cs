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
    public class RepositorioProcessamentoHistoricoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IRepositorio<LogProcessamento> repositorioFabrica;

        private LogProcessamento processamento;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("PROCESSAMENTO HISTORICO"), TestMethod]
        public void DeveConsultarProcessamentoHistorico()
        {
            processamento = new LogProcessamento();
            processamento.dtProcessada = Convert.ToDateTime("2013-05-08");
            processamento.codCarteira = "01103-6, 01170-2";

            repositorioFabrica = new RepositorioProcessamentoHistorico(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<LogProcessamento> lstRet = repositorioFabrica.SelecionarRegistro(processamento);

            Assert.IsTrue(lstRet.Count() > 0);
        }

        [TestCategory("PROCESSAMENTO HISTORICO"), TestMethod]
        public void DeveConsultarProcessamentoHistoricoDetalhe()
        {
            processamento = new LogProcessamento();
            processamento.codProcessamento = 251;
            processamento.codCarteira = "01170-2";

            repositorioFabrica = new RepositorioProcessamentoHistorico(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<LogProcessamento> lstRet = repositorioFabrica.SelecionarRegistroDetalhe(processamento);

            Assert.IsTrue(lstRet.Count() > 0);
        }
    }
}
