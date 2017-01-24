using System.Collections.Generic;

using audatex.br.audabridge2.infrastructure.persistence.interfaces;

namespace audatex.br.audabridge2.domain.repository.interfaces
{
    public interface ISeguradoraRepository<T> : IRepositoryCustom<T>
    {
        IEnumerable<T> GetSeguradora();
    }
}