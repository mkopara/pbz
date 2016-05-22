namespace Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedsalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Salt", c => c.Binary(nullable: false));
            DropColumn("dbo.Users", "Salt2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Salt2", c => c.Binary(nullable: false));
            DropColumn("dbo.Users", "Salt");
        }
    }
}
