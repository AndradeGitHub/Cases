using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ubs.brasil.qualitycontrol.client.webapp
{
    public static class GlobalConfig
    {
        public static readonly string CultureData = ConfigurationManager.AppSettings["CultureData"];

        public static readonly string CultureValLimite = ConfigurationManager.AppSettings["CultureValLimite"];  
  
        public static readonly string Ambiente = ConfigurationManager.AppSettings["Ambiente"];

        public static readonly string TempoEsperaProcessamento = ConfigurationManager.AppSettings["TempoEsperaProcessamento"];

        public static readonly string TempoEsperaLog = ConfigurationManager.AppSettings["TempoEsperaLog"];  
    }
}
