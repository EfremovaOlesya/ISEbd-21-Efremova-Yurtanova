namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TravelMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TravelTours", "Count", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TravelTours", "Count", c => c.Int(nullable: false));
        }
    }
}
