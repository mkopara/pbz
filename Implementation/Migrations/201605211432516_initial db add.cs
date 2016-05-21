namespace Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdbadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 254),
                        EmailConfirmed = c.Boolean(nullable: false),
                        Salt = c.String(),
                        PasswordHash = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
