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
    public class RepositorioLogCarga : Repositorio<LogCarga>
    {
        private readonly RepositorioModeloQC dbQC;
        private readonly RepositorioModeloWM_DB dbWM_DB;        

        public RepositorioLogCarga(RepositorioModeloQC repositorioModeloQC, RepositorioModeloWM_DB repositorioModeloWM_DB)
        {
            this.dbQC = repositorioModeloQC;
            this.dbWM_DB = repositorioModeloWM_DB;
        }

        public override List<LogCarga> SelecionarRegistro(LogCarga logCarga)
        {
            var lstLogCarga = (from log in dbWM_DB.LogCarga
                               where
                                log.codCarga.Equals(logCarga.codCarga) &&
                                log.codOrdem.Equals(logCarga.codOrdem)
                               select new LogCarga
                               {
                                   codCarga = log.codCarga,
                                   codOrdem = log.codOrdem,
                                   dsDescricao = log.dsDescricao
                               }).Take(1).ToList<LogCarga>();            

            return lstLogCarga;
        }
    }
}