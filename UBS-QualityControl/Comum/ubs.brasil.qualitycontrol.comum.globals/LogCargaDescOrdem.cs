using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ubs.brasil.qualitycontrol.comum.globals
{
    public enum LogCargaDescOrdem
    {        
        [Description("Dados Institucionais")]
        DADOS_INSTITUCIONAIS = 1,
        [Description("Dados Carteira")]
        DADOS_CARTEIRA = 2,
        [Description("Acesso Usuário")]
        ACESSO_USUARIO = 3,
        [Description("Perfil Risco")]
        PERFIL_RISCO = 4,
        [Description("Mercado")]
        MERCADO = 5,
        [Description("Indexadores")]
        INDEXADORES = 6,
        [Description("Carteira Cota")]
        CARTEIRA_COTA = 7,
        [Description("Indexador Preço")]
        INDEXADOR_PRECO = 8,
        [Description("Ativos")]
        ATIVOS = 9,
        [Description("Posição")]
        POSICAO = 10,
        [Description("Movimento")]
        MOVIMENTO = 11,
        [Description("Rentabilidade Carteira Benchmark")]
        RENTABILIDADE_CARTEIRA_BENCHMARK = 12,
        [Description("Carteira Benchmark")]
        CARTEIRA_BENCHMARK = 13,
        [Description("Rentabilidade Indexadores")]
        RENTABILIDADE_INDEXADORES = 14
    }    
}
