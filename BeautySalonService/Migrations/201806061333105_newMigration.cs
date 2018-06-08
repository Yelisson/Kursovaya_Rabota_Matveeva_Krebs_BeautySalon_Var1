namespace BeautySalonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        buyerId = c.Int(),
                        Client_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_id)
                .Index(t => t.Client_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "Client_id", "dbo.Clients");
            DropIndex("dbo.MessageInfoes", new[] { "Client_id" });
            DropTable("dbo.MessageInfoes");
        }
    }
}
