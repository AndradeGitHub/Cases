
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace audatex.br.centralconsumer.domain
{
    public interface IOperation
    {
          object MapearConfPedidos01(dynamic Pedido);

        object MapearConfPedidos03(dynamic Seguradora, dynamic Pedido);
        object MapearPpecEnviado01(dynamic Pedido);
    }
}