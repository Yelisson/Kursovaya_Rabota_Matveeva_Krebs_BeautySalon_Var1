namespace BeautySalonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "serviceName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "serviceName");
        }
    }
}
