namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updaterftbl : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SAKs", new[] { "ParentId" });
            DropIndex("dbo.SKs", new[] { "ParentId" });
            AlterColumn("dbo.SAKs", "ParentId", c => c.Int());
            AlterColumn("dbo.SKs", "ParentId", c => c.Int());
            CreateIndex("dbo.SAKs", "ParentId");
            CreateIndex("dbo.SKs", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SKs", new[] { "ParentId" });
            DropIndex("dbo.SAKs", new[] { "ParentId" });
            AlterColumn("dbo.SKs", "ParentId", c => c.Int(nullable: false));
            AlterColumn("dbo.SAKs", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.SKs", "ParentId");
            CreateIndex("dbo.SAKs", "ParentId");
        }
    }
}
