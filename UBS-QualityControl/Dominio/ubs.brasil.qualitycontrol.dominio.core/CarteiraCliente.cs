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
    public class CarteiraCliente : Operacao<Carteira>   
    {
        private static dynamic repositorioFabrica;

        public CarteiraCliente(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            //RepositorioFabrica.repositorioModeloQC = repositorioModeloQC;
            //RepositorioFabrica.repositorioModeloWM_DB = repositorioModeloWM_DB;
            
            //repositorioFabrica = RepositorioFabrica.CriarRepositorio<Carteira, RepositorioCarteira>();            
        }

        //public override List<Carteira> SelecionarTudo()
        //{
        //    return repositorioFabrica.SelecionarTudo();
        //}        
    }
}
