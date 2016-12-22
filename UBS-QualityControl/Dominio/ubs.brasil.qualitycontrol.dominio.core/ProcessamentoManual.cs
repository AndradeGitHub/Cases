using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.core
{
    public class ProcessamentoManual : Operacao<Processamento>
    {
        private static dynamic repositorioFabrica;        
        private static dynamic fabricaLogOperacao; 

        public ProcessamentoManual(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            fabricaLogOperacao = OperacaoFabrica.CriarOperacaoLog<OperacaoLog>(repositorioModeloQC, repositorioModeloWM_DB);  
           
            repositorioFabrica = RepositorioFabrica.CriarRepositorio<Processamento, RepositorioProcessamento>(repositorioModeloQC, repositorioModeloWM_DB);                                                                       
        }

        public override int Gravar(List<Processamento> processamento)
        {             
            Action<object> actionCarga = (object obj) =>
            {
                repositorioFabrica.Gravar(processamento);
            };

            Task taskCarga = Task.Factory.StartNew(actionCarga, "taskCarga");
            taskCarga.Wait(processamento[0].TempoEspera);  
                    
            return 1;
        }

        public override int Processar(List<Processamento> processamento)
        {            
            Action<object> actionProc = (object obj) =>
            {                
                repositorioFabrica.Processar(processamento);                
            };
            
            Task taskProc = Task.Factory.StartNew(actionProc, "taskProcessamento");
            taskProc.Wait(processamento[0].TempoEspera);                        

            return 1;
        }

        public override int GravarLog(List<Processamento> processamento)
        {
            List<LogOperacao> lstLogOperacao = new List<LogOperacao>();

            foreach (Processamento proc in processamento)
            {
                LogOperacao logOperacao = new LogOperacao();

                logOperacao.nomeTipoFuncionalidade = string.Concat(proc.Acao, " Manual");
                logOperacao.nomeFuncionalidade = string.Concat(proc.Acao, " Manual");
                logOperacao.acao = "Execução";
                logOperacao.dtLogOperacao = DateTime.Now;
                logOperacao.codUsuario = proc.codUsuario;
                logOperacao.nomeTipoDescricao = "OK";

                if (proc.Acao.Equals("Processamento"))
                    logOperacao.txDescricao = string.Concat("Período Processamento: Diário", ", Tipo Execucao: Manual", ", Data: ", proc.dtResultado.ToString("dd/MM/yyyy"));
                else if (proc.Acao.Equals("Carga"))
                    logOperacao.txDescricao = string.Concat("Carga", ", Tipo Execucao: Manual", ", Data: ", proc.dtResultado.ToString("dd/MM/yyyy"));

                lstLogOperacao.Add(logOperacao);
            }

            return fabricaLogOperacao.Gravar(lstLogOperacao);            
        }
    }
}
