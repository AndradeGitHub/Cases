using System.Collections.Generic;

namespace audatex.br.centralpublisher.domain.repository.interfaces
{
    public interface Seguradora<T> : IRepository<T>
    {
        IEnumerable<T> GetPedidoPendente();
    }
}