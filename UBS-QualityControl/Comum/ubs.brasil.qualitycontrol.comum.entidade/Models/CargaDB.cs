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
    [Table("CARGA")]
    public class CargaDB : Entidade    
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codCarga { get; set; }
        public DateTime dtReferencia { get; set; }
        public string inTipoProcessamento { get; set; }      
        public DateTime dtExecucaoIni { get; set; }
        public DateTime? dtExecucaoFim { get; set; }
        public int? codUsuarioResponsavel { get; set; }
        public string inFinalizado { get; set; }        
    }
}