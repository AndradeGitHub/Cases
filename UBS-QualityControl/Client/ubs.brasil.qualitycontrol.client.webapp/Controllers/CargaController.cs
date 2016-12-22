using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

using ubs.brasil.qualitycontrol.comum.globals;
using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;
using ubs.brasil.qualitycontrol.dominio.core;

namespace ubs.brasil.qualitycontrol.client.webapp.Controllers
{
    [Authorize] 
    public class CargaController : Controller
    {
        private readonly RepositorioModeloQC repositorioModeloQC;
        private readonly RepositorioModeloWM_DB repositorioModeloWM_DB;
                         
        private readonly IOperacao<Processamento> fabricaProcessamentoManual;
        private readonly IOperacao<LogCarga> fabricaCargaLog;

        private readonly IRepositorio<CargaDB> fabricaRepositorioCarga;
        private readonly IRepositorio<Carteira> fabricaRepositorioCarteira;        

        private CultureInfo cultureData = new CultureInfo(GlobalConfig.CultureData);

        public CargaController()
        {
            this.repositorioModeloQC = new RepositorioModeloQC();
            this.repositorioModeloWM_DB = new RepositorioModeloWM_DB();

            this.fabricaCargaLog = OperacaoFabrica.CriarOperacao<LogCarga, CargaLog>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaProcessamentoManual = OperacaoFabrica.CriarOperacao<Processamento, ProcessamentoManual>(repositorioModeloQC, repositorioModeloWM_DB);            

            this.fabricaRepositorioCarga = RepositorioFabrica.CriarRepositorio<CargaDB, RepositorioCarga>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaRepositorioCarteira = RepositorioFabrica.CriarRepositorio<Carteira, RepositorioCarteira>(repositorioModeloQC, repositorioModeloWM_DB);            
        }

        [HttpGet]
        public ActionResult CargaGeral()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CargaGeral(Processamento model, FormCollection form)
        {
            try
            {
                if (form["acao"] != null)
                {
                    switch (form["acao"].ToUpper())
                    {
                        case "CARGA":
                            model = ProcessamentoCarga(model, form);

                            break;
                        case "CARGA_DETALHE":
                            model = ProcessamentoCargaDetalhe(model, form);                                                    

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                model.Msg = string.Concat("* Erro: ", ex.Message);
            }

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult CarteiraGrid(FormCollection form)
        {
            List<Carteira> lstRet = fabricaRepositorioCarteira.SelecionarTudo();
            if (lstRet.Count == 0)
                lstRet.Add(new Carteira { Msg = "* Registro não encontrado." });
            else
            {
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

                lstRet[0].Order = Request["order"];

                ViewBag.QtdeRegistro = string.Concat(lstRet.Count, " registro(s) encontrado(s).");
            }

            return PartialView("../Processamento/_CarteiraGridPartial", lstRet);
        }

        private Processamento ProcessamentoCarga(Processamento model, FormCollection form)
        {
            List<Processamento> lstCarga = new List<Processamento>();

            model.Acao = "CargaSimples";
            model.codCarteira = form["chkProcessar"];
            model.TempoEspera = Convert.ToInt32(GlobalConfig.TempoEsperaProcessamento);

            lstCarga.Add(model);

            fabricaProcessamentoManual.Gravar(lstCarga);

            Session.Add("dtResultadoCarga", DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData));

            model.dtResultado = Convert.ToDateTime(Session["dtResultadoCarga"]);
            model.acaoProcessamento = "Carga iniciada";

            return model;
        }

        private Processamento ProcessamentoCargaDetalhe(Processamento model, FormCollection form)
        {
            if (string.IsNullOrEmpty(form["hdnCodCarga"]))
                model.codCarga = fabricaRepositorioCarga.SelecionarTudo()[0].codCarga;
            else
                model.codCarga = Convert.ToInt32(form["hdnCodCarga"]);

            if (model.codCarga > 0)
            {
                LogCarga logCarga = new LogCarga();
                logCarga.codCarga = model.codCarga;
                logCarga.TempoEspera = Convert.ToInt32(GlobalConfig.TempoEsperaLog);

                if (string.IsNullOrEmpty(form["ordem"]) || form["ordem"].Equals("0"))
                    logCarga.codOrdem = 1;
                else
                    logCarga.codOrdem = Convert.ToInt32(form["ordem"]) + 1;

                if (logCarga.codOrdem == 15)
                {
                    model.txtDescLogCargas = form["hdnDescLogCarga"];
                    model.acaoProcessamento = "Carga concluída";
                }
                else
                {
                    var retCargaDetalhe = fabricaCargaLog.SelecionarLogCargaEmEspera(logCarga);

                    model.dtResultado = Convert.ToDateTime(Session["dtResultadoCarga"]);                    
                    model.ordem = retCargaDetalhe[0].codOrdem;

                    if (string.IsNullOrEmpty(form["hdnDescLogCarga"]))
                        model.txtDescLogCargas = Utils.GetEnumDescriptionLogCargaOrdem(retCargaDetalhe[0].codOrdem);
                    else
                    {
                        model.txtDescLogCargas = string.Concat(form["hdnDescLogCarga"], "|", Utils.GetEnumDescriptionLogCargaOrdem(retCargaDetalhe[0].codOrdem));
                    }

                    model.acaoProcessamento = "Carga iniciada";
                }
            }

            model.codCarteira = form["chkProcessar"];

            return model;
        }
    }
}
