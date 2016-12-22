using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace ubs.brasil.qualitycontrol.comum.entidade
{    
    public class LogProcessamento : Entidade
    {                        
        public string codCarteira { get; set; }
        public string codCarteiras { get; set; } 
        public DateTime dtProcessada { get; set; }        
        public DateTime dtProcessamento { get; set; }
        public string nomeTipoFiltro { get; set; }
        public string nomeTipoFiltro1 { get; set; }
        public string dsDescricao { get; set; }        
        public int codUsuarioResponsavel { get; set; }
        public int codProcessamento { get; set; }
    }
}