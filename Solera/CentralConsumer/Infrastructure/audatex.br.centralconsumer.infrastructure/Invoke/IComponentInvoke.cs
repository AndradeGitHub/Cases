using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.infrastructure.invoke.interfaces
{
    public interface IComponentInvoke
    {
        List<string> Processar(List<string> lstXml);
    }
}