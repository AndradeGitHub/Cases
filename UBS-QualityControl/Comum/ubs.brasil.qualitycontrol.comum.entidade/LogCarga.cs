using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace ubs.brasil.qualitycontrol.comum.entidade
{    
    public class LogCarga : Entidade
    {
        public int codCarga { get; set; }
        public int codOrdem { get; set; }
        public string dsDescricao { get; set; }
    }
}
