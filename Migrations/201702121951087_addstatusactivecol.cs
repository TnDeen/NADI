namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatusactivecol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "statusActive", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "statusActive");
        }
    }
}
