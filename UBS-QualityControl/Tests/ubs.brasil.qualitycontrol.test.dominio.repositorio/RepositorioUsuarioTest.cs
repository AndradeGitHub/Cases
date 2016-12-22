//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Moq;

//using ubs.brasil.qualitycontrol.comum.entidade;
//using ubs.brasil.qualitycontrol.dominio.core;
//using ubs.brasil.qualitycontrol.dominio.core.interfaces;
//using ubs.brasil.qualitycontrol.dominio.repositorio;
//using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

//namespace ubs.brasil.qualitycontrol.test.dominio.repositorio
//{
//    [TestClass]
//    public class RepositorioUsuarioTest
//    {
//        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
//        private static RepositorioModeloQC _repositorioModeloQC;

//        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
//        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

//        private IRepositorio<Usuario> repositorioFabrica;

//        private Usuario usuario;

//        [TestInitialize]
//        public void InitializeTest()
//        {
//            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
//            _repositorioModeloQC = new RepositorioModeloQC();

//            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
//            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
//        }

//        [TestCategory("USUARIO"), TestMethod]
//        public void DeveConsultarUsuario()
//        {
//            usuario = new Usuario();
//            usuario.nomeLogin = "ANDRE_ANDRADE_ADMIN";
//            usuario.dsSenha = "andrade";

//            repositorioFabrica = new RepositorioUsuario(_repositorioModeloQC, _repositorioModeloWM_DB);

//            List<Usuario> lstProc = repositorioFabrica.SelecionarRegistro(usuario);

//            Assert.IsTrue(lstProc.Count() > 0);
//        }
//    }
//}