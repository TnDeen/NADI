namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpackagetbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberPackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        FreeMember = c.Boolean(nullable: false),
                        VipMember = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberPackages");
        }
    }
}
