namespace Implementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedsalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Salt2", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Salt2");
        }
    }
}
