namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclaimcolumntran : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "claimRequestSend", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "claimRequestApproval", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "ulasan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "ulasan");
            DropColumn("dbo.Transactions", "claimRequestApproval");
            DropColumn("dbo.Transactions", "claimRequestSend");
        }
    }
}
