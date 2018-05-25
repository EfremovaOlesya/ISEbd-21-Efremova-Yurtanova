namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Bonuses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Clients", "Punishment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Punishment");
            DropColumn("dbo.Clients", "Bonuses");
        }
    }
}
