using System.Data.Entity.ModelConfiguration;

using abacanet.diamond.domain.model;

namespace abacanet.diamond.infrastructure.persistence.mappers
{
    public class ProfitAndLossModelMap : EntityTypeConfiguration<ProfitAndLossModel>
    {
        public ProfitAndLossModelMap()
        {
            ToTable("ProfitAndLoss");

            HasKey(u => u.Id);
            Property(p => p.Text).IsRequired().HasMaxLength(50);
            Property(p => p.Row).IsRequired();
            Property(p => p.Column).IsRequired();
            Property(p => p.Value).IsOptional();
        }
    }
}
