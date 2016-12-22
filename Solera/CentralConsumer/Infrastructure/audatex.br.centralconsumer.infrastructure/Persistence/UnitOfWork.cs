using System.Data.Entity;
using audatex.br.centralconsumer.domain.model;
using audatex.br.centralconsumer.infrastructure.persistence.interfaces;
using audatex.br.centralconsumer.infrastructure.persistence.mappers;


namespace audatex.br.centralconsumer.infrastructure.persistence
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        //private static string cliente = ConfigurationManager.AppSettings["Cliente"];

        
        public virtual IDbSet<PedidoStatusModel> PedidoStatus { get; set; }
        public virtual IDbSet<SeguradoraModel> Seguradoras { get; set; }
        public virtual IDbSet<QueueModel> Queue { get; set; }


        public UnitOfWork()
            : base("Fornecimento")
        {          

            Configuration.LazyLoadingEnabled = true;
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new SeguradoraMap());
            modelBuilder.Configurations.Add(new PedidoStatusMap());
            modelBuilder.Configurations.Add(new QueueMap());
        }
    }
}
