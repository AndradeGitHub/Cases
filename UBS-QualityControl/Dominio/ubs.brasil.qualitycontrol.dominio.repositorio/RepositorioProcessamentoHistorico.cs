using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Transactions;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioProcessamentoHistorico : Repositorio<LogProcessamento>
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioProcessamentoHistorico(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;
        }

        public override List<LogProcessamento> SelecionarRegistro(LogProcessamento processamento)
        {
            var lstLogProc = (from log in dbQC.LogProcessamento
                              from tf in dbQC.TipoFiltro.Where(w => w.codTipoFiltro.Equals((int)log.codTipoFiltro)).DefaultIfEmpty() //Left Join                                                                                              
                              from tf1 in dbQC.TipoFiltro.Where(w => w.codTipoFiltro.Equals((int)log.codSubTipoFiltro)).DefaultIfEmpty() //Left Join                                                                                              
                              where
                                log.dtProcessada.Equals((DateTime)processamento.dtProcessada)
                              select new LogProcessamento
                              {
                                  codCarteira = log.codCarteira,
                                  dtProcessada = log.dtProcessada,
                                  dtProcessamento = log.dtProcessamento,
                                  dsDescricao = string.Concat(
                                                              (tf == null ? string.Empty : string.Concat(tf.nomeTipoFiltro, " - ")),
                                                              (tf1 == null ? string.Empty : string.Concat(tf1.nomeTipoFiltro, " - ")),
                                                              log.dsDescricao
                                                             ),                                  
                                  codUsuarioResponsavel = log.codUsuarioResponsavel
                              }
                             ).OrderBy(o => o.codCarteira).ThenByDescending(o => o.dtProcessada).ThenByDescending(o => o.dtProcessamento)
                              .ToList<LogProcessamento>();                             

            if (processamento.codCarteira != null)                            
                lstLogProc = lstLogProc.Where(w => processamento.codCarteira.Contains(w.codCarteira)).ToList<LogProcessamento>();                        

            return lstLogProc;
        }

        public override List<LogProcessamento> SelecionarRegistroDetalhe(LogProcessamento logProcessamento)
        {
            var lstLogProcessamento = new List<LogProcessamento>();            

            //NOLOCK
            using (var tx = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                if (string.IsNullOrEmpty(logProcessamento.codCarteira))
                {
                    lstLogProcessamento = (from log in dbQC.LogProcessamento
                                           where
                                            log.codProcessamento.Equals(logProcessamento.codProcessamento)
                                           select new LogProcessamento
                                           {
                                               codProcessamento = log.codProcessamento,
                                               codCarteira = log.codCarteira,
                                               dtProcessamento = log.dtProcessamento
                                           })
                                           .OrderBy(o => o.dtProcessamento).ThenBy(o => o.codCarteira)
                                           .Distinct<LogProcessamento>()
                                           .Take(1).ToList<LogProcessamento>();
                }
                else
                {
                    string[] carteirasSplit = logProcessamento.codCarteiras.Split('|');

                    lstLogProcessamento = (from log in dbQC.LogProcessamento
                                           where
                                            log.codProcessamento.Equals(logProcessamento.codProcessamento) &&
                                            !carteirasSplit.Contains(log.codCarteira)
                                           select new LogProcessamento
                                           {
                                               codProcessamento = log.codProcessamento,
                                               codCarteira = log.codCarteira,
                                               dtProcessamento = log.dtProcessamento
                                           })                    
                                           .OrderBy(o => o.dtProcessamento).ThenBy(o => o.codCarteira)
                                           .Distinct<LogProcessamento>()
                                           .Take(1).ToList<LogProcessamento>();
                }                
            }

            return lstLogProcessamento;
        }
    }
}
