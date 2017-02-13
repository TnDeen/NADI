namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcol2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccStatus2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AccStatus2");
        }
    }
}
