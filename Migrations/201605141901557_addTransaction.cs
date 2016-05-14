namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        VendorID = c.Int(nullable: false),
                        point = c.Decimal(precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        LastUpdated = c.DateTime(nullable: false),
                        LastUpdatedBy = c.String(),
                        Customer_Id = c.String(maxLength: 128),
                        Vendor_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Vendor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Vendor_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "Vendor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "Customer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Transactions", new[] { "Vendor_Id" });
            DropIndex("dbo.Transactions", new[] { "Customer_Id" });
            DropTable("dbo.Transactions");
        }
    }
}
