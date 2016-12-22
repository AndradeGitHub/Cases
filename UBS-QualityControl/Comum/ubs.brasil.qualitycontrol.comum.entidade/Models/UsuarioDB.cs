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
    [Table("USUARIO")]
    public class UsuarioDB : Entidade
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codUsuario { get; set; }
        public string nomeLogin { get; set; }
        public string dsSenha { get; set; }
        public string inAtivoInativo { get; set; }
    }
}
