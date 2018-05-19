namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IvanSuMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        TravelId = c.Int(nullable: false),
                        AdminId = c.Int(),
                        Day = c.Int(nullable: false),
                        Summa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        DateOfImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.TravelId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TravelTours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .Index(t => t.TravelId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TravelTours", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.TravelTours", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Orders", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Orders", "AdminId", "dbo.Admins");
            DropIndex("dbo.TravelTours", new[] { "TourId" });
            DropIndex("dbo.TravelTours", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "AdminId" });
            DropIndex("dbo.Orders", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropTable("dbo.Tours");
            DropTable("dbo.TravelTours");
            DropTable("dbo.Travels");
            DropTable("dbo.Clients");
            DropTable("dbo.Orders");
            DropTable("dbo.Admins");
        }
    }
}
