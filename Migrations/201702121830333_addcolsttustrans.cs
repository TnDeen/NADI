namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolsttustrans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "TranStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "TranStatus");
        }
    }
}
