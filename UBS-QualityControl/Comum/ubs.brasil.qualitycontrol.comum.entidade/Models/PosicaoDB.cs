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
    [Table("POSICAO")]
    public class PosicaoDB : Entidade
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public DateTime dtPosicao { get; set; }
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string codCarteira { get; set; }
        
        public string codAtivo { get; set; }
        public string codTipoAtivo { get; set; }        
        public DateTime dtAquisicao { get; set; } 
        public double puAquisicao { get; set; }        
        public double vlrSaldoBruto { get; set; }                        
        public double puAtual { get; set; } 
        public double vlrIRRF { get; set; } 
        public double vlrIOF { get; set; } 
        public double qtDisp { get; set; } 
        public double qtBloqueada { get; set; } 
        public double vlrSaldoLiquido { get; set; } 
        public DateTime dtLiquidacao { get; set; } 
        public string descHistorico { get; set; } 
        public string codYMFLegado { get; set; }         
        public double vlrTaxa { get; set; } 
        public string codInterno { get; set; } 
        public string codClearing { get; set; } 
        public double vlrAtivo { get; set; } 
        public double vlrPassivo { get; set; } 
    }
}