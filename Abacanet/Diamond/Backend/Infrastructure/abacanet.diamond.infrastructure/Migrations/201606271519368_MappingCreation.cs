namespace abacanet.diamond.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MappingCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mapping",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Page = c.Int(nullable: false),
                    Project = c.String(nullable: false, maxLength: 100),
                    Program = c.String(nullable: false, maxLength: 100),
                    Function = c.String(nullable: false, maxLength: 100),
                    Object = c.String(maxLength: 100),
                    AFRNumber = c.String(maxLength: 100),
                    TransactionType = c.String(),
                    Notes = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Mapping");
        }
    }
}