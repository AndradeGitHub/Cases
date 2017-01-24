using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using audatex.br.audabridge2.domain.model;

namespace audatex.br.audabridge2.infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<audatex.br.audabridge2.infrastructure.persistence.UnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(audatex.br.audabridge2.infrastructure.persistence.UnitOfWork context)
        {
            //BRADESCO
            //SeguradoraModel seguradora = new SeguradoraModel();
            //seguradora.Cnpj = "33055146000193";
            //seguradora.Nome = "Bradesco";
            //seguradora.Status = (int)EnumStatus.Ativo;

            //context.Seguradora.Add(seguradora);
        }
    }
}
