using System.Data.Entity.ModelConfiguration;
using audatex.br.centralpublisher.domain.model;

namespace audatex.br.centralpublisher.infrastructure.persistence.mappers
{
    public class PedidoStatusMap : EntityTypeConfiguration<PedidoStatusModel>
    {
        public PedidoStatusMap()
        {
            ToTable("PedidoStatus");
            HasKey(x => x.Id);

            Property(x => x.StatusOperacao).HasColumnType("int");
            Property(x => x.TipoOperacao).HasColumnType("int");
        }
    }
}
