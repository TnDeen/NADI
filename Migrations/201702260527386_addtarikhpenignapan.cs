namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtarikhpenignapan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "tarikhPenginapan", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "tarikhPenginapan");
        }
    }
}
