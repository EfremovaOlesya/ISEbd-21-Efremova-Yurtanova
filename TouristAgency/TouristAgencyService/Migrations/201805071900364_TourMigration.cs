namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "PriceTour", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "PriceTour");
        }
    }
}
