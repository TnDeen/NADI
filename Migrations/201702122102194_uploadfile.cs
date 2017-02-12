namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        FilePathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        userID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FilePathId)
                .ForeignKey("dbo.AspNetUsers", t => t.userID)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FilePaths", "userID", "dbo.AspNetUsers");
            DropIndex("dbo.Files", new[] { "userId" });
            DropIndex("dbo.FilePaths", new[] { "userID" });
            DropTable("dbo.Files");
            DropTable("dbo.FilePaths");
        }
    }
}
