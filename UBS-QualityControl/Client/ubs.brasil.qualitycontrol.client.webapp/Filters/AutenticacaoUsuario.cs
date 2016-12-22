using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ubs.brasil.qualitycontrol.client.webapp.Filters
{
    public class AutenticacaoUsuario : Controller
    {
        public Kerberos.Framework.DTO.Authentication VerificaAcesso(string login, string password)
        {                     
            using (Kerberos.BLL.General BLL = new Kerberos.BLL.General())
            {                
                return BLL.User_Authenticate(login, password, new Kerberos.Framework.DTO.Log());                   
            }            
        }

        public Kerberos.Framework.DTO.User RetornaUsuarioPorId(int idUser)
        {                     
            using (Kerberos.BLL.General BLL = new Kerberos.BLL.General())
            {
                return BLL.UserFull_GetIdUser(idUser, new Kerberos.Framework.DTO.Log());          
            }                    
        }                
    }
}
