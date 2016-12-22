using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public class RepositorioLogCargaTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IRepositorio<LogCarga> repositorioFabrica;

        private List<LogCarga> lstLogCarga;
        private LogCarga logCarga;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("LOG CARGA"), TestMethod]
        public void DeveConsultarLogCargaPorOrdem()
        {
            logCarga = new LogCarga();
            logCarga.codCarga = 310;
            logCarga.codOrdem = 1;

            repositorioFabrica = new RepositorioLogCarga(_repositorioModeloQC, _repositorioModeloWM_DB);

            lstLogCarga = repositorioFabrica.SelecionarRegistro(logCarga);

            Assert.IsTrue(lstLogCarga.Count() > 0);
        }
    }
}
