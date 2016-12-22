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

//namespace ubs.brasil.qualitycontrol.test.dominio.core
//{
//    [TestClass]
//    public class UsuarioAutenticacaoTest
//    {
//        private static Mock<RepositorioModeloQC> _mockRepositorioModeloQC;
//        private static RepositorioModeloQC _repositorioModeloQC;

//        private static Mock<RepositorioModeloWM_DB> _mockRepositorioModeloWM_DB;
//        private static RepositorioModeloWM_DB _repositorioModeloWM_DB;

//        private IOperacao<Usuario> operacoesFabrica;

//        [TestInitialize]
//        public void InitializeTest()
//        {
//            _mockRepositorioModeloQC = new Mock<RepositorioModeloQC>();
//            _repositorioModeloQC = new RepositorioModeloQC();

//            _mockRepositorioModeloWM_DB = new Mock<RepositorioModeloWM_DB>();
//            _repositorioModeloWM_DB = new RepositorioModeloWM_DB();
//        }

//        [TestCategory("USUARIO"), TestMethod]
//        public void DeveConsultarUsuarioAutenticacao()
//        {
//            List<Usuario> lstUsuario = new List<Usuario>();

//            Usuario usuario = new Usuario();
//            usuario.nomeLogin = "ANDRE_ANDRADE_ADMIN";
//            //usuario.nomeLogin = "ANDRE_ANDRADE_VIEW";
//            usuario.dsSenha = "andrade";

//            lstUsuario.Add(usuario);

//            operacoesFabrica = new UsuarioAutenticacao(_repositorioModeloQC, _repositorioModeloWM_DB);
//            List<Usuario> lstProc = operacoesFabrica.SelecionarRegistro(usuario);

//            Assert.IsTrue(lstProc.Count > 0);
//        }

//        [TestCategory("USUARIO"), TestMethod]
//        public void DeveConsultarUsuarioAutenticacao_USUARIO_INEXISTENTE()
//        {
//            List<Usuario> lstUsuario = new List<Usuario>();

//            Usuario usuario = new Usuario();
//            usuario.nomeLogin = "";
//            usuario.dsSenha = "";

//            lstUsuario.Add(usuario);

//            operacoesFabrica = new UsuarioAutenticacao(_repositorioModeloQC, _repositorioModeloWM_DB);
//            List<Usuario> lstProc = operacoesFabrica.SelecionarRegistro(usuario);

//            Assert.IsTrue(lstProc.Count == 0);
//        }
//    }
//}