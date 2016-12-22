using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace ubs.brasil.qualitycontrol.comum.entidade
{    
    public class Enquadramento : Entidade
    {
        public string codCarteira { get; set; }
        public int codTipoFiltroPai { get; set; }
        public int codTipoFiltro { get; set; }
        public string nomeTipoFiltro { get; set; }
        public int codSubTipoFiltro { get; set; }        
        public string inEnquadrado { get; set; }
        public DateTime dtResultado { get; set; }
        public string inDiarioMensal { get; set; }
        public int codProcessamento { get; set; }
        public double vlrPontaA { get; set; }
        public double vlrPontaB { get; set; }
        public DateTime dtAberturaCart { get; set; }
        public DateTime dtEncerramentoCart { get; set; }
        public DateTime? dtPosicao { get; set; }
        public string codListaSubTipo { get; set; }        
        public string dsCausaInconsistencia { get; set; }
        public string inLiberado { get; set; }
        public int codUsuario { get; set; }
        public string inPeriodoProcessamento { get; set; }
        public DateTime dtReferenciaMensal { get; set; }
    }
}