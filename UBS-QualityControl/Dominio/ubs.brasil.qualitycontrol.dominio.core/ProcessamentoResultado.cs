using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ubs.brasil.qualitycontrol.comum.globals;
using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.core
{
    public class ProcessamentoResultado : Operacao<Processamento>
    {
        private static dynamic repositorioFabrica;
        private static dynamic fabricaLogOperacao;         

        public ProcessamentoResultado(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            fabricaLogOperacao = OperacaoFabrica.CriarOperacaoLog<OperacaoLog>(repositorioModeloQC, repositorioModeloWM_DB);   
            
            repositorioFabrica = RepositorioFabrica.CriarRepositorio<Processamento, RepositorioProcessamentoResultado>(repositorioModeloQC, repositorioModeloWM_DB);                                                                       
        }

        public override int Gravar(List<Processamento> processamento)
        {
            int ret = repositorioFabrica.Gravar(processamento);

            if (ret.Equals(-1))
                GravarLogOperacao(processamento, "Liberar");

            return ret;
        }

        public override int Alterar(List<Processamento> processamento)
        {
            int ret = repositorioFabrica.Alterar(processamento);

            if (ret > 0)
                GravarLogOperacao(processamento, "Liberar");

            return ret;
        }

        private void GravarLogOperacao(List<Processamento> lstProcessamento, string acao)
        {
            List<LogOperacao> lstLogOperacao = new List<LogOperacao>();

            foreach (Processamento proc in lstProcessamento)
            {
                LogOperacao logOperacao = new LogOperacao();

                string codSubTipoFiltro = string.Empty;
                if (proc.codSubTipoFiltro != null)
                    codSubTipoFiltro = Utils.GetEnumDescriptionTipoFiltro((int)proc.codSubTipoFiltro);
                
                logOperacao.nomeTipoFuncionalidade = acao;
                logOperacao.nomeFuncionalidade = acao;
                logOperacao.acao = acao;
                logOperacao.dtLogOperacao = DateTime.Now;
                logOperacao.codUsuario = proc.codUsuario;
                logOperacao.nomeTipoDescricao = "OK";

                if (proc.codAtivo == null)
                {
                    logOperacao.txDescricao = string.Concat("Sub Tipo Filtro: ", codSubTipoFiltro,
                                                            ", Data Resultado: ", proc.dtResultado);
                }
                else
                {
                    logOperacao.txDescricao = string.Concat("Código Ativo: ", proc.codAtivo,
                                                            ", Código Tipo Ativo: ", proc.codTipoAtivo,
                                                            ", Sub Tipo Filtro: ", codSubTipoFiltro,
                                                            ", Data Resultado: ", proc.dtResultado);
                }

                lstLogOperacao.Add(logOperacao);
            }

            fabricaLogOperacao.Gravar(lstLogOperacao);
        }                
    }
}
