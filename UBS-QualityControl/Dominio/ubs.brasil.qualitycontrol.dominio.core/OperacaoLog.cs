using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.core.interfaces;
using ubs.brasil.qualitycontrol.dominio.repositorio;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.core
{
    public class OperacaoLog : IOperacaoLog
    {
        private static dynamic repositorioFabrica;

        public OperacaoLog(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            repositorioFabrica = RepositorioFabrica.CriarRepositorio<LogOperacao, RepositorioLogOperacao>(repositorioModeloQC, repositorioModeloWM_DB);                                                                   
        }

        public int Gravar(List<LogOperacao> lstLogOperacao)
        {
            lstLogOperacao.ForEach(item => item.nomeSistema = "Q.C.");            

            return repositorioFabrica.Gravar(lstLogOperacao);
        }

        public List<LogOperacao> SelecionarRegistro(LogOperacao logOperacao)
        {
            return repositorioFabrica.SelecionarRegistro(logOperacao);
        }
    }
}
