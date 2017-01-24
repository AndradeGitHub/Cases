using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using audatex.br.audabridge2.domain.model;

namespace audatex.br.audabridge2.infrastructure.persistence.mappers
{
    public class SeguradoraMap : EntityTypeConfiguration<SeguradoraModel>
    {
        public SeguradoraMap()
        {
            ToTable("Seguradora");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(x => x.Cnpj).IsRequired();            
            Property(x => x.Nome).IsRequired();
            Property(x => x.Status).IsRequired();
        }
    }
}
