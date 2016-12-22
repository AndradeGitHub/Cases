using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using audatex.br.centralpublisher.domain.interfaces;

namespace audatex.br.centralpublisher.domain
{
    public class Operation : IOperation
    {
        public virtual object Mapear_PedePedidos01(dynamic Seguradora)
        {
            throw new NotImplementedException();
        }
        public virtual object Mapear_ConfRetorno01(dynamic Seguradora, dynamic ConfRetorno01)
        {
            throw new NotImplementedException();
        }
        public virtual object Mapear_PecRetorno01(dynamic Seguradora, dynamic itemstatus)
        {
            throw new NotImplementedException();
        }
        public virtual object Mapear_ConfPedidos03(dynamic Seguradora, dynamic itemstatus)
        {
            throw new NotImplementedException();
        }


    }
}
