namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeaturedcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "featured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "featured");
        }
    }
}
