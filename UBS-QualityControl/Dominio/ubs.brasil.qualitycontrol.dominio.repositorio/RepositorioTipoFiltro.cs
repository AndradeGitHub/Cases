using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioTipoFiltro : Repositorio<TipoDeFiltro>
    {        
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioTipoFiltro(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;
        }      

        public override List<TipoDeFiltro> SelecionarTudo()
        {            
            var resQC = from tf in dbQC.TipoFiltro
                        where
                          tf.inAtivoInativo == "A"                          
                        orderby tf.codTipoFiltro
                        select tf;            

            return resQC.ToList<TipoDeFiltro>();     
        }  
    }
}
