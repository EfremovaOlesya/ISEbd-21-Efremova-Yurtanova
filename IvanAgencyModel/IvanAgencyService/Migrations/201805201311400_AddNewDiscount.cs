namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewDiscount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiscountClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        DiscountId = c.Int(nullable: false),
                        Bonuses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Punishment = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Discounts", t => t.DiscountId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.DiscountId);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bonuses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Punishment = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Clients", "Bonuses");
            DropColumn("dbo.Clients", "Punishment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Punishment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Clients", "Bonuses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.DiscountClients", "DiscountId", "dbo.Discounts");
            DropForeignKey("dbo.DiscountClients", "ClientId", "dbo.Clients");
            DropIndex("dbo.DiscountClients", new[] { "DiscountId" });
            DropIndex("dbo.DiscountClients", new[] { "ClientId" });
            DropTable("dbo.Discounts");
            DropTable("dbo.DiscountClients");
        }
    }
}
