namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimgurlcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "imageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "imageUrl");
        }
    }
}
