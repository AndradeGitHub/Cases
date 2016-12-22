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
    [Table("LOG_PROCESSAMENTO")]
    public class LogProcessamentoDB
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codLog { get; set; }
        public DateTime dtProcessada { get; set; }
        public string codCarteira { get; set; }
        public int? codTipoFiltro { get; set; }
        public int? codSubTipoFiltro { get; set; }
        public string dsDescricao { get; set; }
        public DateTime dtProcessamento { get; set; }
        public int codUsuarioResponsavel { get; set; }
        public int codProcessamento { get; set; }
        public string inTipoMensagem { get; set; }
    }
}