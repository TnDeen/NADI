namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmenutbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false),
                        Action = c.String(nullable: false),
                        Controller = c.String(nullable: false),
                        htmlAtribute = c.String(),
                        articleTypeId = c.Int(),
                        menuTypeId = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SAKs", t => t.articleTypeId)
                .ForeignKey("dbo.SAKs", t => t.menuTypeId)
                .Index(t => t.articleTypeId)
                .Index(t => t.menuTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "menuTypeId", "dbo.SAKs");
            DropForeignKey("dbo.Menus", "articleTypeId", "dbo.SAKs");
            DropIndex("dbo.Menus", new[] { "menuTypeId" });
            DropIndex("dbo.Menus", new[] { "articleTypeId" });
            DropTable("dbo.Menus");
        }
    }
}
