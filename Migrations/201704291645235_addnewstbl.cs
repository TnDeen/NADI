namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewstbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Link = c.String(),
                        active = c.Boolean(nullable: false),
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
            DropTable("dbo.News");
        }
    }
}
