using System.Collections.Generic;

using audatex.br.audabridge2.infrastructure.persistence.interfaces;

namespace audatex.br.audabridge2.domain.repository.interfaces
{
    public interface IPluginRepository<T> : IRepositoryCustom<T>
    {
        T GetPlugin(T entity);
    }
}
