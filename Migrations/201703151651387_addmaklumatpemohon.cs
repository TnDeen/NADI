namespace MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmaklumatpemohon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Alamat", c => c.String());
            AddColumn("dbo.AspNetUsers", "NoTelRum", c => c.String());
            AddColumn("dbo.AspNetUsers", "NoTelBim", c => c.String());
            AddColumn("dbo.AspNetUsers", "TempatLahir", c => c.String());
            AddColumn("dbo.AspNetUsers", "NoPengenalan", c => c.String());
            AddColumn("dbo.AspNetUsers", "Bangsa", c => c.String());
            AddColumn("dbo.AspNetUsers", "Jantina", c => c.String());
            AddColumn("dbo.AspNetUsers", "Pekerjaan", c => c.String());
            AddColumn("dbo.AspNetUsers", "maritalStatus", c => c.String());
            AddColumn("dbo.AspNetUsers", "AlamatWaris", c => c.String());
            AddColumn("dbo.AspNetUsers", "NomborTelefonWarisHP", c => c.String());
            AddColumn("dbo.AspNetUsers", "NegeriId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "NegeriId");
            AddForeignKey("dbo.AspNetUsers", "NegeriId", "dbo.SAKs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "NegeriId", "dbo.SAKs");
            DropIndex("dbo.AspNetUsers", new[] { "NegeriId" });
            DropColumn("dbo.AspNetUsers", "NegeriId");
            DropColumn("dbo.AspNetUsers", "NomborTelefonWarisHP");
            DropColumn("dbo.AspNetUsers", "AlamatWaris");
            DropColumn("dbo.AspNetUsers", "maritalStatus");
            DropColumn("dbo.AspNetUsers", "Pekerjaan");
            DropColumn("dbo.AspNetUsers", "Jantina");
            DropColumn("dbo.AspNetUsers", "Bangsa");
            DropColumn("dbo.AspNetUsers", "NoPengenalan");
            DropColumn("dbo.AspNetUsers", "TempatLahir");
            DropColumn("dbo.AspNetUsers", "NoTelBim");
            DropColumn("dbo.AspNetUsers", "NoTelRum");
            DropColumn("dbo.AspNetUsers", "Alamat");
        }
    }
}
