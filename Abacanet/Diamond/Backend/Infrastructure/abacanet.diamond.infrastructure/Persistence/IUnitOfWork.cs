using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using abacanet.diamond.domain.model;

namespace abacanet.diamond.infrastructure.persistence.interfaces
{
    public interface IUnitOfWork
    {
        IDbSet<MappingDomainModel> Mapping { get; set; }
        IDbSet<ProfitAndLossModel> ProfitsAndLosses { get; set; }
        IDbSet<UserDomainModel> Users { get; set; }
        IDbSet<UserInviteDomainModel> UserInvite { get; set; }
        IDbSet<OrderDomainModel> Orders { get; set; }        

        void Commit();
    }
}
