//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using ubs.brasil.qualitycontrol.comum.entidade;
//using ubs.brasil.qualitycontrol.dominio.core.interfaces;
//using ubs.brasil.qualitycontrol.dominio.repositorio;
//using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

//namespace ubs.brasil.qualitycontrol.dominio.core
//{
//    public class UsuarioAutenticacao : IOperacao<UsuarioDB>
//    {
//        private static dynamic repositorioFabrica;        

//        public UsuarioAutenticacao(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
//        {
//            RepositorioFabrica.repositorioModeloQC = repositorioModeloQC;
//            RepositorioFabrica.repositorioModeloWM_DB = repositorioModeloWM_DB;

//            repositorioFabrica = RepositorioFabrica.CriarRepositorio<UsuarioDB, RepositorioUsuario>();            
//        }

//        public List<Usuario> SelecionarRegistro(UsuarioDB usuario)
//        {
//            return repositorioFabrica.SelecionarRegistro(usuario);
//        }

//        #region NotImplemented
//        public int Gravar(List<UsuarioDB> usuario)
//        {
//            throw new NotImplementedException();
//        }

//        public int Alterar(List<UsuarioDB> usuario)
//        {
//            throw new NotImplementedException();
//        }

//        public int Apagar(List<UsuarioDB> usuario)
//        {
//            throw new NotImplementedException();
//        }

//        public List<UsuarioDB> SelecionarTudo()
//        {
//            throw new NotImplementedException();
//        }

//        public List<UsuarioDB> SelecionarRegistroDetalhe(Usuario usuario)
//        {
//            throw new NotImplementedException();
//        }

//        public List<UsuarioDB> SelecionarRegistroDetalheItem(Usuario usuario)
//        {
//            throw new NotImplementedException();
//        }

//        public List<UsuarioDB> SelecionarRegistroPorId(List<int> id)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion NotImplemented
//    }
//}