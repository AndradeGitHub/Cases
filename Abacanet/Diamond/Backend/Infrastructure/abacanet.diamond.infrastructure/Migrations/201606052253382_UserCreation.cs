namespace abacanet.diamond.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Company = c.String(maxLength: 100),
                        Email = c.String(maxLength: 50),
                        Notes = c.String(),
                        RequestDate = c.DateTime(),
                        Address = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 2),
                        ZipCode = c.String(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
