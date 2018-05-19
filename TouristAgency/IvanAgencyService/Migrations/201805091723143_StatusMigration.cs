namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TravelTours", "TourPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TravelTours", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TravelTours", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TravelTours", "TourPrice");
        }
    }
}
