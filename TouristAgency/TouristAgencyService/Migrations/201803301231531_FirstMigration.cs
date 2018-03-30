namespace TouristAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientFIO = c.String(nullable: false),
                        Bonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientLogin = c.String(nullable: false),
                        ClientPassword = c.String(nullable: false),
                        Block = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        TravelId = c.Int(nullable: false),
                        WorkerId = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId)
                .Index(t => t.ClientId)
                .Index(t => t.TravelId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelName = c.String(nullable: false),
                        DayCount = c.Int(nullable: false),
                        AdultsCount = c.Int(nullable: false),
                        ChildrenCount = c.Int(nullable: false),
                        PriceTravel = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        TravelId = c.Int(nullable: false),
                        PriceTour = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkerFIO = c.String(nullable: false),
                        WorkerLogin = c.String(nullable: false),
                        WorkerPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.TravelTours", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.TravelTours", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Orders", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.TravelTours", new[] { "TourId" });
            DropIndex("dbo.TravelTours", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "WorkerId" });
            DropIndex("dbo.Orders", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropTable("dbo.Workers");
            DropTable("dbo.Tours");
            DropTable("dbo.TravelTours");
            DropTable("dbo.Travels");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
