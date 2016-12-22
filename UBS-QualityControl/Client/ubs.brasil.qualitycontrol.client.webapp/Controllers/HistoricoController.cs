using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using PagedList;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;
using ubs.brasil.qualitycontrol.dominio.core;

namespace ubs.brasil.qualitycontrol.client.webapp.Controllers
{
    [Authorize]  
    public class HistoricoController : Controller
    {
        private readonly RepositorioModeloQC repositorioModeloQC;
        private readonly RepositorioModeloWM_DB repositorioModeloWM_DB;

        private readonly IOperacaoLog fabricaHistoricoOperacao;        

        private readonly IRepositorio<Carteira> fabricaRepositorioCarteira;
        private readonly IRepositorio<LogProcessamento> fabricaRepositorioHistorico; 

        private CultureInfo cultureData = new CultureInfo(GlobalConfig.CultureData);

        public HistoricoController()
        {
            this.repositorioModeloQC = new RepositorioModeloQC();
            this.repositorioModeloWM_DB = new RepositorioModeloWM_DB();

            this.fabricaHistoricoOperacao = OperacaoFabrica.CriarOperacaoLog<OperacaoLog>(repositorioModeloQC, repositorioModeloWM_DB);            

            this.fabricaRepositorioCarteira = RepositorioFabrica.CriarRepositorio<Carteira, RepositorioCarteira>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaRepositorioHistorico = RepositorioFabrica.CriarRepositorio<LogProcessamento, RepositorioProcessamentoHistorico>(repositorioModeloQC, repositorioModeloWM_DB);
        }

        #region Histórico de Operação
        public ActionResult HistoricoOperacao()
        {
            return View();
        }
       
        [ValidateInput(false)]
        public ActionResult HistoricoOperacaoGrid(FormCollection form, string dtIniForm, string dtFimForm, string logOperacaoForm, int pagina = 0)        
        {
            if (form["acao"] != null && form["acao"].ToUpper().Equals("PESQUISAR") || !string.IsNullOrEmpty(logOperacaoForm))
            {
                LogOperacao logOperacao = new LogOperacao();

                if (string.IsNullOrEmpty(logOperacaoForm))
                {
                    DateTime dtInicial = DateTime.ParseExact(form["dataInicial"], "d/M/yyyy", cultureData);
                    DateTime dtFinal = DateTime.ParseExact(string.Concat(form["dataFinal"], " 23:59:00"), "d/M/yyyy HH:mm:ss", cultureData);
                    
                    logOperacao.dataInicial = dtInicial;
                    logOperacao.dataFinal = dtFinal;
                    logOperacao.nomeFuncionalidade = form["optFuncionalidade"];
                    logOperacao.acao = form["optAcao"];
                    logOperacao.nomeTipoDescricao = form["optTipoDesc"];                    
                }
                else
                {
                    string[] logOperacaoFormSplit = logOperacaoForm.Split(',');

                    DateTime dtInicial = DateTime.ParseExact(logOperacaoFormSplit[0], "d/M/yyyy", cultureData);
                    DateTime dtFinal = DateTime.ParseExact(string.Concat(logOperacaoFormSplit[1], " 23:59:00"), "d/M/yyyy HH:mm:ss", cultureData);
                    
                    logOperacao.dataInicial = dtInicial;
                    logOperacao.dataFinal = dtFinal;
                    logOperacao.nomeFuncionalidade = logOperacaoFormSplit[2];
                    logOperacao.acao = logOperacaoFormSplit[3];
                    logOperacao.nomeTipoDescricao = logOperacaoFormSplit[4];                    
                }

                logOperacao.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]); 

                List<LogOperacao> lstRet = fabricaHistoricoOperacao.SelecionarRegistro(logOperacao);
                if (lstRet.Count == 0)
                {
                    lstRet.Add(new LogOperacao { Msg = "* Registro não encontrado." });

                    return PartialView("../Historico/_HistoricoOperacaoGridPartial", lstRet.ToPagedList(1, 1));             
                }
                else
                {                                        
                    ViewBag.logOperacaoForm = string.Concat(logOperacao.dataInicial.ToString("d/M/yyyy"),",",logOperacao.dataFinal.ToString("d/M/yyyy"),",",logOperacao.nomeFuncionalidade,",",logOperacao.acao,",",logOperacao.nomeTipoDescricao);
                    ViewBag.QtdeRegistro = string.Concat(lstRet.Count, " registro(s) encontrado(s).");

                    if (string.IsNullOrEmpty(dtIniForm))
                        ViewBag.DtIniForm = DateTime.ParseExact(form["dataInicial"], "d/M/yyyy", cultureData);
                    else
                        ViewBag.DtIniForm = DateTime.ParseExact(dtIniForm.ToString(), "d/M/yyyy", cultureData);

                    if (string.IsNullOrEmpty(dtFimForm))
                        ViewBag.DtFimForm = DateTime.ParseExact(form["dataFinal"], "d/M/yyyy", cultureData);
                    else
                        ViewBag.DtFimForm = DateTime.ParseExact(dtFimForm.ToString(), "d/M/yyyy", cultureData);  

                    int paginaTamanho = 500;
                    int paginaNumero = (pagina == 0 ? 1 : pagina);                    

                    return PartialView("../Historico/_HistoricoOperacaoGridPartial", lstRet.ToPagedList(paginaNumero, paginaTamanho));                                       
                }                
            }
            else
            {
                return PartialView("../Historico/_HistoricoOperacaoGridPartial");
            }
        }       
        #endregion Histórico de Operação

        #region Histórico de Processamento
        public ActionResult HistoricoProcessamento()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["parametrosSelecionar"]))                
                Session.Add("carteiras", Request.QueryString["parametrosSelecionar"]);                       

            return View();
        }
        
        [ValidateInput(false)]        
        public ActionResult HistoricoProcessamentoGrid(FormCollection form, string dtForm, string logProcessamentoForm, int pagina = 0)
        {
            if (form["acao"] != null && form["acao"].ToUpper().Equals("PESQUISAR") || !string.IsNullOrEmpty(logProcessamentoForm))
            {
                LogProcessamento processamento = new LogProcessamento();

                if (string.IsNullOrEmpty(logProcessamentoForm))
                    processamento.dtProcessada = DateTime.ParseExact(form["dtProcessada"], "d/M/yyyy", cultureData);                
                else
                {
                    processamento.dtProcessada = DateTime.ParseExact(logProcessamentoForm, "d/M/yyyy", cultureData);                                        
                }

                if (Session["carteiras"] != null)
                {
                    processamento.codCarteira = Session["carteiras"].ToString();
                    Session.Remove("carteiras");                
                }

                List<LogProcessamento> lstRet = fabricaRepositorioHistorico.SelecionarRegistro(processamento);
                if (lstRet.Count == 0)
                {
                    lstRet.Add(new LogProcessamento { Msg = "* Registro não encontrado." });

                    return PartialView("../Historico/_HistoricoProcessamentoGridPartial", lstRet.ToPagedList(1, 1));    
                }
                else
                {                    
                    ViewBag.logProcessamentoForm = processamento.dtProcessada.ToString("d/M/yyyy");
                    ViewBag.QtdeRegistro = string.Concat(lstRet.Count, " registro(s) encontrado(s).");

                    if (string.IsNullOrEmpty(dtForm))
                        ViewBag.DtForm = DateTime.ParseExact(form["dtProcessada"], "d/M/yyyy", cultureData);
                    else
                        ViewBag.DtForm = DateTime.ParseExact(dtForm.ToString(), "d/M/yyyy", cultureData);                     

                    int paginaTamanho = 500;
                    int paginaNumero = (pagina == 0 ? 1 : pagina);                    

                    return PartialView("../Historico/_HistoricoProcessamentoGridPartial", lstRet.ToPagedList(paginaNumero, paginaTamanho));
                }                               
            }
            else
            {                
                return PartialView("../Historico/_HistoricoProcessamentoGridPartial");
            }
        }

        [ValidateInput(false)]        
        public ActionResult HistoricoProcessamentoCarteiras()
        {
            List<Carteira> lstRet = fabricaRepositorioCarteira.SelecionarTudo();

            lstRet = lstRet.GroupBy(group => new
            {
                group.codCarteira,
                group.nomeCarteira
            })
            .Select(group => new Carteira
            {
                codCarteira = group.Key.codCarteira,
                nomeCarteira = group.Key.nomeCarteira
            })
            .ToList<Carteira>();   

            return PartialView("../Historico/_CarteiraPopPartial", lstRet);                        
        }        
        #endregion Histórico de Processamento
    }
}