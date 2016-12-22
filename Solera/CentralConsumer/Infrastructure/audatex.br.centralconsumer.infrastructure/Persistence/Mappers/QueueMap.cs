using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using audatex.br.centralconsumer.domain.model;

namespace audatex.br.centralconsumer.infrastructure.persistence.mappers
{
    public class QueueMap : EntityTypeConfiguration<QueueModel>
    {
        public QueueMap()
        {
            ToTable("Queue");


            HasKey(x => x.Id);
            Property(x => x.SeguradoraId).HasColumnType("int").IsRequired();


        }
    }
}
