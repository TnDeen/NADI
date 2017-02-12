namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccStatus");
        }
    }
}
