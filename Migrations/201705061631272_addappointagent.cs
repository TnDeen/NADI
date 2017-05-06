namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addappointagent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointAgents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        IntroducerId = c.String(maxLength: 128),
                        ListingId = c.Int(),
                        TarikhSah = c.DateTime(),
                        TarikhTamat = c.DateTime(),
                        StatusActive = c.Boolean(nullable: false),
                        nama = c.String(nullable: false),
                        ic = c.String(nullable: false),
                        contact = c.String(nullable: false),
                        address = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(),
                        CreateBy = c.String(),
                        DateUpdated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IntroducerId)
                .ForeignKey("dbo.Listings", t => t.ListingId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.IntroducerId)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppointAgents", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppointAgents", "ListingId", "dbo.Listings");
            DropForeignKey("dbo.AppointAgents", "IntroducerId", "dbo.AspNetUsers");
            DropIndex("dbo.AppointAgents", new[] { "ListingId" });
            DropIndex("dbo.AppointAgents", new[] { "IntroducerId" });
            DropIndex("dbo.AppointAgents", new[] { "UserId" });
            DropTable("dbo.AppointAgents");
        }
    }
}
