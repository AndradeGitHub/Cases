namespace audatex.br.audabridge2.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Integracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Integracao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdAcao = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Sinistro = c.String(),
                        Wan = c.String(),
                        DataRegistro = c.DateTime(nullable: false),
                        OperacaoId = c.Int(nullable: false),
                        SeguradoraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Operacao", t => t.OperacaoId, cascadeDelete: false)
                .ForeignKey("dbo.Seguradora", t => t.SeguradoraId, cascadeDelete: false)
                .Index(t => t.OperacaoId)
                .Index(t => t.SeguradoraId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Integracao", "SeguradoraId", "dbo.Seguradora");
            DropForeignKey("dbo.Integracao", "OperacaoId", "dbo.Operacao");
            DropIndex("dbo.Integracao", new[] { "SeguradoraId" });
            DropIndex("dbo.Integracao", new[] { "OperacaoId" });
            DropTable("dbo.Integracao");
        }
    }
}
