using System.Collections.Generic;

namespace audatex.br.centralconsumer.infrastructure.invoke.interfaces
{
    public interface IQueueInvoke<T>
    {
        List<object> BasicConsumerListener(List<T> lstQueue);
    }
}