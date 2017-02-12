namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlevelcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "level");
        }
    }
}
