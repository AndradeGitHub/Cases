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
    public class RepositorioProcessamentoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IRepositorio<Processamento> repositorioFabrica;

        private List<Processamento> lstProcessamento;
        private Processamento processamento;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("PROCESSAMENTO"), TestMethod]
        public void DeveGravarProcessamento()
        {
            lstProcessamento = new List<Processamento>();

            processamento = new Processamento();
            processamento.codUsuario = 1;
            processamento.dtResultado = DateTime.Now;
            processamento.codCarteira = null;
            processamento.inTipoExecucao = "M";

            lstProcessamento.Add(processamento);

            repositorioFabrica = new RepositorioProcessamento(_repositorioModeloQC, _repositorioModeloWM_DB);

            int ret = repositorioFabrica.Gravar(lstProcessamento);

            Assert.IsTrue(ret > 0);            
        }
    }
}
