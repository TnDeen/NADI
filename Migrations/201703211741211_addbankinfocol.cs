namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbankinfocol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NamaBank", c => c.String());
            AddColumn("dbo.AspNetUsers", "NomborAkaunBank", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NomborAkaunBank");
            DropColumn("dbo.AspNetUsers", "NamaBank");
        }
    }
}
