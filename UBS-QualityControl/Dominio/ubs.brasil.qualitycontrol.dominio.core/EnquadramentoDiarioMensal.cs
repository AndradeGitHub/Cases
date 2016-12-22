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
    public class EnquadramentoDiarioMensal : Operacao<Enquadramento>
    {
        private static dynamic repositorioFabrica;
        private static dynamic fabricaLogOperacao;         

        public EnquadramentoDiarioMensal(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            fabricaLogOperacao = OperacaoFabrica.CriarOperacaoLog<OperacaoLog>(repositorioModeloQC, repositorioModeloWM_DB);   

            repositorioFabrica = RepositorioFabrica.CriarRepositorio<Enquadramento, RepositorioEnquadramento>(repositorioModeloQC, repositorioModeloWM_DB);                                           
        }

        public override int Alterar(List<Enquadramento> lstEnquadramento)
        {
            int ret = repositorioFabrica.Alterar(lstEnquadramento);

            if (ret > 0)
                GravarLogOperacao(lstEnquadramento, "Liberar");

            return ret;
        }

        private void GravarLogOperacao(List<Enquadramento> lstEnquadramento, string acao)
        {
            List<LogOperacao> lstLogOperacao = new List<LogOperacao>();

            foreach (Enquadramento enquadramento in lstEnquadramento)
            {
                LogOperacao logOperacao = new LogOperacao();

                logOperacao.nomeTipoFuncionalidade = acao;
                logOperacao.nomeFuncionalidade = acao;
                logOperacao.acao = acao;
                logOperacao.dtLogOperacao = DateTime.Now;
                logOperacao.codUsuario = enquadramento.codUsuario;
                logOperacao.nomeTipoDescricao = "OK";

                logOperacao.txDescricao = string.Concat("Código Carteira: ", enquadramento.codCarteira,
                                                 ", Código Processamento: ", enquadramento.codProcessamento,
                                                 ", Sub Tipos Filtro: ", enquadramento.codListaSubTipo);
                                                 //", Data Resultado: ", enquadramento.dtResultado);

                lstLogOperacao.Add(logOperacao);
            }

            fabricaLogOperacao.Gravar(lstLogOperacao);
        }    
    }
}
