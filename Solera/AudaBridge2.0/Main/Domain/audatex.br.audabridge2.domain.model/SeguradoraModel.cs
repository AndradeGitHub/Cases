namespace audatex.br.audabridge2.domain.model
{
    public class SeguradoraModel : EntityBase
    {        
        public string Cnpj { get; set; }        
        public string Nome { get; set; }
        public int Status { get; set; }
    }
}