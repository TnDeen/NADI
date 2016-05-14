namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecolumntransaction : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Transactions", new[] { "Customer_Id" });
            DropIndex("dbo.Transactions", new[] { "Vendor_Id" });
            DropColumn("dbo.Transactions", "CustomerID");
            DropColumn("dbo.Transactions", "VendorID");
            RenameColumn(table: "dbo.Transactions", name: "Customer_Id", newName: "CustomerID");
            RenameColumn(table: "dbo.Transactions", name: "Vendor_Id", newName: "VendorID");
            AlterColumn("dbo.Transactions", "CustomerID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Transactions", "VendorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "CustomerID");
            CreateIndex("dbo.Transactions", "VendorID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Transactions", new[] { "VendorID" });
            DropIndex("dbo.Transactions", new[] { "CustomerID" });
            AlterColumn("dbo.Transactions", "VendorID", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "CustomerID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Transactions", name: "VendorID", newName: "Vendor_Id");
            RenameColumn(table: "dbo.Transactions", name: "CustomerID", newName: "Customer_Id");
            AddColumn("dbo.Transactions", "VendorID", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "Vendor_Id");
            CreateIndex("dbo.Transactions", "Customer_Id");
        }
    }
}
