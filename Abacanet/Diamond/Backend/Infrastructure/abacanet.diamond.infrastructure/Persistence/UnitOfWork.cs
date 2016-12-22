using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

using abacanet.diamond.domain.model;
using abacanet.diamond.infrastructure.persistence.mappers;
using abacanet.diamond.infrastructure.persistence.interfaces;

namespace abacanet.diamond.infrastructure.persistence
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public virtual IDbSet<MappingDomainModel> Mapping { get; set; }
        public virtual IDbSet<ProfitAndLossModel> ProfitsAndLosses { get; set; }
        public virtual IDbSet<UserDomainModel> Users { get; set; }
        public virtual IDbSet<UserInviteDomainModel> UserInvite { get; set; }
        public virtual IDbSet<OrderDomainModel> Orders { get; set; }        

        public UnitOfWork() : base("DiamondContext")
        {
        }

        public void Commit()
        {            
            SaveChanges();            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MappingDomainModelMap());
            modelBuilder.Configurations.Add(new ProfitAndLossModelMap());
            modelBuilder.Configurations.Add(new OrderDomainModelMap());
            modelBuilder.Configurations.Add(new UserDomainModelMap());
            modelBuilder.Configurations.Add(new UserInviteDomainModelMap());            
        }
    }
}
