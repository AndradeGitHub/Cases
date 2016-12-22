using System.Data.Entity.ModelConfiguration;

using abacanet.diamond.domain.model;

namespace abacanet.diamond.infrastructure.persistence.mappers
{
    public class MappingDomainModelMap : EntityTypeConfiguration<MappingDomainModel>
    {
        public MappingDomainModelMap()
        {
            ToTable("Mapping");

            HasKey(u => u.Id);
            Property(p => p.Page).IsRequired();
            Property(p => p.Project).IsRequired();
            Property(p => p.Program).IsRequired();
            Property(p => p.Function).IsRequired();
            Property(p => p.Object).IsRequired();
            Property(p => p.AFRNumber).IsRequired();
            Property(p => p.TransactionType).IsRequired();
            Property(p => p.Notes).IsRequired();
        }
    }
}
