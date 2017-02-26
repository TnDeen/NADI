namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecoldateval : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "tarikhPenginapan", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "tarikhPenginapan", c => c.DateTime(nullable: false));
        }
    }
}
