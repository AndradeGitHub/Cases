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
    [Table("LOG_OPERACAO")]
    public class LogOperacao : Entidade
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public string nomeSistema { get; set; }
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime dtLogOperacao { get; set; }
        [Key, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.None), MaxLength]
        public string txDescricao { get; set; }

        public string nomeFuncionalidade { get; set; }        
        public string nomeTipoFuncionalidade { get; set; } 
        public string acao { get; set; }        
        public string nomeTipoDescricao { get; set; }                
        public int codUsuario { get; set; }        

        [NotMapped] public DateTime dataInicial { get; set; }
        [NotMapped] public DateTime dataFinal { get; set; }
    }
}

