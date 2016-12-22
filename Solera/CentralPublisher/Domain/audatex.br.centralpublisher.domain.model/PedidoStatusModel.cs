using System;

namespace audatex.br.centralpublisher.domain.model
{
    public class PedidoStatusModel
    {
        public int Id { get; set; }
        public string CnpjSeguradora { get; set; }
        public string CnpjOficina { get; set; }
        public string CnpjFornecedor { get; set; }
        public string IdPedido { get; set; }
        public int? Numero { get; set; }
        public DateTime? DataAbertura { get; set; }

        public int StatusOperacao { get; set; }
        public int TipoOperacao { get; set; }
        public string Operacao { get; set; }
    }
}