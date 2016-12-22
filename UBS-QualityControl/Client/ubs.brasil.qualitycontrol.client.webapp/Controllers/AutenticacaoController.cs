using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.core;

using ubs.brasil.qualitycontrol.client.webapp.Filters;

namespace ubs.brasil.qualitycontrol.client.webapp.Controllers
{
    public class AutenticacaoController : Controller
    {                
        public AutenticacaoController()
        {
        }

        [AllowAnonymous]
        public ActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Usuario model, FormCollection form)
        {
            try
            {
                AutenticacaoUsuario kerberosAutenticacao = new AutenticacaoUsuario();

                var usuarioAutenticacao = kerberosAutenticacao.VerificaAcesso(model.nomeLogin, model.dsSenha);                

                if (usuarioAutenticacao.Authenticated)
                {                                        
                    if (usuarioAutenticacao.User.Profiles.Count == 0)
                    {
                        model.Msg = "* Usuário sem perfil associado.";

                        return View(model);
                    }
                    else
                        FormsAuthentication.SetAuthCookie(string.Concat(usuarioAutenticacao.User.IdUser, "|", model.nomeLogin, "|", usuarioAutenticacao.User.Profiles[0].Name), false);                                            

                    return RedirectToAction("Login", "Autenticacao");
                }
                else
                    model.Msg = "* Username e/ou Password inválido";
            }
            catch (Exception ex)
            {
                model.Msg = string.Concat("* Erro: ", ex);
            }

            return View(model);
        }

        [Authorize]  
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Autenticacao");
        }
    }
}
