using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace ubs.brasil.qualitycontrol.comum.entidade
{    
    public class Processamento : Entidade
    {
        public int codProcessamento { get; set; }
        public string codCarteira { get; set; }
        public int qtdeCarteira { get; set; }
        public int? codSubTipoFiltro { get; set; }
        public int? codTipoFiltro { get; set; }
        public int? codTipoFiltroPai { get; set; }   
        public string nomeFiltro { get; set; }
        public string nomeFiltroAbrev { get; set; }
        public double? vlPatrimonial { get; set; }
        public string vlPatrimonialBR { get; set; }
        public string codAtivo { get; set; }
        public string nomeAtivo { get; set; }
        public string codTipoAtivo { get; set; }    
        public string nomeCarteira { get; set; }
        public string nomeAtivoCarteira { get; set; }            
        public DateTime dtResultado { get; set; }
        public DateTime dtPosicao { get; set; }    
        public string inLiberado { get; set; }
        public string inEnquadrado { get; set; }        

        public int codUsuario{ get; set; }
        public string inTipoExecucao { get; set; }  
        public string inPeriodoProcessamento { get; set; }
        public DateTime dtExecucaoIni { get; set; }
        public string txDescricao { get; set; }

        public int? codSubTipoFiltroRet { get; set; }        
        public DateTime dtResultadoRet { get; set; }
        public string dtResultadoPesq { get; set; }
        public DateTime dtReferencia { get; set; }
        public double? vlrSaldoBruto { get; set; }
        public double vlrPosicaoAtivo { get; set; }
        public int ordem { get; set; }
        public string acaoProcessamento { get; set; }
        public int codCarga { get; set; }
        public string codCarteiras { get; set; }
        public string txtDescLogCargas { get; set; }
    }
}