using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace abacanet.diamond.infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<abacanet.diamond.infrastructure.persistence.UnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(abacanet.diamond.infrastructure.persistence.UnitOfWork context)
        {
        }
    }
}
