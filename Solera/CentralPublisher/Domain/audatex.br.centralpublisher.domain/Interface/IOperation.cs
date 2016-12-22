using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralpublisher.domain.interfaces
{
    public interface IOperation
    {
        object Mapear_PedePedidos01(dynamic Seguradora);
        object Mapear_ConfRetorno01(dynamic Seguradora, dynamic ConfRetorno01);
        object Mapear_PecRetorno01(dynamic Seguradora, dynamic itemstatus);
        object Mapear_ConfPedidos03(dynamic Seguradora, dynamic itemstatus);
    }
}