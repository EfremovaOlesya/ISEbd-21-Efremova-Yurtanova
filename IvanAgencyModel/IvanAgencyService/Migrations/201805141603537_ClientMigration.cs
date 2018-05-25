namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "Mail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Mail");
            DropColumn("dbo.Clients", "Password");
        }
    }
}
