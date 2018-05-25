namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPunishment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DiscountClients", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.DiscountClients", "DiscountId", "dbo.Discounts");
            DropIndex("dbo.DiscountClients", new[] { "ClientId" });
            DropIndex("dbo.DiscountClients", new[] { "DiscountId" });
            AddColumn("dbo.Orders", "Punishment", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Punishment", c => c.Int(nullable: false));
            DropTable("dbo.DiscountClients");
            DropTable("dbo.Discounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bonuses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Punishment = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Clients", "Punishment");
            DropColumn("dbo.Orders", "Punishment");
            CreateIndex("dbo.DiscountClients", "DiscountId");
            CreateIndex("dbo.DiscountClients", "ClientId");
            AddForeignKey("dbo.DiscountClients", "DiscountId", "dbo.Discounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DiscountClients", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
