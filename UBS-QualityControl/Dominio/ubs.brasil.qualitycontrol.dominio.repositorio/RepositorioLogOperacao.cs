using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioLogOperacao : Repositorio<LogOperacao>
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioLogOperacao(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;    
        }

        public override int Gravar(List<LogOperacao> logOperacao)
        {            
            logOperacao.ForEach(p => dbWM_DB.Entry(p).State = EntityState.Added);

            return dbWM_DB.SaveChanges();  
        }

        public override List<LogOperacao> SelecionarRegistro(LogOperacao logOperacao)
        {
            var resWM_DB = from log in dbWM_DB.LogOperacao
                           where
                             (log.dtLogOperacao >= logOperacao.dataInicial && log.dtLogOperacao <= logOperacao.dataFinal) &&
                             log.nomeSistema.Equals("Q.C.")
                           select log;

            if (!string.IsNullOrEmpty(logOperacao.nomeFuncionalidade))
                resWM_DB = resWM_DB.Where(w => w.nomeFuncionalidade.Equals(logOperacao.nomeFuncionalidade));
            if (!string.IsNullOrEmpty(logOperacao.acao))
                resWM_DB = resWM_DB.Where(w => w.acao.Equals(logOperacao.acao));
            if (!string.IsNullOrEmpty(logOperacao.nomeTipoDescricao))
                resWM_DB = resWM_DB.Where(w => w.nomeTipoDescricao.Equals(logOperacao.nomeTipoDescricao));


            resWM_DB = resWM_DB.OrderByDescending(o => o.dtLogOperacao);

            return resWM_DB.ToList<LogOperacao>(); 
        }
    }
}