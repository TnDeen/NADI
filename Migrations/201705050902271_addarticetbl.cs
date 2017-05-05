namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addarticetbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Link = c.String(),
                        active = c.Boolean(nullable: false),
                        featured = c.Boolean(nullable: false),
                        articleTypeId = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SAKs", t => t.articleTypeId)
                .Index(t => t.articleTypeId);
            
            AddColumn("dbo.News", "featured", c => c.Boolean(nullable: false));
            AddColumn("dbo.News", "newsTypeId", c => c.Int());
            CreateIndex("dbo.News", "newsTypeId");
            AddForeignKey("dbo.News", "newsTypeId", "dbo.SAKs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "newsTypeId", "dbo.SAKs");
            DropForeignKey("dbo.Articles", "articleTypeId", "dbo.SAKs");
            DropIndex("dbo.News", new[] { "newsTypeId" });
            DropIndex("dbo.Articles", new[] { "articleTypeId" });
            DropColumn("dbo.News", "newsTypeId");
            DropColumn("dbo.News", "featured");
            DropTable("dbo.Articles");
        }
    }
}
