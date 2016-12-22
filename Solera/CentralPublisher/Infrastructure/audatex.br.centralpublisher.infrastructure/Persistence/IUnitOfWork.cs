using System.Data.Entity;
using audatex.br.centralpublisher.domain.model;

namespace audatex.br.centralpublisher.infrastructure.persistence.interfaces
{
    public interface IUnitOfWork
    {
        IDbSet<PedidoStatusModel> PedidoStatus { get; set; }
        IDbSet<SeguradoraModel> Seguradoras { get; set; }
        IDbSet<QueueModel> Queue { get; set; }

        void Commit();
    }
}
