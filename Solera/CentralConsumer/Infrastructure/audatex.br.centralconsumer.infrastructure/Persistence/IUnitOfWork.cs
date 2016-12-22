
using System.Data.Entity;

using audatex.br.centralconsumer.domain.model;

namespace audatex.br.centralconsumer.infrastructure.persistence.interfaces
{
    public interface IUnitOfWork
    {
        IDbSet<PedidoStatusModel> PedidoStatus { get; set; }
        IDbSet<SeguradoraModel> Seguradoras { get; set; }
        IDbSet<QueueModel> Queue { get; set; }
             
        void Commit();
    }
}
