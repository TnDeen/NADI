namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewletter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyAuctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Unit = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        AuctionDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        SoldPrice = c.Decimal(precision: 18, scale: 2),
                        ReservePrice = c.Decimal(precision: 18, scale: 2),
                        SelfBid = c.Boolean(nullable: false),
                        AppointAgent = c.Boolean(nullable: false),
                        Status = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsLetterSubscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Subcribe = c.Boolean(nullable: false),
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
            DropTable("dbo.NewsLetterSubscriptions");
            DropTable("dbo.MyAuctions");
        }
    }
}
