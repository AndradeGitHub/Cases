using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.core
{
    public class ProcessamentoHistorico : Operacao<LogProcessamento>
    {        
        private static dynamic fabricaProcessamento;
        private static dynamic repositorioFabrica;
        private static dynamic repositorioFabricaProcessamento;

        public ProcessamentoHistorico(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            fabricaProcessamento = OperacaoFabrica.CriarOperacao<Processamento, ProcessamentoManual>(repositorioModeloQC, repositorioModeloWM_DB);  

            repositorioFabrica = RepositorioFabrica.CriarRepositorio<LogProcessamento, RepositorioProcessamentoHistorico>(repositorioModeloQC, repositorioModeloWM_DB);
            repositorioFabricaProcessamento = RepositorioFabrica.CriarRepositorio<Processamento, RepositorioProcessamento>(repositorioModeloQC, repositorioModeloWM_DB);                                                                   
        }

        public override List<LogProcessamento> SelecionarLogProcessamentoEmEspera(LogProcessamento logProcessamento)
        {
            List<LogProcessamento> lstLogProcessamento = new List<LogProcessamento>();

            for (int i = 0; i < 100; i++)
            {
                lstLogProcessamento = repositorioFabrica.SelecionarRegistroDetalhe(logProcessamento);
                
                if (lstLogProcessamento.Count > 0)
                    break;
                else
                {
                    Processamento processamento = new Processamento();
                    processamento.codProcessamento = logProcessamento.codProcessamento;

                    List<Processamento> lstProcessamento = repositorioFabricaProcessamento.SelecionarRegistro(processamento);
                    if (lstProcessamento.Count > 0)
                        break;
                    else
                    {
                        Thread.Sleep(logProcessamento.TempoEspera);
                    }
                }
            }

            return lstLogProcessamento;
        }
    }
}