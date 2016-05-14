namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ParentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "ParentId");
            AddForeignKey("dbo.AspNetUsers", "ParentId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ParentId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ParentId" });
            DropColumn("dbo.AspNetUsers", "ParentId");
        }
    }
}
