namespace IvanAgencyService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "Password");
        }
    }
}
