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
    [Table("ATIVO")]
    public class Ativo : Entidade
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public string codAtivo { get; set; }
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string codTipoAtivo { get; set; }        
        public string nomeAtivo { get; set; }        
        public int? codClasseAtivo { get; set; }   
        public string codIsin { get; set; }        
    }
}