namespace Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtoken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTokens", "Token", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTokens", "Token");
        }
    }
}
