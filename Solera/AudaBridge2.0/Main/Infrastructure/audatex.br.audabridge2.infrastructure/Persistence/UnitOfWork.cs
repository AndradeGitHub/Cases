using System.Data.Entity;

using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.infrastructure.persistence.interfaces;
using audatex.br.audabridge2.infrastructure.persistence.mappers;

namespace audatex.br.audabridge2.infrastructure.persistence
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {        
        public virtual IDbSet<SeguradoraModel> Seguradora { get; set; }
        public virtual IDbSet<PluginModel> Plugin { get; set; }
        public virtual IDbSet<OperacaoModel> Operacao { get; set; }
        public virtual IDbSet<IntegracaoModel> Integracao { get; set; }

        public UnitOfWork()
            : base("AxBaseAudabridge")
        {                        
            Configuration.LazyLoadingEnabled = false;
        }

        public void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new SeguradoraMap());
            modelBuilder.Configurations.Add(new PluginMap());
            modelBuilder.Configurations.Add(new OperacaoMap());
            modelBuilder.Configurations.Add(new IntegracaoMap());
        }
    }
}
