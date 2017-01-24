namespace audatex.br.audabridge2.domain.model
{
    public class PluginModel : EntityBase
    {        
        public int Tomada { get; set; }        
        public string Nome { get; set; }
        public SeguradoraModel Seguradora { get; set; }
        public int Status { get; set; }
    }
}