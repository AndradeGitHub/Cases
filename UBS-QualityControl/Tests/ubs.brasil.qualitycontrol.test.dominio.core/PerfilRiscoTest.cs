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
    public class PerfilRiscoTest
    {
        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
        private static RepositorioModeloQC _repositorioModeloQC;

        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

        private IOperacao<PerfilDeRisco> operacoesFabrica;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
            _repositorioModeloQC = new RepositorioModeloQC();

            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
        }

        //[TestCategory("PERFIL RISCO"), TestMethod]
        //public void DeveConsultarTodosOsPerfisRisco()
        //{
        //    operacoesFabrica = new PerfilRisco(_repositorioModeloQC, _repositorioModeloWM_DB);
        //    List<PerfilDeRisco> lstRet = operacoesFabrica.SelecionarTudo();

        //    Assert.IsTrue(lstRet.Count > 0);
        //}
    }
}