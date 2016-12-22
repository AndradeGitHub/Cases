using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioPerfilRisco : Repositorio<PerfilDeRisco>    
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioPerfilRisco(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;            
        }

        public override List<PerfilDeRisco> SelecionarTudo()
        {
            var resWM = from pr in dbWM_DB.PerfilRisco
                        where pr.inAtivoInativo == "A"
                        orderby pr.nomePerfilRisco
                        select pr;            

            return resWM.ToList<PerfilDeRisco>();
        }
    }
}
