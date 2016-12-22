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
    public class EnquadramentoDiarioMensalTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IOperacao<Enquadramento> operacoesFabrica;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoDiario()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "DIARIO";
        //    enquadramento.dtResultado = Convert.ToDateTime("2013-06-25");
            
        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistro(enquadramento);

        //    Assert.IsTrue(lstRet.Count > 0);
        //}

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoMensal()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "MENSAL";
        //    enquadramento.dtResultado = Convert.ToDateTime("2013-06-25");

        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistro(enquadramento);

        //    Assert.IsTrue(lstRet.Count > 0);
        //}

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoDetalheDiario()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "DIARIO";
        //    enquadramento.dtResultado = Convert.ToDateTime("2013-06-25");

        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistroDetalhe(enquadramento);

        //    Assert.IsTrue(lstRet.Count > 0);
        //}

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoDetalheMensal()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "MENSAL";
        //    enquadramento.dtResultado = Convert.ToDateTime("2013-06-25");

        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistroDetalhe(enquadramento);

        //    Assert.IsTrue(lstRet.Count > 0);
        //}

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoDiario_REGISTRO_INEXISTENTE()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "DIARIO";
        //    enquadramento.dtResultado = Convert.ToDateTime("2050-06-25");

        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistro(enquadramento);

        //    Assert.IsTrue(lstRet.Count == 0);
        //}

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoMensal_REGISTRO_INEXISTENTE()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "MENSAL";
        //    enquadramento.dtResultado = Convert.ToDateTime("2050-06-25");

        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistro(enquadramento);

        //    Assert.IsTrue(lstRet.Count == 0);
        //}

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoDetalheDiario_REGISTRO_INEXISTENTE()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "DIARIO";
        //    enquadramento.dtResultado = Convert.ToDateTime("2050-06-25");

        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistroDetalhe(enquadramento);

        //    Assert.IsTrue(lstRet.Count == 0);
        //}

        //[TestCategory("ENQUADRAMENTO"), TestMethod]
        //public void DeveConsultarEnquadramentoDetalheMensal_REGISTRO_INEXISTENTE()
        //{
        //    Enquadramento enquadramento = new Enquadramento();
        //    enquadramento.inDiarioMensal = "MENSAL";
        //    enquadramento.dtResultado = Convert.ToDateTime("2050-06-25");

        //    operacoesFabrica = new EnquadramentoDiarioMensal(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<Enquadramento> lstRet = operacoesFabrica.SelecionarRegistroDetalhe(enquadramento);

        //    Assert.IsTrue(lstRet.Count == 0);
        //}
    }
}