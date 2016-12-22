namespace audatex.br.centralpublisher.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrigemDestino : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PedidoStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CnpjSeguradora = c.String(),
                        CnpjOficina = c.String(),
                        CnpjFornecedor = c.String(),
                        IdPedido = c.String(),
                        Numero = c.Int(),
                        DataAbertura = c.DateTime(),
                        StatusOperacao = c.Int(nullable: false),
                        TipoOperacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Queue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Exchange = c.String(),
                        Operacao = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Origem = c.Int(nullable: false),
                        Destino = c.Int(nullable: false),
                        SeguradoraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seguradora", t => t.SeguradoraId, cascadeDelete: true)
                .Index(t => t.SeguradoraId);
            
            CreateTable(
                "dbo.Seguradora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CNPJ = c.String(nullable: false, maxLength: 14, unicode: false),
                        Chamador = c.String(nullable: false, maxLength: 200, unicode: false),
                        Perfil = c.String(nullable: false, maxLength: 3, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 100, unicode: false),
                        Serie = c.String(nullable: false, maxLength: 100, unicode: false),
                        HD = c.String(nullable: false, maxLength: 50, unicode: false),
                        CPF = c.String(nullable: false, maxLength: 11, unicode: false),
                        Perito = c.String(nullable: false, maxLength: 200, unicode: false),
                        CNPJDest = c.String(nullable: false, maxLength: 14, unicode: false),
                        CPFDest = c.String(nullable: false, maxLength: 11, unicode: false),
                        Qtde = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queue", "SeguradoraId", "dbo.Seguradora");
            DropIndex("dbo.Queue", new[] { "SeguradoraId" });
            DropTable("dbo.Seguradora");
            DropTable("dbo.Queue");
            DropTable("dbo.PedidoStatus");
        }
    }
}
