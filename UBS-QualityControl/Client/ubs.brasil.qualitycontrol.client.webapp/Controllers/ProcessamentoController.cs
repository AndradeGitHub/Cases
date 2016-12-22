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
    public class ProcessamentoController : Controller
    {
        private readonly RepositorioModeloQC repositorioModeloQC;
        private readonly RepositorioModeloWM_DB repositorioModeloWM_DB;

        private readonly IOperacao<Processamento> fabricaProcessamentoResultado;        
        private readonly IOperacao<Enquadramento> fabricaOperacaoEnquadramento;        
        private readonly IOperacao<Processamento> fabricaProcessamentoManual;
        private readonly IOperacao<LogProcessamento> fabricaProcessamentoLog;
        private readonly IOperacao<LogCarga> fabricaCargaLog;

        private readonly IRepositorio<CargaDB> fabricaRepositorioCarga;
        private readonly IRepositorio<Carteira> fabricaRepositorioCarteira;
        private readonly IRepositorio<Processamento> fabricaRepositorioProcessamentoManual;
        private readonly IRepositorio<Processamento> fabricaRepositorioProcessamentoResultado;        

        private CultureInfo cultureData = new CultureInfo(GlobalConfig.CultureData);        

        public ProcessamentoController()
        {
            this.repositorioModeloQC = new RepositorioModeloQC();
            this.repositorioModeloWM_DB = new RepositorioModeloWM_DB();

            this.fabricaProcessamentoResultado = OperacaoFabrica.CriarOperacao<Processamento, ProcessamentoResultado>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaOperacaoEnquadramento = OperacaoFabrica.CriarOperacao<Enquadramento, EnquadramentoDiarioMensal>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaProcessamentoManual = OperacaoFabrica.CriarOperacao<Processamento, ProcessamentoManual>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaProcessamentoLog = OperacaoFabrica.CriarOperacao<LogProcessamento, ProcessamentoHistorico>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaCargaLog = OperacaoFabrica.CriarOperacao<LogCarga, CargaLog>(repositorioModeloQC, repositorioModeloWM_DB);

            this.fabricaRepositorioCarga = RepositorioFabrica.CriarRepositorio<CargaDB, RepositorioCarga>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaRepositorioCarteira = RepositorioFabrica.CriarRepositorio<Carteira, RepositorioCarteira>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaRepositorioProcessamentoManual = RepositorioFabrica.CriarRepositorio<Processamento, RepositorioProcessamento>(repositorioModeloQC, repositorioModeloWM_DB);
            this.fabricaRepositorioProcessamentoResultado = RepositorioFabrica.CriarRepositorio<Processamento, RepositorioProcessamentoResultado>(repositorioModeloQC, repositorioModeloWM_DB);            
        }

        #region Processamento Resultado
        public ActionResult ProcessamentoResultado()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ProcessamentoResultadoGrid(FormCollection form, Processamento model)
        {
            if (form["acao"] != null && form["acao"].ToUpper().Equals("PESQUISAR"))
            {
                List<Processamento> lstRet = fabricaRepositorioProcessamentoResultado.SelecionarRegistro(model);                
                if (lstRet.Count() == 0)
                    lstRet.Add(new Processamento { Msg = "* Registro não encontrado." });

                return PartialView("../Processamento/_ProcessamentoGridPartial", lstRet);
            }
            else
            {
                return PartialView("../Processamento/_ProcessamentoGridPartial");
            }
        }

        [HttpGet]
        [ValidateInput(false)]        
        public ActionResult ProcessamentoResultadoDetalhe(Processamento model)
        {
            try
            {
                Processamento processamento = new Processamento();

                if (!string.IsNullOrEmpty(Request.QueryString["parametros"]))
                {
                    string[] parametro = Request.QueryString["parametros"].Split(',');
                
                    processamento.codSubTipoFiltro = Convert.ToInt32(parametro[0]);                    

                    processamento.dtResultado = DateTime.ParseExact(parametro[1], "dd/MM/yyyy", cultureData);

                    List<Processamento> lstRet = fabricaRepositorioProcessamentoResultado.SelecionarRegistroDetalhe(processamento);

                    if (lstRet.Count > 0)
                    {
                        lstRet[0].dtResultadoRet = processamento.dtResultado;
                        lstRet[0].codSubTipoFiltroRet = processamento.codSubTipoFiltro;
                    }

                    return PartialView("../Processamento/_ProcessamentoDetalhePartial", lstRet);
                }
                else if (!string.IsNullOrEmpty(Request["hdnLiberar"]) )                
                {
                    int retLiberar = 0;                    

                    string paramCabecalho = Request["hdnParametrosRet"];
                    string parametroLiberar = Request["hdnLiberar"];                             

                    if (!string.IsNullOrEmpty(parametroLiberar))
                        retLiberar = Liberar(parametroLiberar);                                        

                    List<Processamento> lstRet = new List<Processamento>();
                    if (retLiberar > 0)
                        lstRet.Add(new Processamento { Msg = "A" });

                    return PartialView("../Processamento/_ProcessamentoDetalhePartial", lstRet);
                }                
                else
                    return PartialView("../Processamento/_ProcessamentoDetalhePartial");                
            }
            catch (Exception ex)
            {
                model.Msg = string.Concat("* Erro: ", ex);

                return View(model);
            }
        }

        private int Liberar(string parametrosLiberar)
        {
            Processamento processamento = new Processamento();            

            //Update Liberar
            string[] parametroLiberarUpdate = parametrosLiberar.Split(';');

            List<Processamento> llstProcUpdate = new List<Processamento>();
            for (int i = 0; i < parametroLiberarUpdate.Length; i++)
            {
                string[] parametroUpdateInt = parametroLiberarUpdate[i].Split(',');

                Processamento processamentoUpd = new Processamento();

                processamentoUpd.codSubTipoFiltro = Convert.ToInt32(parametroUpdateInt[0]);
                processamentoUpd.dtResultado = DateTime.ParseExact(parametroUpdateInt[1], "dd/MM/yyyy", cultureData); 
                processamentoUpd.codAtivo = parametroUpdateInt[2];
                processamentoUpd.codTipoAtivo = parametroUpdateInt[3];
                processamentoUpd.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]);                 

                llstProcUpdate.Add(processamentoUpd);
            }            

            return fabricaProcessamentoResultado.Gravar(llstProcUpdate);
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ProcessamentoResultadoDetalheAtivo(Processamento model)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["parametros"]))
            {
                string[] parametro = Request.QueryString["parametros"].Split(',');

                Processamento processamento = new Processamento();
                processamento.codSubTipoFiltro = Convert.ToInt32(parametro[0]);
                processamento.dtResultado = DateTime.ParseExact(parametro[1], "dd/MM/yyyy", cultureData);

                if (processamento.codSubTipoFiltro.Equals(16))
                {
                    processamento.codAtivo = parametro[2];
                    processamento.codTipoAtivo = parametro[3];
                }

                List<Processamento> lstRet = fabricaRepositorioProcessamentoResultado.SelecionarRegistroDetalheItem(processamento);

                return PartialView("../Processamento/_ProcessamentoDetalheAtivoPartial", lstRet);
            }
            else
            {
                return PartialView("../Processamento/_ProcessamentoDetalheAtivoPartial");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProcessamentoResultadoDetalheCarteira(Processamento model)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request["hdnLiberar"]))
                {
                    string[] parametroSplit = Request["hdnLiberar"].Split(',');

                    Processamento processamento = new Processamento();
                    processamento.codSubTipoFiltro = Convert.ToInt32(parametroSplit[0]);
                    processamento.dtResultado = DateTime.ParseExact(parametroSplit[1], "dd/MM/yyyy", cultureData);
                    processamento.codCarteira = parametroSplit[2].ToString().Trim();
                    processamento.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]); 
                    processamento.inLiberado = "S";

                    //Libera as carteiras
                    int retLiberar = LiberarPorCarteira(processamento);

                    List<Processamento> lstRet = new List<Processamento>();
                    if (retLiberar > 0)
                        lstRet.Add(new Processamento { Msg = "A" });

                    return PartialView("../Processamento/_ProcessamentoDetalheAtivoPartial", lstRet);
                }
                else
                {                    
                    return PartialView("../Processamento/_ProcessamentoDetalheAtivoPartial");
                }
            }
            catch (Exception ex)
            {
                model.Msg = string.Concat("* Erro: ", ex);

                return View(model);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProcessamentResultadoLiberar()
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

            return View();
        }

        private int LiberarPorCarteira(Processamento processamentoLiberar)
        {
            List<Processamento> llstProcUpdate = new List<Processamento>() { processamentoLiberar };            

            return fabricaProcessamentoResultado.Alterar(llstProcUpdate);
        }
        #endregion Processamento Resultado

        #region Processamento Manual
        public ActionResult ProcessamentoManual()
        {            
            return View();
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProcessamentoManual(Processamento model, FormCollection form)
        {
            try
            {                
                if (form["acao"] != null)
                {
                    switch (form["acao"].ToUpper())
                    {
                        case "CARGA":
                            List<Processamento> lstCarga = new List<Processamento>();

                            model.codCarteira = form["chkProcessar"];
                            model.TempoEspera = Convert.ToInt32(GlobalConfig.TempoEsperaProcessamento);

                            lstCarga.Add(model);

                            fabricaProcessamentoManual.Gravar(lstCarga);

                            Session.Add("dtResultado", DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData));

                            model.dtResultado = Convert.ToDateTime(Session["dtResultado"]); 
                            //model.dtResultado = DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData);                            
                            model.acaoProcessamento = "Carga iniciada";

                            break;
                        case "CARGA_DETALHE":
                            model = ProcessamentoCargaDetalhe(model, form);

                            break;
                        case "PROCESSAR":
                            model = Processar(model, form);

                            break;
                        case "PROCESSAMENTO_DETALHE":
                            model = ProcessamentoProcessamentoDetalhe(model, form);

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

        private Processamento Processar(Processamento model, FormCollection form)
        {
            //Inserção do Log de Carga
            List<Processamento> lstLogCarga = new List<Processamento>();

            Processamento procLogCarga = new Processamento();
            procLogCarga.Acao = "Carga";
            procLogCarga.dtResultado = Convert.ToDateTime(Session["dtResultado"]);
            //procLogCarga.dtResultado = DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData);
            procLogCarga.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]);

            lstLogCarga.Add(procLogCarga);

            fabricaProcessamentoManual.GravarLog(lstLogCarga);
            //Fim Inserção do Log de Carga

            List<Processamento> lstProcessamento = new List<Processamento>();

            Processamento processamento = new Processamento();
            processamento.Acao = "Processamento";
            processamento.TempoEspera = Convert.ToInt32(GlobalConfig.TempoEsperaProcessamento);
            processamento.codCarteira = form["chkProcessar"];
            processamento.inTipoExecucao = "M";
            processamento.dtResultado = Convert.ToDateTime(Session["dtResultado"]);
            //processamento.dtResultado = DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData);
            processamento.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]);

            lstProcessamento.Add(processamento);

            fabricaProcessamentoManual.Processar(lstProcessamento);

            model.txtDescLogCargas = form["hdnDescLogCarga"];
            model.acaoProcessamento = "Processamento iniciado";

            //Inserção do Log de Processamento
            List<Processamento> lstLogProcessamento = new List<Processamento>();

            Processamento procLogProc = new Processamento();
            procLogProc.Acao = "Processamento";
            procLogProc.dtResultado = Convert.ToDateTime(Session["dtResultado"]);
            //procLogProc.dtResultado = DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData);
            procLogProc.codUsuario = Convert.ToInt32(User.Identity.Name.Split('|')[0]);

            lstLogProcessamento.Add(procLogProc);

            fabricaProcessamentoManual.GravarLog(lstLogProcessamento);
            //Fim Inserção do Log de Processamento

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

                    model.dtResultado = Convert.ToDateTime(Session["dtResultado"]);
                    //model.dtResultado = DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData);
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

        private Processamento ProcessamentoProcessamentoDetalhe(Processamento model, FormCollection form)
        {            
            model.acaoProcessamento = "Processamento iniciado";   
                        
            if (string.IsNullOrEmpty(form["hdnCodProcessamento"]) || form["hdnCodProcessamento"].Equals("0"))
            {
                var lstProcessamento = fabricaRepositorioProcessamentoManual.SelecionarTudo();
                if (lstProcessamento.Count == 0)
                    model.acaoProcessamento = "Processamento concluído";
                else
                    model.codProcessamento = lstProcessamento[0].codProcessamento;
            }
            else
                model.codProcessamento = Convert.ToInt32(form["hdnCodProcessamento"]);            

            if (model.codProcessamento > 0)
            {                
                LogProcessamento logProcessamento = new LogProcessamento();
                logProcessamento.codProcessamento = model.codProcessamento;                
                logProcessamento.TempoEspera = Convert.ToInt32(GlobalConfig.TempoEsperaLog);

                if (!string.IsNullOrEmpty(form["hdnCarteira"]))
                    logProcessamento.codCarteira = string.Concat(form["hdnCarteira"]);                

                if (!string.IsNullOrEmpty(form["hdnCarteirasProcessamento"]))
                    logProcessamento.codCarteiras = string.Concat(form["hdnCarteirasProcessamento"]);

                var retProcDetalhe = fabricaProcessamentoLog.SelecionarLogProcessamentoEmEspera(logProcessamento);                

                if (retProcDetalhe.Count > 0 && string.IsNullOrEmpty(form["hdnCarteirasProcessamento"]))
                    model.codCarteiras = retProcDetalhe[0].codCarteira;
                else
                {
                    if (retProcDetalhe.Count > 0)
                        model.codCarteiras = string.Concat(form["hdnCarteirasProcessamento"], "|", retProcDetalhe[0].codCarteira);
                    else
                    {
                        model.codCarteiras = form["hdnCarteirasProcessamento"];

                        model.acaoProcessamento = "Processamento concluído";
                    }
                }

                if (retProcDetalhe.Count > 0)
                    model.codCarteira = retProcDetalhe[0].codCarteira;

                model.dtResultado = Convert.ToDateTime(Session["dtResultado"]);
                //model.dtResultado = DateTime.ParseExact(form["dtResultado"], "d/M/yyyy", cultureData);                            
            }

            model.txtDescLogCargas = form["hdnDescLogCarga"];

            return model;
        }
        #endregion Processamento Manual
    }
}
