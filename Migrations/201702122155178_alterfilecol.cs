namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterfilecol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enrollments", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enrollments", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Students", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Students", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Files", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Files", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Files", "CreateBy", c => c.String());
            AddColumn("dbo.Files", "DateUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Files", "LastUpdated", c => c.DateTime());
            AddColumn("dbo.Files", "LastUpdatedBy", c => c.String());
            AlterColumn("dbo.Courses", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Courses", "LastUpdated", c => c.DateTime());
            AlterColumn("dbo.Enrollments", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Enrollments", "LastUpdated", c => c.DateTime());
            AlterColumn("dbo.Students", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Students", "LastUpdated", c => c.DateTime());
            AlterColumn("dbo.Transactions", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Transactions", "LastUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "LastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Transactions", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "LastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Students", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Enrollments", "LastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Enrollments", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "LastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Files", "LastUpdatedBy");
            DropColumn("dbo.Files", "LastUpdated");
            DropColumn("dbo.Files", "DateUpdated");
            DropColumn("dbo.Files", "CreateBy");
            DropColumn("dbo.Files", "CreateDate");
            DropColumn("dbo.Files", "DateCreated");
            DropColumn("dbo.Transactions", "DateUpdated");
            DropColumn("dbo.Transactions", "DateCreated");
            DropColumn("dbo.Students", "DateUpdated");
            DropColumn("dbo.Students", "DateCreated");
            DropColumn("dbo.Enrollments", "DateUpdated");
            DropColumn("dbo.Enrollments", "DateCreated");
            DropColumn("dbo.Courses", "DateUpdated");
            DropColumn("dbo.Courses", "DateCreated");
        }
    }
}
