using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using audatex.br.audabridge2.domain.model;

namespace audatex.br.audabridge2.infrastructure.persistence.mappers
{
    public class PluginMap : EntityTypeConfiguration<PluginModel>
    {
        public PluginMap()
        {
            ToTable("Plugin");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Tomada).IsRequired();                        
            Property(x => x.Nome).IsRequired();            
            Property(x => x.Status).IsRequired();

            HasRequired(x => x.Seguradora)
              .WithMany()
              .Map(m => m.MapKey("SeguradoraId"));
        }
    }
}
