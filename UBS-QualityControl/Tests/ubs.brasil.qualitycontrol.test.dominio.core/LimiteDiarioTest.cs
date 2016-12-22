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
    public class LimiteDiarioTest
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

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void DeveGravarLimiteDiarioMOCK()
        {
            operacoesFabrica = new LimiteDiario(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);

            Assert.AreEqual(0, operacoesFabrica.Gravar(PopularEntidadeGravar()));
        }

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void DeveAlterarLimiteDiarioMOCK()
        {                         
            //var mock = new Mock<IOperacao<LimitePerfilRisco>>();
            //var _mock = mock.Setup(x => x.Alterar(lstLimitePerfilRisco)).Returns(2);    

            operacoesFabrica = new LimiteDiario(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);

            Assert.AreEqual(0, operacoesFabrica.Alterar(PopularEntidadeAlterar()));
        }

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void DeveApagarLimiteDiarioMOCK()
        {
            //var mock = new Mock<IOperacao<LimiteDiario>>();
            //var _mock = mock.Setup(x => x.Apagar(limiteDiario)).Returns(2);

            operacoesFabrica = new LimiteDiario(_mockRepositorioModeloQC.Object, _mockRepositorioModeloWM_DB.Object);

            Assert.AreEqual(0, operacoesFabrica.Apagar(PopularEntidadeApagar()));
        }

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void DeveGravarLimiteDiario()
        {
            operacoesFabrica = new LimiteDiario(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.AreEqual(PopularEntidadeGravar().Count(), operacoesFabrica.Gravar(PopularEntidadeGravar()));
        }

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void NAO_DeveGravarLimiteDiario_REGISTRO_EXISTENTE()
        {
            operacoesFabrica = new LimiteDiario(_repositorioModeloQC, _repositorioModeloWM_DB);

            try
            {
                Assert.AreEqual(PopularEntidadeGravar().Count(), operacoesFabrica.Gravar(PopularEntidadeGravar()));                
            }
            catch (Exception ex) { string erro = ex.InnerException.InnerException.ToString(); }
        }

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void DeveAlterarLimiteDiario()
        {
            //var mock = new Mock<IOperacao<TipoFiltro>>();
            //var _mock = mock.Setup(x => x.(tipoFiltro)).Returns(2);                         

            operacoesFabrica = new LimiteDiario(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.AreEqual(PopularEntidadeAlterar().Count(), operacoesFabrica.Alterar(PopularEntidadeAlterar()));
        }

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void NAO_DeveAlterarLimiteDiario_CHAVE_EXISTENTE()
        {
            List<LimitePerfilRisco> lstLimite = PopularEntidadeAlterar();
            lstLimite[0].codPerfilRisco = "EQUITY";

            operacoesFabrica = new LimiteDiario(_repositorioModeloQC, _repositorioModeloWM_DB);

            try
            {
                Assert.AreEqual(PopularEntidadeAlterar().Count(), operacoesFabrica.Alterar(lstLimite));
            }
            catch (Exception ex) { string erro = ex.InnerException.InnerException.ToString(); }
        }

        [TestCategory("LIMITE DIÁRIO"), TestMethod]
        public void DeveApagarLimiteDiario()
        {
            //var mock = new Mock<IOperacao<LimiteDiario>>();
            //var _mock = mock.Setup(x => x.Apagar(limiteDiario)).Returns(2); 

            operacoesFabrica = new LimiteDiario(_repositorioModeloQC, _repositorioModeloWM_DB);

            Assert.AreEqual(PopularEntidadeApagar().Count(), operacoesFabrica.Apagar(PopularEntidadeApagar()));
        }

        //[TestCategory("LIMITE DIÁRIO"), TestMethod]
        //public void DeveSelecionarRegistroLimiteDiario()
        //{
        //    LimitePerfilRisco limitePerfilRisco = new LimitePerfilRisco();
        //    limitePerfilRisco.dtIniVigencia = DateTime.Parse("2013-01-01 00:00:00");
        //    limitePerfilRisco.dtFimVigencia = DateTime.Parse("2013-12-30 00:00:00");
        //    limitePerfilRisco.inExcecao = "N";
        //    limitePerfilRisco.inDiarioMensal = "D";

        //    operacoesFabrica = new LimiteDiario(_repositorioModeloQC, _repositorioModeloWM_DB);

        //    var lstLimitePerfilRisco = operacoesFabrica.SelecionarRegistro(limitePerfilRisco);

        //    //Assert.IsTrue(lstLimitePerfilRisco.Count() > 0);
        //}

        #region Entidades
        private List<LimitePerfilRisco> PopularEntidadeGravar()
        {
            LimitePerfilRisco limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco.codLimitePerfilRisco = 1;
            limitePerfilRisco.codPerfilRisco = "AGGRESSI";
            limitePerfilRisco.dtIniVigencia = DateTime.Parse("2013-08-09 00:00:00");
            limitePerfilRisco.dtFimVigencia = DateTime.Parse("2013-08-25 00:00:00");
            limitePerfilRisco.vlrLimiteMin = 2;
            //limitePerfilRisco.vlrLimiteMax = ;
            limitePerfilRisco.codTipoFiltro = 1;
            limitePerfilRisco.codSubTipoFiltro = 1;
            limitePerfilRisco.dtAlteracao = DateTime.Now;
            //limitePerfilRisco.codUsuarioAlteracao = 1;    
            limitePerfilRisco.inExcecao = "N";
            limitePerfilRisco.inDiarioMensal = "D"; 

            LimitePerfilRisco limitePerfilRisco1 = new LimitePerfilRisco();
            limitePerfilRisco1.codLimitePerfilRisco = 2;
            limitePerfilRisco1.codPerfilRisco = "EQUITY";
            limitePerfilRisco1.dtIniVigencia = DateTime.Parse("2013-08-09 00:00:00");
            limitePerfilRisco1.dtFimVigencia = DateTime.Parse("2013-08-25 00:00:00");
            limitePerfilRisco1.vlrLimiteMin = 2;
            //limitePerfilRisco.vlrLimiteMax = ;
            limitePerfilRisco1.codTipoFiltro = 1;
            limitePerfilRisco1.codSubTipoFiltro = 1;
            limitePerfilRisco1.dtAlteracao = DateTime.Now;
            //limitePerfilRisco.codUsuarioAlteracao = 1; 
            limitePerfilRisco1.inExcecao = "N";
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
            limitePerfilRisco.codLimitePerfilRisco = 1;

            LimitePerfilRisco limitePerfilRisco1 = new LimitePerfilRisco();
            limitePerfilRisco1.codLimitePerfilRisco = 2;

            List<LimitePerfilRisco> lstLimitePerfilRisco = new List<LimitePerfilRisco>();
            lstLimitePerfilRisco.Add(limitePerfilRisco);
            lstLimitePerfilRisco.Add(limitePerfilRisco1); 

            return lstLimitePerfilRisco;
        }
        #endregion Entidades
    }
}
