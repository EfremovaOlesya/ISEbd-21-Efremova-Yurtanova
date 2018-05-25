namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Discount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Bonuses", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Bonuses", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Bonuses");
            DropColumn("dbo.Orders", "Bonuses");
        }
    }
}
