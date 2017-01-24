namespace audatex.br.audabridge2.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seguradora : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seguradora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cnpj = c.String(nullable: false),
                        Nome = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Seguradora");
        }
    }
}
