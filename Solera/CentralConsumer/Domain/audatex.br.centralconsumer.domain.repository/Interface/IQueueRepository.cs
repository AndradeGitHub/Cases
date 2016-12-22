using audatex.br.centralconsumer.domain.repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace audatex.br.centralconsumer.domain.repository.interfaces
{
    public interface IQueueRepository<T> : IRepository<T>
    {
        IEnumerable<T> GetQueue(T entity);
    }
}
