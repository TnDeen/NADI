namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgecolposkod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "Poskod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "Poskod");
        }
    }
}
