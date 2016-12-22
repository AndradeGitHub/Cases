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
    public class ProcessamentoResultadoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IOperacao<Processamento> operacoesFabrica;

        private Processamento processamento;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        //[TestCategory("PROCESSAMENTO RESULTADO"), TestMethod]
        //public void DeveConsultarProcessamentoResultadoInicial()
        //{
        //    processamento = new Processamento();

        //    operacoesFabrica = new ProcessamentoResultado(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Processamento> lstRet = operacoesFabrica.SelecionarRegistro(processamento);

        //    Assert.IsTrue(lstRet.Count > 0);
        //}

        //[TestCategory("PROCESSAMENTO RESULTADO"), TestMethod]
        //public void DeveConsultarProcessamentoResultadoDetalhe()
        //{
        //    processamento = new Processamento();
        //    processamento.codSubTipoFiltro = 7;

        //    operacoesFabrica = new ProcessamentoResultado(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Processamento> lstRet = operacoesFabrica.SelecionarRegistroDetalhe(processamento);

        //    Assert.IsTrue(lstRet.Count > 0);
        //}

        //[TestCategory("PROCESSAMENTO RESULTADO"), TestMethod]
        //public void DeveConsultarProcessamentoResultadoDetalheItem()
        //{
        //    processamento = new Processamento();
        //    processamento.codSubTipoFiltro = 7;
        //    processamento.codAtivo = "BBDC4";
        //    processamento.codTipoAtivo = "RV";

        //    operacoesFabrica = new ProcessamentoResultado(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Processamento> lstRet = operacoesFabrica.SelecionarRegistroDetalheItem(processamento);

        //    Assert.IsTrue(lstRet.Count > 0);
        //}
    }
}