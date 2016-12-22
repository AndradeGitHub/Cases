
using audatex.br.centralconsumer.infrastructure.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.domain.Map
{
    public class MapeadorDestinatario
    {
        public object CriarDestinatario(dynamic Pedido)
        {
            var DESTINATARIO = new
            {
                CNPJ = Pedido.DESTINATARIO.CNPJ ?? "",
                CPF = Pedido.DESTINATARIO.CPF ?? ""
            };

            return DESTINATARIO;
        }
    }
}
