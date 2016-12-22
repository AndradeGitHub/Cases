using abacanet.diamond.domain.model;
using System.Data.Entity.ModelConfiguration;

namespace abacanet.diamond.infrastructure.persistence.mappers
{
    public class UserInviteDomainModelMap : EntityTypeConfiguration<UserInviteDomainModel>
    {
        public UserInviteDomainModelMap()
        {
            ToTable("UserInvite");

            HasKey(u => u.Id);
            Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            Property(p => p.LastName).IsRequired().HasMaxLength(50);
            Property(p => p.Company).IsOptional().HasMaxLength(100);
            Property(p => p.Email).IsOptional().HasMaxLength(50);
            Property(p => p.Notes).IsOptional();
            Property(p => p.RequestDate).IsOptional();
            Property(p => p.Address).IsOptional().HasMaxLength(50);
            Property(p => p.City).IsOptional().HasMaxLength(50);
            Property(p => p.State).IsOptional().HasMaxLength(2);
            Property(p => p.ZipCode).IsOptional();            
        }
    }
}
