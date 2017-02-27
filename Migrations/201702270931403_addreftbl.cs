namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreftbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SAKs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        SkId = c.Int(nullable: false),
                        Nama = c.String(),
                        Kod = c.String(),
                        Perihal = c.String(),
                        StatusActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SAKs", t => t.ParentId)
                .ForeignKey("dbo.SKs", t => t.SkId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.SkId);
            
            CreateTable(
                "dbo.SKs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        Nama = c.String(),
                        Kod = c.String(),
                        Perihal = c.String(),
                        StatusActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SKs", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Perihal = c.String(),
                        Sender = c.String(nullable: false),
                        Recipient = c.String(nullable: false),
                        ReadStatus = c.Boolean(nullable: false),
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
            DropForeignKey("dbo.SAKs", "SkId", "dbo.SKs");
            DropForeignKey("dbo.SKs", "ParentId", "dbo.SKs");
            DropForeignKey("dbo.SAKs", "ParentId", "dbo.SAKs");
            DropIndex("dbo.SKs", new[] { "ParentId" });
            DropIndex("dbo.SAKs", new[] { "SkId" });
            DropIndex("dbo.SAKs", new[] { "ParentId" });
            DropTable("dbo.Messages");
            DropTable("dbo.SKs");
            DropTable("dbo.SAKs");
        }
    }
}
