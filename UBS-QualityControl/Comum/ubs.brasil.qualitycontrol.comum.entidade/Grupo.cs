using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ubs.brasil.qualitycontrol.comum.entidade
{
    [Table("GRUPO")]
    public class Grupo 
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)] 
        public int codAcesso { get; set; }
        public int codGrupo { get; set; }
        public string nomeGrupo { get; set; }
        public string ativoInativo { get; set; }
        public int codUsuario { get; set; }
        public DateTime? dtUltimaAlteracao { get; set; }                        
    }
}