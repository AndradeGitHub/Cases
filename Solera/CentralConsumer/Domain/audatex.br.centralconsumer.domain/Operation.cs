using System;
using System.Collections.Generic;
using System.Linq;

namespace audatex.br.centralconsumer.domain
{
    public class Operation : IOperation
    {
        public virtual object MapearConfPedidos01(dynamic Pedido)
        {
            throw new NotImplementedException();
        }

        public virtual object MapearConfPedidos03(dynamic Seguradora, dynamic Pedido)
        {
            throw new NotImplementedException();
        }

        public virtual object MapearPpecEnviado01(dynamic DadosSeguradora)
        {
            throw new NotImplementedException();
        }
     
       
    }
}