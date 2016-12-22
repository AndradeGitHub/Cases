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
    public class Usuario : Entidade
    {
        [NotMapped] public int codUsuario { get; set; }
        [NotMapped] public string nomeLogin { get; set; }
        [NotMapped] public string dsSenha { get; set; }
        [NotMapped] public string inAtivoInativo { get; set; }
    }
}
