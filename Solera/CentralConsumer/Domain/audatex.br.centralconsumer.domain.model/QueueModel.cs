using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace audatex.br.centralconsumer.domain.model
{
    public class QueueModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }       
        public string Exchange { get; set; }        
        public int Operacao { get; set; }
        public int Status { get; set;  }
        public int Origem { get; set; }
        public int Destino { get; set; }

        [ForeignKey("SeguradoraModel")]
        public int SeguradoraId { get; set; }
        public virtual SeguradoraModel SeguradoraModel { get; set; }
    }
}