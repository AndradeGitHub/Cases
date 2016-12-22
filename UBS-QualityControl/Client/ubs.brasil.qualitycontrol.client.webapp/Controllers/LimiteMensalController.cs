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
    public class LimiteMensalController : Controller
    {
        private readonly RepositorioModeloQC repositorioModeloQC;
        private readonly RepositorioModeloWM_DB repositorioModeloWM_DB;

        private readonly IOperacao<LimitePerfilRisco> fabricaOperacao;        

        private readonly IRepositorio<PerfilDeRisco> fabricaRepositorioPerfil;
        private readonly IRepositorio<LimitePerfilRisco> fabricaRepositorioLimite;
  
        private List<LimitePerfilRisco> lstLimiteOperacaoList;
        
        private CultureInfo cultureData = new CultureInfo(GlobalConfig.CultureData);
        private CultureInfo cultureVal = new CultureInfo(GlobalConfig.CultureValLimite);  

        public LimiteMensalController()
        {
            this.repositorioModeloQC = new RepositorioModeloQC();
            this.repositorioModeloWM_DB = new RepositorioModeloWM_DB();

            this.fabricaOperacao = OperacaoFabrica.CriarOperacao<LimitePerfilRisco, LimiteMensal>(repositorioModeloQC, repositorioModeloWM_DB);

            this.fabricaRepositorioPerfil = RepositorioFabrica.CriarRepositorio<PerfilDeRisco, RepositorioPerfilRisco>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaRepositorioLimite = RepositorioFabrica.CriarRepositorio<LimitePerfilRisco, RepositorioLimiteMensal>(repositorioModeloQC, repositorioModeloWM_DB);            

            this.lstLimiteOperacaoList = new List<LimitePerfilRisco>();
        }

        #region LimiteMensal
        
        public ActionResult LimiteMensal(LimitePerfilRisco model)
        {                             
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult PerfilRiscoList()
        {
            return PartialView("../Operacao/_PerfilRiscoPartial", fabricaRepositorioPerfil.SelecionarTudo());
        }

        [ValidateInput(false)]
        public ActionResult LimiteRiscoList(LimitePerfilRisco model, FormCollection form)
        {
            if (form["acao"] != null && form["acao"].ToUpper().Equals("PESQUISAR"))
            {
                LimitePerfilRisco limitePerfilRisco = new LimitePerfilRisco();
                limitePerfilRisco.codPerfilRisco = form["cboPerfilRisco"];

                if (!string.IsNullOrEmpty(form["dtInicial"]))
                    limitePerfilRisco.dtInicial = DateTime.ParseExact(string.Concat("01/", form["dtInicial"]), "dd/MM/yyyy", cultureData);

                limitePerfilRisco.codUsuarioAlteracao = Convert.ToInt32(User.Identity.Name.Split('|')[0]);

                var lstRet = fabricaRepositorioLimite.SelecionarRegistro(limitePerfilRisco);
                if (lstRet.Count > 0) 
                    lstRet[0].Acao = "mensal";
                else
                    lstRet.Add(new LimitePerfilRisco { Msg = "* Registro não encontrado." });

                return PartialView("../Operacao/_LimiteRiscoPartial", lstRet);
            }
            else
                return PartialView("../Operacao/_LimiteRiscoPartial");
        }

        [HttpPost]
        [ValidateInput(false)]   
        public ActionResult LimiteMensal(LimitePerfilRisco model, FormCollection form)
        {       
            return View(model);
        } 

        #endregion LimiteMensal

        #region LimiteMensalOperacao
        
        public ActionResult LimiteMensalOperacao()
        {            
            return View();
        }
        
        [ChildActionOnly]
        public ActionResult LimitePerfilRiscoList()
        {
            List<PerfilDeRisco> lstRet = fabricaRepositorioPerfil.SelecionarTudo();
            if (lstRet.Count > 0) 
                lstRet[0].Acao = "mensal";
            
            return PartialView("../Operacao/_LimitePerfilRiscoPartial", lstRet);
        }
        
        [ChildActionOnly]
        public ActionResult LimiteMensalOperacaoList(string codLimitePerfilRisco, string dtIniVigencia, string dtFimVigencia)
        {
            LimitePerfilRisco limitePerfilRisco = new LimitePerfilRisco();
            limitePerfilRisco.dtInicial = DateTime.ParseExact(dtIniVigencia, "M/yyyy", cultureData); 
            limitePerfilRisco.dtFinal = DateTime.ParseExact(dtFimVigencia, "M/yyyy", cultureData);

            var lstRet = fabricaRepositorioLimite.SelecionarRegistro(limitePerfilRisco);
            if (lstRet.Count > 0) lstRet[0].Acao = "mensal";

            return PartialView("../Operacao/_LimiteRiscoOperacaoPartial", lstRet);                        
        }

        [HttpPost]        
        public ActionResult LimiteMensalOperacao(LimitePerfilRisco model, FormCollection form)
        {
            string acao = form["acao"];

            try
            {
                switch (acao.ToUpper())
                {
                    case "INCLUIR":
                        int retInclusao = IncluirLimiteMensal(form);

                        if (retInclusao > 0)
                            model.Msg = "* Limite Mensal incluído.";
                        else                        
                            if (retInclusao == -1)
                                model.Msg = "* Já existe limite para esta data. Inclusão não efetuada.";
                            else
                                model.Msg = "* Erro: Limite Mensal não incluído.";                    
                        break;
                    case "ALTERAR":
                        if (AlterarLimiteMensal(form) > 0)
                            model.Msg = "* Limite Mensal alterado.";
                        else
                            model.Msg = "* Erro: Limite Mensal não alterado.";
                        break;
                    case "EXCLUIR":
                        if (ExcluirLimiteMensal(form) > 0)
                            model.Msg = "* Limite Mensal excluído.";
                        else
                            model.Msg = "* Erro: Limite Mensal não encontrado para exclusão.";
                        break;
                    default:
                        model.Msg = "* Erro: Ação não selecionada.";
                        break;
                }
            }
            catch (Exception ex)
            {
                model.Msg = string.Concat("* Erro: ", ex);
            }

            return View(model);
        }

        private int IncluirLimiteMensal(FormCollection form)
        {
            return fabricaOperacao.Gravar(MontaParamsIncluir(form));
        }

        private int AlterarLimiteMensal(FormCollection form)
        {
            return fabricaOperacao.Alterar(MontaParamsAlterarExcluir(form));
        }

        private int ExcluirLimiteMensal(FormCollection form)
        {
            return fabricaOperacao.Apagar(MontaParamsAlterarExcluir(form));
        }

        private List<LimitePerfilRisco> MontaParamsIncluir(FormCollection form)
        {
            List<LimitePerfilRisco> lstPerfilrisco = new List<LimitePerfilRisco>();

            string vlrLimiteMinForm = string.Empty;
            string vlrLimiteMaxForm = string.Empty;

            int countform = (form.Count - 9) / 4;

            for (int i = 0; i < countform; i++)
            {
                //Diário Patrimonial
                LimitePerfilRisco perfilRiscoPatrimonial = new LimitePerfilRisco();
                perfilRiscoPatrimonial.codPerfilRisco = form["codPerfilRiscoHidden_" + i];

                perfilRiscoPatrimonial.dtIniVigencia = DateTime.ParseExact(string.Concat("01/", form["dtIniVigencia"]), "dd/MM/yyyy", cultureData); 
                perfilRiscoPatrimonial.dtFimVigencia = DateTime.ParseExact(string.Concat("01/", form["dtIniVigencia"]), "dd/MM/yyyy", cultureData);

                vlrLimiteMinForm = "vlLimiteMinPatrimonial_" + perfilRiscoPatrimonial.codPerfilRisco;
                perfilRiscoPatrimonial.vlrLimiteMin = string.IsNullOrEmpty(form[vlrLimiteMinForm]) ? 0 : Convert.ToDouble(form[vlrLimiteMinForm], cultureVal);

                vlrLimiteMaxForm = "vlLimiteMaxPatrimonial_" + perfilRiscoPatrimonial.codPerfilRisco;
                perfilRiscoPatrimonial.vlrLimiteMax = string.IsNullOrEmpty(form[vlrLimiteMaxForm]) ? 0 : Convert.ToDouble(form[vlrLimiteMaxForm], cultureVal);

                perfilRiscoPatrimonial.codTipoFiltro = 2;
                perfilRiscoPatrimonial.codSubTipoFiltro = 10;
                perfilRiscoPatrimonial.dtAlteracao = DateTime.Now;
                perfilRiscoPatrimonial.inExcecao = "N";
                perfilRiscoPatrimonial.inDiarioMensal = "M";
                perfilRiscoPatrimonial.codUsuarioAlteracao = Convert.ToInt32(User.Identity.Name.Split('|')[0]); 

                lstPerfilrisco.Add(perfilRiscoPatrimonial);

                //Diário da Cota
                LimitePerfilRisco perfilRiscoCota = new LimitePerfilRisco();
                perfilRiscoCota.codPerfilRisco = form["codPerfilRiscoHidden_" + i];

                perfilRiscoCota.dtIniVigencia = DateTime.ParseExact(string.Concat("01/", form["dtIniVigencia"]), "dd/MM/yyyy", cultureData); 
                perfilRiscoCota.dtFimVigencia = DateTime.ParseExact(string.Concat("01/", form["dtIniVigencia"]), "dd/MM/yyyy", cultureData); 

                vlrLimiteMinForm = "vlLimiteMinDiaria_" + perfilRiscoCota.codPerfilRisco;
                perfilRiscoCota.vlrLimiteMin = string.IsNullOrEmpty(form[vlrLimiteMinForm]) ? 0 : Convert.ToDouble(form[vlrLimiteMinForm], cultureVal);

                vlrLimiteMaxForm = "vlLimiteMaxDiaria_" + perfilRiscoCota.codPerfilRisco;
                perfilRiscoCota.vlrLimiteMax = string.IsNullOrEmpty(form[vlrLimiteMaxForm]) ? 0 : Convert.ToDouble(form[vlrLimiteMaxForm], cultureVal);

                perfilRiscoCota.codTipoFiltro = 2;
                perfilRiscoCota.codSubTipoFiltro = 12;
                perfilRiscoCota.dtAlteracao = DateTime.Now;
                perfilRiscoCota.inExcecao = "N";
                perfilRiscoCota.inDiarioMensal = "M";
                perfilRiscoCota.codUsuarioAlteracao = Convert.ToInt32(User.Identity.Name.Split('|')[0]); 

                lstPerfilrisco.Add(perfilRiscoCota);
            }

            return lstPerfilrisco;
        }

        private List<LimitePerfilRisco> MontaParamsAlterarExcluir(FormCollection form)
        {
            List<LimitePerfilRisco> lstPerfilrisco = new List<LimitePerfilRisco>();

            string[] codLimiteSplit = form["codLimitePerfilRiscoHidden"].Split(',');
            string[] codPerfilSplit = form["codPerfilRiscoHidden"].Split(',');
            string[] codLimiteMinPatrimonialSplit = form["vlLimiteMinPatrimonial"].Split(',');
            string[] codLimiteMaxPatrimonialSplit = form["vlLimiteMaxPatrimonial"].Split(',');
            string[] codLimiteMinDiariaSplit = form["vlLimiteMinDiaria"].Split(',');
            string[] codLimiteMaxDiariaSplit = form["vlLimiteMaxDiaria"].Split(',');

            for (int i = 0; i < codLimiteSplit.Count(); i++)
            {
                string[] codSplitInt = codLimiteSplit[i].Split('/');

                for (int j = 0; j < codSplitInt.Count(); j++)
                {
                    LimitePerfilRisco perfilRiscoPatrimonial = new LimitePerfilRisco();

                    perfilRiscoPatrimonial.codLimitePerfilRisco = Convert.ToInt32(codSplitInt[j]);
                    perfilRiscoPatrimonial.codPerfilRisco = codPerfilSplit[i].ToString();
                    perfilRiscoPatrimonial.dtIniVigencia = DateTime.ParseExact(form["dtIniVigenciaHidden"], "dd/MM/yyyy", cultureData); 
                    perfilRiscoPatrimonial.dtFimVigencia = DateTime.ParseExact(form["dtFimVigenciaHidden"], "dd/MM/yyyy", cultureData); 
                    perfilRiscoPatrimonial.codTipoFiltro = 2;
                    perfilRiscoPatrimonial.dtAlteracao = DateTime.Now;
                    perfilRiscoPatrimonial.inExcecao = "N";
                    perfilRiscoPatrimonial.inDiarioMensal = "M";
                    perfilRiscoPatrimonial.codUsuarioAlteracao = Convert.ToInt32(User.Identity.Name.Split('|')[0]); 

                    if (j == 0) //Diário Patrimonial
                    {
                        perfilRiscoPatrimonial.vlrLimiteMin = string.IsNullOrEmpty(codLimiteMinPatrimonialSplit[i]) ? 0 : Convert.ToDouble(codLimiteMinPatrimonialSplit[i], cultureVal);
                        perfilRiscoPatrimonial.vlrLimiteMax = string.IsNullOrEmpty(codLimiteMaxPatrimonialSplit[i]) ? 0 : Convert.ToDouble(codLimiteMaxPatrimonialSplit[i], cultureVal);
                        perfilRiscoPatrimonial.codSubTipoFiltro = 10;
                    }
                    else //Diário da Cota
                    {

                        perfilRiscoPatrimonial.vlrLimiteMin = string.IsNullOrEmpty(codLimiteMinDiariaSplit[i]) ? 0 : Convert.ToDouble(codLimiteMinDiariaSplit[i], cultureVal);
                        perfilRiscoPatrimonial.vlrLimiteMax = string.IsNullOrEmpty(codLimiteMaxDiariaSplit[i]) ? 0 : Convert.ToDouble(codLimiteMaxDiariaSplit[i], cultureVal);
                        perfilRiscoPatrimonial.codSubTipoFiltro = 12;
                    }

                    lstPerfilrisco.Add(perfilRiscoPatrimonial);
                }
            }

            return lstPerfilrisco;
        }

        #endregion LimiteMensalOperacao
    }
}
