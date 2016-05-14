namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTransac3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "CustomerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "VendorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "CustomerID" });
            DropIndex("dbo.Transactions", new[] { "VendorID" });
            DropIndex("dbo.Transactions", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Transactions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false),
                        CustomerID = c.String(maxLength: 128),
                        VendorID = c.String(maxLength: 128),
                        point = c.Decimal(precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        LastUpdated = c.DateTime(nullable: false),
                        LastUpdatedBy = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionID);
            
            CreateIndex("dbo.Transactions", "ApplicationUser_Id");
            CreateIndex("dbo.Transactions", "VendorID");
            CreateIndex("dbo.Transactions", "CustomerID");
            AddForeignKey("dbo.Transactions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transactions", "VendorID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transactions", "CustomerID", "dbo.AspNetUsers", "Id");
        }
    }
}
