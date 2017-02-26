namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtarikhtamanahli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TarikhTamatAhli", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TarikhTamatAhli");
        }
    }
}
