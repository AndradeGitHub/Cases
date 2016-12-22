using System;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Collections.Generic;
using System.Globalization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ubs.brasil.qualitycontrol.comum.entidade
{
    public abstract class Entidade
    {
        [NotMapped] public string Acao { get; set; }
        [NotMapped] public string Msg { get; set; }
        [NotMapped] public string QtdeRegistro { get; set; }
        [NotMapped] public string Order { get; set; }
        [NotMapped] public DateTime DtPesq { get; set; }
        [NotMapped] public int TempoEspera { get; set; }
    }
}
