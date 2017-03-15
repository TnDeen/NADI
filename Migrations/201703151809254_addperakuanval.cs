namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addperakuanval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Perakuan", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Perakuan");
        }
    }
}
