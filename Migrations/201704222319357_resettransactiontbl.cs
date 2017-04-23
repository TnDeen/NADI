namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resettransactiontbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyTypeId = c.Int(),
                        UnitNo = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Address4 = c.String(),
                        BandarId = c.Int(),
                        NegeriId = c.Int(),
                        Size = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        AuctionDate = c.DateTime(),
                        AuctionTime = c.String(),
                        AuctionTypeId = c.Int(),
                        AuctionBankId = c.Int(),
                        AuctionVenue = c.String(),
                        AuctionNeer = c.String(),
                        Lawyer = c.String(),
                        Assignor = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SAKs", t => t.AuctionBankId)
                .ForeignKey("dbo.SAKs", t => t.AuctionTypeId)
                .ForeignKey("dbo.SAKs", t => t.BandarId)
                .ForeignKey("dbo.SAKs", t => t.NegeriId)
                .ForeignKey("dbo.SAKs", t => t.PropertyTypeId)
                .Index(t => t.PropertyTypeId)
                .Index(t => t.BandarId)
                .Index(t => t.NegeriId)
                .Index(t => t.AuctionTypeId)
                .Index(t => t.AuctionBankId);
            
            DropTable("dbo.Transactions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerID = c.String(),
                        VendorID = c.String(),
                        TranStatus = c.String(),
                        statusActive = c.Boolean(nullable: false),
                        claimRequestSend = c.Boolean(nullable: false),
                        claimRequestApproval = c.Boolean(nullable: false),
                        ulasan = c.String(),
                        level = c.Int(nullable: false),
                        point = c.Decimal(precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Listings", "PropertyTypeId", "dbo.SAKs");
            DropForeignKey("dbo.Listings", "NegeriId", "dbo.SAKs");
            DropForeignKey("dbo.Listings", "BandarId", "dbo.SAKs");
            DropForeignKey("dbo.Listings", "AuctionTypeId", "dbo.SAKs");
            DropForeignKey("dbo.Listings", "AuctionBankId", "dbo.SAKs");
            DropIndex("dbo.Listings", new[] { "AuctionBankId" });
            DropIndex("dbo.Listings", new[] { "AuctionTypeId" });
            DropIndex("dbo.Listings", new[] { "NegeriId" });
            DropIndex("dbo.Listings", new[] { "BandarId" });
            DropIndex("dbo.Listings", new[] { "PropertyTypeId" });
            DropTable("dbo.Listings");
        }
    }
}
