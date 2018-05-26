namespace BeautySalonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "password", c => c.String());
            AddColumn("dbo.Clients", "mail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "mail");
            DropColumn("dbo.Clients", "password");
        }
    }
}
