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
    [Table("RESULTADO_ENQUADRAMENTO")]
    public class EnquadramentoDB : Entidade
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public int codResultado { get; set; }
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime dtResultado { get; set; }
        [Key, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string codCarteira { get; set; }
        [Key, Column(Order = 3), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codTipoFiltro { get; set; }
        [Key, Column(Order = 4), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codSubTipoFiltro { get; set; }
        public string codAtivo { get; set; }
        public string codTipoAtivo { get; set; }
        public string inEnquadrado { get; set; }
        public double? vlrPontaA { get; set; }
        public double? vlrPontaB { get; set; }
        public DateTime? dtAberturaCart { get; set; }          
        public DateTime? dtEncerramentoCart { get; set; }            
        public DateTime? dtAlteracao { get; set; }
        public string txDescricao { get; set; }
        public int? codUsuarioAlteracao { get; set; }
        public int? codProcessamento { get; set; }
        public string inLiberado { get; set; }
        public DateTime? dtLiberacao { get; set; }
        public DateTime? dtPosicao { get; set; }
    }
}