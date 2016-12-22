using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioCarteira : Repositorio<Carteira>    
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioCarteira(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;
        }

        public override List<Carteira> SelecionarTudo()
        {
            DateTime diaAtual = DateTime.Now;
            DateTime diaMenos30 = diaAtual.AddDays(-30);
            DateTime diaMais30 = diaAtual.AddDays(30);

            var resWM_DB = from c in dbWM_DB.Carteira
                           where
                            c.inAtivoInativo.ToUpper().Equals("A") ||
                            ((DateTime)c.dtEncerramento >= diaMenos30 && (DateTime)c.dtEncerramento <= diaMais30)
                           select c;            

            return resWM_DB.ToList<Carteira>();
        }
    }
}
