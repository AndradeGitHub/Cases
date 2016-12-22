namespace abacanet.diamond.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfitAndLossCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfitAndLoss",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 50),
                        Value = c.Decimal(precision: 18, scale: 2),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProfitAndLoss", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfitAndLoss", "Parent_Id", "dbo.ProfitAndLoss");
            DropIndex("dbo.ProfitAndLoss", new[] { "Parent_Id" });
            DropTable("dbo.ProfitAndLoss");
        }
    }
}
