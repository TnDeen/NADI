namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolimgurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "imgUrl", c => c.String());
            AddColumn("dbo.News", "imgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "imgUrl");
            DropColumn("dbo.Articles", "imgUrl");
        }
    }
}
