using audatex.br.centralpublisher.domain.model;
using System.Data.Entity.ModelConfiguration;

namespace audatex.br.centralpublisher.infrastructure.persistence.mappers
{
    public class SeguradoraMap : EntityTypeConfiguration<SeguradoraModel>
    {
        public SeguradoraMap()
        {
            ToTable("Seguradora");

            HasKey(x => x.Id);
            Property(p => p.CNPJ).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(p => p.Chamador).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(p => p.Perfil).HasColumnType("varchar").HasMaxLength(3).IsRequired();
            Property(p => p.Senha).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(p => p.Serie).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(p => p.HD).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(p => p.CPF).HasColumnType("varchar").HasMaxLength(11).IsRequired();
            Property(p => p.Perito).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            Property(p => p.CNPJDest).HasColumnType("varchar").HasMaxLength(14).IsRequired();
            Property(p => p.CPFDest).HasColumnType("varchar").HasMaxLength(11).IsRequired();
            Property(p => p.Qtde).HasColumnType("int").IsRequired();

        }
    }
}
