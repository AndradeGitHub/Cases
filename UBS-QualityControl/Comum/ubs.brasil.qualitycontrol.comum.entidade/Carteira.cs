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
    [Table("CARTEIRA")]
    public class Carteira : Entidade
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]       
        public string codCarteira { get; set; }        
        public string nomeCarteira { get; set; }        
        public string nuCGC { get; set; }        
        public string inAtivoInativo { get; set; }   
        public int? codCliente { get; set; }        
        public DateTime dtAbertura { get; set; }      
        public DateTime? dtEncerramento { get; set; }      
        public string codPerfilRisco { get; set; }      
        public string codAdministrador { get; set; }      
        public string codCustodiante { get; set; }      
        public string codGestor { get; set; }      
        public string codIndex { get; set; }      
        public int? codUsuarioCA { get; set; }      
    }
}