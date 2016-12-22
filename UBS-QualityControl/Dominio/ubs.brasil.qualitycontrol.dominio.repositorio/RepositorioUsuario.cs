//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Security;

//using ubs.brasil.qualitycontrol.comum.entidade;
//using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

//namespace ubs.brasil.qualitycontrol.dominio.repositorio
//{
//    public class RepositorioUsuario : Repositorio<Usuario>
//    {
//        private readonly RepositorioModeloQC dbQC;
//        private readonly RepositorioModeloWM_DB dbWM_DB;

//        public RepositorioUsuario(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
//        {
//            this.dbQC = repositorioModeloQC;
//            this.dbWM_DB = repositorioModeloWM_DB;
//        }

//        public override List<Usuario> SelecionarRegistro(Usuario usuario)
//        {
//            var resQC = from usu in dbQC.Usuario
//                        where
//                          usu.nomeLogin == usuario.nomeLogin &&
//                          usu.dsSenha == usuario.dsSenha &&
//                          usu.inAtivoInativo.ToUpper() == "A"
//                        select usu;

//            return resQC.ToList<Usuario>();
//        }
//    }
//}