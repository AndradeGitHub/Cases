using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using audatex.br.audabridge2.domain.model;

namespace audatex.br.audabridge2.infrastructure.persistence.mappers
{
    public class OperacaoMap : EntityTypeConfiguration<OperacaoModel>
    {
        public OperacaoMap()
        {
            ToTable("Operacao");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome).IsRequired();
            Property(x => x.Tomada).IsRequired();            
        }
    }
}
