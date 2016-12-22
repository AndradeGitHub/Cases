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
    public class CargaGeral : Operacao<CargaDB>
    {
        private static dynamic repositorioFabrica;

        public CargaGeral(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            //RepositorioFabrica.repositorioModeloQC = repositorioModeloQC;
            //RepositorioFabrica.repositorioModeloWM_DB = repositorioModeloWM_DB;

            //repositorioFabrica = RepositorioFabrica.CriarRepositorio<CargaDB, RepositorioCarga>();                               
        }

        //public override List<CargaDB> SelecionarTudo()
        //{    
        //    return repositorioFabrica.SelecionarTudo();
        //}
    }
}
