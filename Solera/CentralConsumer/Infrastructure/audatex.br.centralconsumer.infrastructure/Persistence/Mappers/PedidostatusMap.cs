using System.Data.Entity.ModelConfiguration;
using audatex.br.centralconsumer.domain.model;

namespace audatex.br.centralconsumer.infrastructure.persistence.mappers
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
