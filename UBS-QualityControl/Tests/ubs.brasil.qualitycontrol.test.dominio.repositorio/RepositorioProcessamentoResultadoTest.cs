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
    public class RepositorioProcessamentoResultadoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IRepositorio<Processamento> repositorioFabrica;

        private Processamento processamento;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("PROCESSAMENTO RESULTADO"), TestMethod]
        public void DeveConsultarProcessamentoResultadoInicial()
        {
            processamento = new Processamento();
            processamento.dtResultadoPesq = "22/04/2013";

            repositorioFabrica = new RepositorioProcessamentoResultado(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<Processamento> lstProc = repositorioFabrica.SelecionarRegistro(processamento);

            Assert.IsTrue(lstProc.Count() > 0);
        }

        [TestCategory("PROCESSAMENTO RESULTADO"), TestMethod]
        public void DeveConsultarProcessamentoResultadoDetalhe()
        {
            processamento = new Processamento();
            processamento.dtResultado = Convert.ToDateTime("2013-09-30");
            processamento.codSubTipoFiltro = 16;

            repositorioFabrica = new RepositorioProcessamentoResultado(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<Processamento> lstProc = repositorioFabrica.SelecionarRegistroDetalhe(processamento);

            Assert.IsTrue(lstProc.Count() > 0);
        }

        [TestCategory("PROCESSAMENTO RESULTADO"), TestMethod]
        public void DeveConsultarProcessamentoResultadoDetalheItem()
        {
            processamento = new Processamento();
            processamento.dtResultado = Convert.ToDateTime("2013-07-31");
            processamento.codSubTipoFiltro = 7;
            processamento.codAtivo = "BBDC4";
            processamento.codTipoAtivo = "RV";

            repositorioFabrica = new RepositorioProcessamentoResultado(_repositorioModeloQC, _repositorioModeloWM_DB);

            List<Processamento> lstProc = repositorioFabrica.SelecionarRegistroDetalheItem(processamento);

            Assert.IsTrue(lstProc.Count() > 0);
        }

        [TestCategory("PROCESSAMENTO RESULTADO"), TestMethod]
        public void DeveLiberarProcessamentoResultado()
        {
            List<Processamento> lstProc = new List<Processamento>();

            processamento = new Processamento();
            processamento.codSubTipoFiltro = 7;
            processamento.codAtivo = "BBDC3";
            processamento.codTipoAtivo = "RV";
            processamento.dtResultado = Convert.ToDateTime("2013-05-09");
            processamento.inLiberado = "S";

            lstProc.Add(processamento);

            repositorioFabrica = new RepositorioProcessamentoResultado(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.IsTrue(repositorioFabrica.Alterar(lstProc) > 0);
        }
    }
}
