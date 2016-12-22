using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ubs.brasil.qualitycontrol.comum.globals
{
    public enum TiposDeFiltro
    {        
        [Description("Conciliação")] 
        CONCILIACAO = 1,
        [Description("Rentabilidade")] 
        RENTABILIDADE = 2,
        [Description("Acuracidade")] 
        ACURACIDADE = 3,
        [Description("C/C")] 
        CONTA_CORRENTE = 5,
        [Description("RF")] 
        RENDA_FIXA = 6,
        [Description("RV")] 
        RENDA_VARIAVEL = 7,
        [Description("Fundos")] 
        FUNDOS_DE_INVESTIMENTO = 8,
        [Description("Val. Patri. Dia")] 
        VALORIZACAO_PATRIMONIAL_DIARIA = 9,
        [Description("Val. Patri. Mês")] 
        VALORIZACAO_PATRIMONIAL_MENSAL = 10,
        [Description("Var. Cota Dia")] 
        VALORIZACAO_DIARIA_COTA = 11,
        [Description("Var. Cota Mês")] 
        VALORIZACAO_MENSAL_COTA = 12,
        [Description("Carteira Abertura")] 
        CARTEIRA_NOVA = 13,
        [Description("Carteira Encerrada")] 
        CARTEIRA_ENCERRADA = 14,

        AUM_ZERADO_NEGATIVO = 15,
        [Description("Bloqueio Ativo")] 
        BLOQUEIO_ATIVOS_MENSAL = 16
    }        
}