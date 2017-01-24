namespace audatex.br.audabridge2.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Plugin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plugin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tomada = c.Int(nullable: false),
                        Nome = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        SeguradoraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seguradora", t => t.SeguradoraId, cascadeDelete: true)
                .Index(t => t.SeguradoraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plugin", "SeguradoraId", "dbo.Seguradora");
            DropIndex("dbo.Plugin", new[] { "SeguradoraId" });
            DropTable("dbo.Plugin");
        }
    }
}
