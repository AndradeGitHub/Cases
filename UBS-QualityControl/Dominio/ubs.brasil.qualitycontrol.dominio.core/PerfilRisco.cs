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
    public class PerfilRisco : Operacao<PerfilDeRisco>
    {
        private static dynamic repositorioFabrica;

        public PerfilRisco(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            //RepositorioFabrica.repositorioModeloQC = repositorioModeloQC;
            //RepositorioFabrica.repositorioModeloWM_DB = repositorioModeloWM_DB;

            //repositorioFabrica = RepositorioFabrica.CriarRepositorio<PerfilDeRisco, RepositorioPerfilRisco>();                               
        }

        //public override List<PerfilDeRisco> SelecionarTudo()
        //{
        //    return repositorioFabrica.SelecionarTudo();
        //}        
    }
}
