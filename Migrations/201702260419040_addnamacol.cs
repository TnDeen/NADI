namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnamacol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nama", c => c.String());
            AddColumn("dbo.AspNetUsers", "TarikhSahAhli", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "AccStatus2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccStatus2", c => c.String());
            DropColumn("dbo.AspNetUsers", "TarikhSahAhli");
            DropColumn("dbo.AspNetUsers", "Nama");
        }
    }
}
