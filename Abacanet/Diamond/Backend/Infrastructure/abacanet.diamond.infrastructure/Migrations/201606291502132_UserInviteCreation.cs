namespace abacanet.diamond.infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInviteCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInvite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInvite");
        }
    }
}