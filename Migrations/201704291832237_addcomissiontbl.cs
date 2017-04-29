namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcomissiontbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AffiliateComissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        IntroducerId = c.String(maxLength: 128),
                        statusActive = c.Boolean(nullable: false),
                        ulasan = c.String(),
                        level = c.Int(nullable: false),
                        point = c.Decimal(precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IntroducerId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.IntroducerId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AffiliateComissions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AffiliateComissions", "IntroducerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AffiliateComissions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AffiliateComissions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AffiliateComissions", new[] { "IntroducerId" });
            DropIndex("dbo.AffiliateComissions", new[] { "UserId" });
            DropTable("dbo.AffiliateComissions");
        }
    }
}
