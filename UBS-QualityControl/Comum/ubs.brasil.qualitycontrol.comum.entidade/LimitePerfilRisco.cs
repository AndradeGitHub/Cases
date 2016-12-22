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
    [Table("LIMITES_PERFIL_RISCO")]
    public class LimitePerfilRisco : Entidade
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]     
        public int codLimitePerfilRisco { get; set; }
        public string codPerfilRisco { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]    
        public DateTime dtIniVigencia { get; set; }        
        public DateTime dtFimVigencia { get; set; }
        public double? vlrLimiteMin { get; set; }
        public double? vlrLimiteMax { get; set; }
        public int codTipoFiltro { get; set; }        
        public int codSubTipoFiltro { get; set; }        
        public DateTime? dtAlteracao { get; set; }
        public int codUsuarioAlteracao { get; set; }               
        public string inExcecao { get; set; }     
        public string inDiarioMensal { get; set; }
        
        [NotMapped] public DateTime? dtInicial { get; set; }
        [NotMapped] public DateTime? dtFinal { get; set; }
    }
}