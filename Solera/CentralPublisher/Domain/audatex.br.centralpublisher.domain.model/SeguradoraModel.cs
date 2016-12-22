using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralpublisher.domain.model
{
    public class SeguradoraModel
    {
        public SeguradoraModel()
        {
            ListaQueue = new List<QueueModel>();
        }
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string Chamador { get; set; }
        public string Perfil { get; set; }
        public string Senha { get; set; }
        public string Serie { get; set; }
        public string HD { get; set; }
        public string CPF { get; set; }
        public string Perito { get; set; }
        public string CNPJDest { get; set; }
        public string CPFDest { get; set; }
        public int Qtde { get; set; }

        public virtual ICollection<QueueModel> ListaQueue { get; set; }
    }
}
