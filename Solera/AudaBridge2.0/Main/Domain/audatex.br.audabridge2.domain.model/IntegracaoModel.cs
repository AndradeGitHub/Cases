using System;

namespace audatex.br.audabridge2.domain.model
{
    public class IntegracaoModel : EntityBase
    {        
        public int IdAcao { get; set; }        
        public OperacaoModel Operacao { get; set; }
        public int Status { get; set; }        
        public SeguradoraModel Seguradora { get; set; }
        public string Sinistro { get; set; }
        public string Wan { get; set; }
        public DateTime DataRegistro {  get; set; }
    }
}