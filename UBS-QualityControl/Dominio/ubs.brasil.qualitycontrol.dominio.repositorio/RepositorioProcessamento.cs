using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Transactions;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioProcessamento : Repositorio<Processamento> 
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        private static dynamic repositorioLogOperacao; 

        private CultureInfo cultureBR = new CultureInfo("pt-BR");

        public RepositorioProcessamento(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;

            repositorioLogOperacao = RepositorioFabrica.CriarRepositorio<LogOperacao, RepositorioLogOperacao>(repositorioModeloQC, repositorioModeloWM_DB);                         
        }

        public override int Gravar(List<Processamento> processamento)
        {            
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                SqlParameter dtInicio = new SqlParameter("@DT_INI", SqlDbType.VarChar, 10);

                if (processamento[0].Acao.Equals("CargaSimples"))                                    
                    dtInicio.Value = processamento[0].dtResultado.ToString("d/M/yyyy");                
                else
                    dtInicio.Value = DBNull.Value; 

                parameters[0] = dtInicio;

                SqlParameter codCarteira;

                if (processamento[0].codCarteira == null)
                    codCarteira = new SqlParameter("@LISTA_CD_CARTEIRA", DBNull.Value);
                else
                    codCarteira = new SqlParameter("@LISTA_CD_CARTEIRA", processamento[0].codCarteira);                                    
            
                parameters[1] = codCarteira;

                SqlParameter inAutManual = new SqlParameter("@in_aut_manual", "M");
                parameters[2] = inAutManual;

                SqlParameter codUsuario = new SqlParameter("@cd_usuario", DBNull.Value);
                parameters[3] = codUsuario;

                dbWM_DB.Database.ExecuteSqlCommand("exec SP_WMDB_CARGA_GERAL @DT_INI, @LISTA_CD_CARTEIRA, @in_aut_manual, @cd_usuario", parameters);
            }
            catch (Exception ex)
            {
                List<LogOperacao> lstLogOperacao = new List<LogOperacao>();

                LogOperacao logOperacao = new LogOperacao();

                logOperacao.nomeSistema = "Q.C.";
                logOperacao.nomeTipoFuncionalidade = "Carga Manual";
                logOperacao.nomeFuncionalidade = "Carga Manual";
                logOperacao.acao = "Execução";
                logOperacao.codUsuario = processamento[0].codUsuario;
                logOperacao.dtLogOperacao = DateTime.Now;                
                logOperacao.nomeTipoDescricao = "ERRO";
                logOperacao.txDescricao = string.Concat("Carga", ", Tipo Execucao: Manual", ", Data: ", processamento[0].dtResultado.ToString("dd/MM/yyyy"), ", Erro: ", ex.Message);

                lstLogOperacao.Add(logOperacao);

                repositorioLogOperacao.Gravar(lstLogOperacao);                   
            }

            return 1;
        }

        public override int Processar(List<Processamento> processamento)
        {            
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];

                SqlParameter codUsuario = new SqlParameter("@CD_USUARIO", processamento[0].codUsuario);
                parameters[0] = codUsuario;

                SqlParameter dtReferencia = new SqlParameter("@DT_REF", SqlDbType.VarChar, 10);                
                dtReferencia.Value = processamento[0].dtResultado.ToString("d/M/yyyy");            
                parameters[1] = dtReferencia;

                SqlParameter codCarteira = new SqlParameter("@LISTA_CD_CARTEIRA", processamento[0].codCarteira);
                parameters[2] = codCarteira;

                SqlParameter inTipoExecucao = new SqlParameter("@IN_TIPO_EXECUCAO", processamento[0].inTipoExecucao);
                parameters[3] = inTipoExecucao;

                dbQC.Database.ExecuteSqlCommand("exec SP_PROCESSAMENTO_FILTROS @CD_USUARIO, @DT_REF, @LISTA_CD_CARTEIRA, @IN_TIPO_EXECUCAO", parameters);
            }
            catch (Exception ex)
            {
                List<LogOperacao> lstLogOperacao = new List<LogOperacao>();

                LogOperacao logOperacao = new LogOperacao();

                logOperacao.nomeSistema = "Q.C.";
                logOperacao.nomeTipoFuncionalidade = "Processamento Manual";
                logOperacao.nomeFuncionalidade = "Processamento Manual";
                logOperacao.acao = "Execução";
                logOperacao.codUsuario = processamento[0].codUsuario;
                logOperacao.dtLogOperacao = DateTime.Now;
                logOperacao.nomeTipoDescricao = "ERRO";
                logOperacao.txDescricao = string.Concat("Período Processamento: Diário", ", Tipo Execucao: Manual", ", Data: ", processamento[0].dtResultado.ToString("dd/MM/yyyy"), ", Erro: ", ex.Message);

                lstLogOperacao.Add(logOperacao);

                repositorioLogOperacao.Gravar(lstLogOperacao);
            }

            return 1;
        }
         
        public override List<Processamento> SelecionarRegistro(Processamento processamento)
        {
            List<Processamento> lstProcessamento = new List<Processamento>();

            //NOLOCK
            using (var tx = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {

                lstProcessamento = (from proc in dbQC.ProcessamentoDB
                                    where
                                     proc.codProcessamento.Equals(processamento.codProcessamento) &&
                                     proc.inFinalizado.Equals("S")
                                    orderby proc.dtExecucaoIni descending
                                    select new Processamento
                                    {
                                        codProcessamento = proc.codProcessamento
                                    }).ToList<Processamento>();
            }

            return lstProcessamento;
        }

        public override List<Processamento> SelecionarTudo()
        {
            var lstProcessamento = (from proc in dbQC.ProcessamentoDB
                                    where
                                     proc.dtExecucaoIni.Year.Equals(DateTime.Now.Year) && proc.dtExecucaoIni.Month.Equals(DateTime.Now.Month) && proc.dtExecucaoIni.Day.Equals(DateTime.Now.Day) &&
                                     proc.inTipoProcessamento.ToUpper().Equals("M") 
                                     //proc.inFinalizado.ToUpper().Equals("N")
                                    orderby proc.dtExecucaoIni descending
                                    select new Processamento
                                    {
                                        codProcessamento = proc.codProcessamento
                                    }).Take(1).ToList<Processamento>();                                    

            return lstProcessamento;
        }
    }
}