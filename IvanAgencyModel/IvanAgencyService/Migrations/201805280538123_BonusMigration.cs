namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BonusMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Bonus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Bonus");
        }
    }
}
