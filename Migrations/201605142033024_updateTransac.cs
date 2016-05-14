namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTransac : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Transactions");
            AlterColumn("dbo.Transactions", "TransactionID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Transactions", "TransactionID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Transactions");
            AlterColumn("dbo.Transactions", "TransactionID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Transactions", "TransactionID");
        }
    }
}
