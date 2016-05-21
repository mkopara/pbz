namespace Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduserstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 254),
                        EmailConfirmed = c.Boolean(nullable: false),
                        Salt = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.Users");
        }
    }
}
