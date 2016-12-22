using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;
using ubs.brasil.qualitycontrol.dominio.core;

namespace ubs.brasil.qualitycontrol.client.webapp.Controllers
{
    [Authorize]  
    public class EnquadramentoController : Controller
    {
        private readonly RepositorioModeloQC repositorioModeloQC;
        private readonly RepositorioModeloWM_DB repositorioModeloWM_DB;

        private readonly IOperacao<Enquadramento> fabricaOperacaoEnquadramento;

        private readonly IRepositorio<Enquadramento> fabricaRepositorionquadramento;     

        private CultureInfo cultureData = new CultureInfo(GlobalConfig.CultureData);

        public EnquadramentoController()
        {            
            this.repositorioModeloQC = new RepositorioModeloQC();
            this.repositorioModeloWM_DB = new RepositorioModeloWM_DB();

            this.fabricaOperacaoEnquadramento = OperacaoFabrica.CriarOperacao<Enquadramento, EnquadramentoDiarioMensal>(repositorioModeloQC, repositorioModeloWM_DB);

            this.fabricaRepositorionquadramento = RepositorioFabrica.CriarRepositorio<Enquadramento, RepositorioEnquadramento>(repositorioModeloQC, repositorioModeloWM_DB);            
        }

        public ActionResult EnquadramentoDiario(Enquadramento model)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request["hdnLiberar"]))
                {
                    List<Enquadramento> lstEnqUpdate = new List<Enquadramento>();

                    Enquadramento enquadramento = new Enquadramento();
                    enquadramento.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]); 
                    enquadramento.codProcessamento = Convert.ToInt32(Request["hdnCodProcessamento"]);
                    enquadramento.codCarteira = Request["hdnCodCarteira"].ToString().Trim();
                    enquadramento.codListaSubTipo = Request["hdnLiberar"];

                    lstEnqUpdate.Add(enquadramento);

                    //Libera a carteira
                    int retLiberar = fabricaOperacaoEnquadramento.Alterar(lstEnqUpdate);                    
                }
            }
            catch (Exception ex)
            {
                model.Msg = string.Concat("* Erro: ", ex);                
            }

            return View(model);
        }

        public ActionResult EnquadramentoMensal(Enquadramento model)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request["hdnLiberar"]))
                {
                    List<Enquadramento> lstEnqUpdate = new List<Enquadramento>();

                    Enquadramento enquadramento = new Enquadramento();
                    enquadramento.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]); 
                    enquadramento.codProcessamento = Convert.ToInt32(Request["hdnCodProcessamento"]);
                    enquadramento.codCarteira = Request["hdnCodCarteira"].ToString().Trim();
                    enquadramento.codListaSubTipo = Request["hdnLiberar"];

                    lstEnqUpdate.Add(enquadramento);

                    //Libera a carteira
                    int retLiberar = fabricaOperacaoEnquadramento.Alterar(lstEnqUpdate);                    
                }
            }
            catch (Exception ex)
            {
                model.Msg = string.Concat("* Erro: ", ex);
            }            

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult EnquadramentoGrid(FormCollection form)
        {
            List<Enquadramento> lstRet = new List<Enquadramento>();

            if (form["acao"] != null && form["acao"].ToUpper().Equals("PESQUISAR"))
            {
                Enquadramento enquadramento = new Enquadramento();                
                enquadramento.inDiarioMensal = form["inDiarioMensal"];

                if (enquadramento.inDiarioMensal.ToUpper().Equals("MENSAL"))
                {
                    if (form["dtResultado"].Length.Equals(7))
                        enquadramento.dtResultado = DateTime.ParseExact(string.Concat("01/", form["dtResultado"]), "dd/MM/yyyy", cultureData);
                    else
                        enquadramento.dtResultado = DateTime.ParseExact(string.Concat("01/", form["dtResultado"].ToString().Substring(3, 7)), "dd/MM/yyyy", cultureData);
                }
                else
                    enquadramento.dtResultado = DateTime.ParseExact(form["dtResultado"], "dd/MM/yyyy", cultureData);

                lstRet = fabricaRepositorionquadramento.SelecionarRegistro(enquadramento);
                if (lstRet.Count == 0)
                    lstRet.Add(new Enquadramento { Msg = "* Registro não encontrado." });
                else
                {                    
                    lstRet[0].inDiarioMensal = enquadramento.inDiarioMensal;

                    if (enquadramento.inDiarioMensal.ToUpper().Equals("MENSAL"))
                    {
                        if (form["dtResultado"].Length.Equals(7))
                            lstRet[0].DtPesq = DateTime.ParseExact(form["dtResultado"], "MM/yyyy", cultureData);
                        else
                            lstRet[0].DtPesq = DateTime.ParseExact(form["dtResultado"].ToString().Substring(3, 7), "MM/yyyy", cultureData);                        
                    }
                    else
                        lstRet[0].DtPesq = DateTime.ParseExact(form["dtResultado"], "dd/MM/yyyy", cultureData);
                }

                return PartialView("../Enquadramento/_EnquadramentoGridPartial", lstRet);                
            }
            else
                return PartialView("../Enquadramento/_EnquadramentoGridPartial");
        }

        [ValidateInput(false)]
        public ActionResult EnquadramentoDetalhe(Enquadramento model)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["parametros"]))
            {
                string[] parametro = Request.QueryString["parametros"].Split(',');

                Enquadramento enquadramento = new Enquadramento();
                enquadramento.codCarteira = parametro[0];
                enquadramento.codProcessamento = Convert.ToInt32(parametro[1]);

                List<Enquadramento> lstRet = fabricaRepositorionquadramento.SelecionarRegistroDetalhe(enquadramento);                
                if (lstRet.Count > 0)
                {
                    if (!string.IsNullOrEmpty(parametro[2]))
                    {
                        lstRet[0].dtResultado = DateTime.ParseExact(parametro[2], "dd/MM/yyyy", cultureData);
                        lstRet[0].inDiarioMensal = parametro[3];
                    }
                    else
                    {
                        lstRet[0].Acao = parametro[4];
                    }


                    return PartialView("../Enquadramento/_EnquadramentoDetalhePartial", lstRet);
                }
                else
                {
                    return PartialView("../Enquadramento/_EnquadramentoDetalhePartial");
                }
            }
            else
            {
                return PartialView("../Enquadramento/_EnquadramentoDetalhePartial");
            }
        }
    }
}
