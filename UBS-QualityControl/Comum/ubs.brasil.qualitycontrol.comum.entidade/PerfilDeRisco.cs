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
    [Table("PERFIL_RISCO")]
    public class PerfilDeRisco : Entidade
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]     
        public string codPerfilRisco { get; set; }
        public string nomePerfilRisco { get; set; }
        public string inAtivoInativo { get; set; }        
    }
}
