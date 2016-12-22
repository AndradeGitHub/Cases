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
    [Table("LOG_CARGA")]
    public class LogCargaDB : Entidade    
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codLog { get; set; }
        public int codCarga { get; set; }
        public DateTime dtCarga { get; set; }      
        public string codCarteira { get; set; }
        public int codOrdem { get; set; }
        public string dsDescricao { get; set; }
        public DateTime dtLog { get; set; }
        public int codUsuarioResponsavel { get; set; }
        public string inTipoMensagem { get; set; }        
    }
}