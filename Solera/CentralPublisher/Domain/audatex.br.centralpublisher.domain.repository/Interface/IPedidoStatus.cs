using System.Collections.Generic;

namespace audatex.br.centralpublisher.domain.repository.interfaces
{
    public interface IPedidoStatus<T> : IRepository<T>
    {
        IEnumerable<T> GetPedidoPendente();
    }
}