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
    [Table("CARTEIRA_COTA")]
    public class CarteiraCota : Entidade
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public string codCarteira { get; set; }
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime dtCota { get; set; }        
        public double? qtCota { get; set; }        
        public double? vlCota { get; set; }   
        public double? vlPatrimonio { get; set; }        
    }
}