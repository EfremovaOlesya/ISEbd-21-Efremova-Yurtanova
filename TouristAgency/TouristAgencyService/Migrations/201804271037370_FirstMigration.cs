namespace TouristAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Shtraf", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "DayCount", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "AdultsCount", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ChildrenCount", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "DateImplement", c => c.DateTime());
            DropColumn("dbo.Travels", "DayCount");
            DropColumn("dbo.Travels", "AdultsCount");
            DropColumn("dbo.Travels", "ChildrenCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travels", "ChildrenCount", c => c.Int(nullable: false));
            AddColumn("dbo.Travels", "AdultsCount", c => c.Int(nullable: false));
            AddColumn("dbo.Travels", "DayCount", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "DateImplement");
            DropColumn("dbo.Orders", "ChildrenCount");
            DropColumn("dbo.Orders", "AdultsCount");
            DropColumn("dbo.Orders", "DayCount");
            DropColumn("dbo.Clients", "Shtraf");
        }
    }
}
