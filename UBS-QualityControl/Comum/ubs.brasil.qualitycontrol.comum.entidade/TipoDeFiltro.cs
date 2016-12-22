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
    [Table("TIPO_FILTRO")]
    public class TipoDeFiltro : Entidade
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]     
        public int codTipoFiltro { get; set; }        
        public string nomeTipoFiltro { get; set; }
        public string nomeTipoFiltroAbrev { get; set; }
        public int codTipoFiltroPai { get; set; }
        public string inAtivoInativo { get; set; }
        public DateTime? dtAlteracao { get; set; }
        public int? codUsuarioAlteracao { get; set; }                
    }
}