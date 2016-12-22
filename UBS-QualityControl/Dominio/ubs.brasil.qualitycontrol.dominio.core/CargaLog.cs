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
    public class CargaLog : Operacao<LogCarga>
    {
        private static dynamic repositorioFabrica;

        public CargaLog(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            repositorioFabrica = RepositorioFabrica.CriarRepositorio<LogCarga, RepositorioLogCarga>(repositorioModeloQC, repositorioModeloWM_DB);                               
        }

        public override List<LogCarga> SelecionarLogCargaEmEspera(LogCarga logCarga)
        {
            List<LogCarga> lstLogCarga = new List<LogCarga>();

            for (int i = 0; i < 100; i++)
            {                    
                lstLogCarga = repositorioFabrica.SelecionarRegistro(logCarga);

                if (lstLogCarga.Count.Equals(0))
                    Thread.Sleep(logCarga.TempoEspera);
                else
                    break;
            }            

            return lstLogCarga;
        }
    }
}
