using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using audatex.br.audabridge2.domain.model;

namespace audatex.br.audabridge2.infrastructure.persistence.mappers
{
    public class IntegracaoMap : EntityTypeConfiguration<IntegracaoModel>
    {
        public IntegracaoMap()
        {
            ToTable("Integracao");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.IdAcao).IsRequired();            
            Property(x => x.Status).IsRequired();
            Property(x => x.Sinistro).IsOptional();
            Property(x => x.Wan).IsOptional();
            Property(x => x.DataRegistro).IsRequired();

            HasRequired(x => x.Operacao)
              .WithMany()
              .Map(m => m.MapKey("OperacaoId"));

            HasRequired(x => x.Seguradora)
              .WithMany()
              .Map(m => m.MapKey("SeguradoraId"));
        }
    }
}
