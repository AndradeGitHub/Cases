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
    public class LimiteExcecaoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IOperacao<LimitePerfilRisco> operacoesFabrica;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveGravarLimiteExcecaoMOCK()
        {
            operacoesFabrica = new LimiteExcecao(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);

            Assert.AreEqual(0, operacoesFabrica.Gravar(PopularEntidadeGravar()));
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveAlterarLimiteExcecaoMOCK()
        {
            operacoesFabrica = new LimiteExcecao(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);

            Assert.AreEqual(0, operacoesFabrica.Alterar(PopularEntidadeAlterar()));
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveApagarLimiteExcecaoMOCK()
        {
            operacoesFabrica = new LimiteExcecao(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);

            Assert.AreEqual(0, operacoesFabrica.Apagar(PopularEntidadeApagar()));
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveGravarLimiteExcecao()
        {
            operacoesFabrica = new LimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.AreEqual(PopularEntidadeGravar().Count(), operacoesFabrica.Gravar(PopularEntidadeGravar()));
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void NAO_DeveGravarLimiteExcecao_REGISTRO_EXISTENTE()
        {
            operacoesFabrica = new LimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            try
            {
                Assert.AreEqual(PopularEntidadeGravar().Count(), operacoesFabrica.Gravar(PopularEntidadeGravar()));
            }
            catch (Exception ex) { string erro = ex.InnerException.InnerException.ToString(); }
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveAlterarLimiteExcecao()
        {
            operacoesFabrica = new LimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.AreEqual(PopularEntidadeAlterar().Count(), operacoesFabrica.Alterar(PopularEntidadeAlterar()));
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void NAO_DeveAlterarLimiteExcecao_CHAVE_EXISTENTE()
        {
            List<LimitePerfilRisco> lstLimite = PopularEntidadeAlterar();
            lstLimite[0].codPerfilRisco = "EQUITY";

            operacoesFabrica = new LimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            try
            {
                Assert.AreEqual(PopularEntidadeAlterar().Count(), operacoesFabrica.Alterar(lstLimite));
            }
            catch (Exception ex) { string erro = ex.InnerException.InnerException.ToString(); }
        }

        [TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        public void DeveApagarLimiteExcecao()
        {
            operacoesFabrica = new LimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.AreEqual(PopularEntidadeApagar().Count(), operacoesFabrica.Apagar(PopularEntidadeApagar()));
        }

        //[TestCategory("LIMITE EXCEÇÃO"), TestMethod]
        //public void DeveSelecionarRegistroLimiteExcecao()
        //{
        //    LimitePerfilRisco limitePerfilRisco = new LimitePerfilRisco();
        //    limitePerfilRisco.dtIniVigencia = DateTime.Parse("2013-01-01 00:00:00");
        //    limitePerfilRisco.dtFimVigencia = DateTime.Parse("2013-12-30 00:00:00");
        //    limitePerfilRisco.inExcecao = "S";
        //    limitePerfilRisco.inDiarioMensal = "D";

        //    operacoesFabrica = new LimiteExcecao(_repositorioModeloQC, _repositorioModeloWM_DB);

        //    var lstLimitePerfilRisco = operacoesFabrica.SelecionarRegistro(limitePerfilRisco);

        //    //Assert.IsTrue(lstLimitePerfilRisco.Count() > 0);
        //}

        #region Entidades
        private List<LimitePerfilRisco> PopularEntidadeGravar()
        {
            LimitePerfilRisco limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco.codLimitePerfilRisco = 5;
            limitePerfilRisco.codPerfilRisco = "AGGRESSI";
            limitePerfilRisco.dtIniVigencia = DateTime.Parse("2013-08-09 00:00:00");
            limitePerfilRisco.dtFimVigencia = DateTime.Parse("2013-08-25 00:00:00");
            limitePerfilRisco.vlrLimiteMin = 2;
            //limitePerfilRisco.vlrLimiteMax = ;
            limitePerfilRisco.codTipoFiltro = 1;
            limitePerfilRisco.codSubTipoFiltro = 1;
            limitePerfilRisco.dtAlteracao = DateTime.Now;
            //limitePerfilRisco.codUsuarioAlteracao = 1;    
            limitePerfilRisco.inExcecao = "S";
            limitePerfilRisco.inDiarioMensal = "D";

            LimitePerfilRisco limitePerfilRisco1 = new LimitePerfilRisco();
            limitePerfilRisco1.codLimitePerfilRisco = 6;
            limitePerfilRisco1.codPerfilRisco = "EQUITY";
            limitePerfilRisco1.dtIniVigencia = DateTime.Parse("2013-08-09 00:00:00");
            limitePerfilRisco1.dtFimVigencia = DateTime.Parse("2013-08-25 00:00:00");
            limitePerfilRisco1.vlrLimiteMin = 2;
            //limitePerfilRisco.vlrLimiteMax = ;
            limitePerfilRisco1.codTipoFiltro = 1;
            limitePerfilRisco1.codSubTipoFiltro = 1;
            limitePerfilRisco1.dtAlteracao = DateTime.Now;
            //limitePerfilRisco.codUsuarioAlteracao = 1; 
            limitePerfilRisco1.inExcecao = "S";
            limitePerfilRisco1.inDiarioMensal = "D";

            List<LimitePerfilRisco> lstLimitePerfilRisco = new List<LimitePerfilRisco>();
            lstLimitePerfilRisco.Add(limitePerfilRisco);
            lstLimitePerfilRisco.Add(limitePerfilRisco1);

            return lstLimitePerfilRisco;
        }

        private List<LimitePerfilRisco> PopularEntidadeAlterar()
        {
            List<LimitePerfilRisco> lstLimitePerfilRisco = PopularEntidadeGravar();
            lstLimitePerfilRisco[0].vlrLimiteMax = 10;
            lstLimitePerfilRisco[0].codUsuarioAlteracao = 1;
            lstLimitePerfilRisco[1].vlrLimiteMax = 15;
            lstLimitePerfilRisco[1].codUsuarioAlteracao = 1;

            return lstLimitePerfilRisco;
        }

        private List<LimitePerfilRisco> PopularEntidadeApagar()
        {
            LimitePerfilRisco limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco.codLimitePerfilRisco = 5;

            LimitePerfilRisco limitePerfilRisco1 = new LimitePerfilRisco();
            limitePerfilRisco1.codLimitePerfilRisco = 6;

            List<LimitePerfilRisco> lstLimitePerfilRisco = new List<LimitePerfilRisco>();
            lstLimitePerfilRisco.Add(limitePerfilRisco);
            lstLimitePerfilRisco.Add(limitePerfilRisco1);

            return lstLimitePerfilRisco;
        }
        #endregion Entidades
    }
}
