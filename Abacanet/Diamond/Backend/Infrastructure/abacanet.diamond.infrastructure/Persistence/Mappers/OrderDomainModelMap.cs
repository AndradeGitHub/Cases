using abacanet.diamond.domain.model;
using System.Data.Entity.ModelConfiguration;

namespace abacanet.diamond.infrastructure.persistence.mappers
{
    public class OrderDomainModelMap : EntityTypeConfiguration<OrderDomainModel>
    {
        public OrderDomainModelMap()
        {
            ToTable("Order");

            HasKey(u => u.Id);
            Property(p => p.OrderName).IsOptional().HasMaxLength(50);
        }
    }
}
