namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolwaris : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NamaWaris", c => c.String());
            AddColumn("dbo.AspNetUsers", "NomborTelefonWaris", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NomborTelefonWaris");
            DropColumn("dbo.AspNetUsers", "NamaWaris");
        }
    }
}
