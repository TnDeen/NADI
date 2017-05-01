namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagecolpaackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberPackages", "ContentFree", c => c.String());
            AddColumn("dbo.MemberPackages", "ContentVip", c => c.String());
            DropColumn("dbo.MemberPackages", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberPackages", "Content", c => c.String());
            DropColumn("dbo.MemberPackages", "ContentVip");
            DropColumn("dbo.MemberPackages", "ContentFree");
        }
    }
}
