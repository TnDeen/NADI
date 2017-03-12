namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtarikhdaftarcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TarikhDaftarAhli", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TarikhDaftarAhli");
        }
    }
}
