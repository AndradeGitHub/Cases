using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

using ubs.brasil.qualitycontrol.comum.entidade;
using ubs.brasil.qualitycontrol.dominio.repositorio.interfaces;

namespace ubs.brasil.qualitycontrol.dominio.repositorio
{
    public class RepositorioCarga : Repositorio<CargaDB>
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;

        public RepositorioCarga(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;
        }

        public override List<CargaDB> SelecionarTudo()
        {                     
            var lstCarga = (from carga in dbWM_DB.Carga
                               where
                                carga.dtExecucaoIni.Year.Equals(DateTime.Now.Year) && carga.dtExecucaoIni.Month.Equals(DateTime.Now.Month) && carga.dtExecucaoIni.Day.Equals(DateTime.Now.Day) &&
                                carga.inTipoProcessamento.ToUpper().Equals("M") 
                                //carga.inFinalizado.ToUpper().Equals("N") 
                               orderby carga.dtExecucaoIni descending
                               select carga).Take(1).ToList<CargaDB>();

            return lstCarga;
        }
    }
}