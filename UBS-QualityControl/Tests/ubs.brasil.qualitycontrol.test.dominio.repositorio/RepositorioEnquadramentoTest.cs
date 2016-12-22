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
    public class RepositorioEnquadramentoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IRepositorio<Enquadramento> repositorioFabrica;

        private List<Enquadramento> lstEnquadramento;
        private Enquadramento enquadramento;                

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("ENQUADRAMENTO"), TestMethod]
        public void DeveLiberarEnquadramento()
        {
            lstEnquadramento = new List<Enquadramento>();

            enquadramento = new Enquadramento();
            enquadramento.codUsuario = 1;
            enquadramento.codProcessamento = 1;
            enquadramento.codCarteira = "";
            enquadramento.codListaSubTipo = "1";

            lstEnquadramento.Add(enquadramento);

            repositorioFabrica = new RepositorioEnquadramento(_repositorioModeloQC, _repositorioModeloWM_DB);

            int ret = repositorioFabrica.Alterar(lstEnquadramento);

            Assert.IsTrue(ret > 0);
        }

        [TestCategory("ENQUADRAMENTO"), TestMethod]
        public void DeveConsultarEnquadramentoDiarioMensal()
        {
            enquadramento = new Enquadramento();
            enquadramento.dtResultado = Convert.ToDateTime("2013-09-30");
            enquadramento.inDiarioMensal = "DIARIO";

            repositorioFabrica = new RepositorioEnquadramento(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<Enquadramento> lstProc = repositorioFabrica.SelecionarRegistro(enquadramento);

            Assert.IsTrue(lstProc.Count() > 0);
        }

        [TestCategory("ENQUADRAMENTO"), TestMethod]
        public void DeveConsultarEnquadramentoMensal()
        {
            enquadramento = new Enquadramento();
            enquadramento.dtResultado = Convert.ToDateTime("2013-09-30");
            enquadramento.inDiarioMensal = "MENSAL";

            repositorioFabrica = new RepositorioEnquadramento(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<Enquadramento> lstProc = repositorioFabrica.SelecionarRegistro(enquadramento);

            Assert.IsTrue(lstProc.Count() > 0);
        }

        [TestCategory("ENQUADRAMENTO"), TestMethod]
        public void DeveConsultarEnquadramentoDetalhe()
        {
            enquadramento = new Enquadramento();
            enquadramento.codCarteira = "01244-0";
            enquadramento.codProcessamento = 30;

            repositorioFabrica = new RepositorioEnquadramento(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<Enquadramento> lstProc = repositorioFabrica.SelecionarRegistroDetalhe(enquadramento);

            Assert.IsTrue(lstProc.Count() > 0);
        }
    }
}