namespace audatex.br.audabridge2.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Operacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Operacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),                        
                        Nome = c.String(nullable: false),
                        Tomada = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Operacao");
        }
    }
}
