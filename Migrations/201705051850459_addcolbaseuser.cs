namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolbaseuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipRequests", "nama", c => c.String(nullable: false));
            AddColumn("dbo.MembershipRequests", "ic", c => c.String(nullable: false));
            AddColumn("dbo.MembershipRequests", "contact", c => c.String(nullable: false));
            AddColumn("dbo.MembershipRequests", "address", c => c.String());
            AddColumn("dbo.PosRequests", "nama", c => c.String(nullable: false));
            AddColumn("dbo.PosRequests", "ic", c => c.String(nullable: false));
            AddColumn("dbo.PosRequests", "contact", c => c.String(nullable: false));
            AddColumn("dbo.PosRequests", "address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PosRequests", "address");
            DropColumn("dbo.PosRequests", "contact");
            DropColumn("dbo.PosRequests", "ic");
            DropColumn("dbo.PosRequests", "nama");
            DropColumn("dbo.MembershipRequests", "address");
            DropColumn("dbo.MembershipRequests", "contact");
            DropColumn("dbo.MembershipRequests", "ic");
            DropColumn("dbo.MembershipRequests", "nama");
        }
    }
}
