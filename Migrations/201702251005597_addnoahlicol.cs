namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnoahlicol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NomborAhli", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NomborAhli");
        }
    }
}
