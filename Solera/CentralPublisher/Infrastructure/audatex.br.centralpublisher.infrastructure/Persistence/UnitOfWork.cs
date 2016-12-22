using System.Data.Entity;
using audatex.br.centralpublisher.domain.model;
using audatex.br.centralpublisher.infrastructure.persistence.interfaces;
using audatex.br.centralpublisher.infrastructure.persistence.mappers;

namespace audatex.br.centralpublisher.infrastructure.persistence
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {        
        public virtual IDbSet<PedidoStatusModel> PedidoStatus { get; set; }
        public virtual IDbSet<SeguradoraModel> Seguradoras { get; set; }
        public virtual IDbSet<QueueModel> Queue { get; set; }

        public UnitOfWork() : base("Fornecimento")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PedidoStatusMap());
            modelBuilder.Configurations.Add(new SeguradoraMap());
            modelBuilder.Configurations.Add(new QueueMap());
        }
    }
}
