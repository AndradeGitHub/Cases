namespace abacanet.diamond.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludeNumberToProfitAndLoss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfitAndLoss", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfitAndLoss", "Number");
        }
    }
}
