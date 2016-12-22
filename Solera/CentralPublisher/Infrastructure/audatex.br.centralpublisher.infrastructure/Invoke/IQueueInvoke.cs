using System.Collections.Generic;

namespace audatex.br.centralpublisher.infrastructure.invoke.interfaces
{
    public interface IQueueInvoke
    {
        void PublishDirect<T>(string exchange, string queue, T message);
    }
}