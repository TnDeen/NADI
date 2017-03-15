namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoljawatan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Jawatan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Jawatan");
        }
    }
}
