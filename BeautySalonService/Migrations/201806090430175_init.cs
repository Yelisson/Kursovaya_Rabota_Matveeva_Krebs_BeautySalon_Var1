namespace BeautySalonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        clientFirstName = c.String(nullable: false),
                        clientSecondName = c.String(nullable: false),
                        number = c.Int(nullable: false),
                        password = c.String(),
                        mail = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        clientId = c.Int(nullable: false),
                        number = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clients", t => t.clientId, cascadeDelete: true)
                .Index(t => t.clientId);
            
            CreateTable(
                "dbo.OrderServices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        orderId = c.Int(nullable: false),
                        serviceId = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.serviceId, cascadeDelete: true)
                .Index(t => t.orderId)
                .Index(t => t.serviceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        serviceName = c.String(nullable: false),
                        description = c.String(),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ServiceResources",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        serviceId = c.Int(nullable: false),
                        resourceId = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Resources", t => t.resourceId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.serviceId, cascadeDelete: true)
                .Index(t => t.serviceId)
                .Index(t => t.resourceId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        resourceName = c.String(nullable: false),
                        sumCount = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DeliveryResources",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        deliveryId = c.Int(nullable: false),
                        resourceId = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Deliveries", t => t.deliveryId, cascadeDelete: true)
                .ForeignKey("dbo.Resources", t => t.resourceId, cascadeDelete: true)
                .Index(t => t.deliveryId)
                .Index(t => t.resourceId);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
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
            DropForeignKey("dbo.ServiceResources", "serviceId", "dbo.Services");
            DropForeignKey("dbo.ServiceResources", "resourceId", "dbo.Resources");
            DropForeignKey("dbo.DeliveryResources", "resourceId", "dbo.Resources");
            DropForeignKey("dbo.DeliveryResources", "deliveryId", "dbo.Deliveries");
            DropForeignKey("dbo.OrderServices", "serviceId", "dbo.Services");
            DropForeignKey("dbo.OrderServices", "orderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "clientId", "dbo.Clients");
            DropIndex("dbo.MessageInfoes", new[] { "Client_id" });
            DropIndex("dbo.DeliveryResources", new[] { "resourceId" });
            DropIndex("dbo.DeliveryResources", new[] { "deliveryId" });
            DropIndex("dbo.ServiceResources", new[] { "resourceId" });
            DropIndex("dbo.ServiceResources", new[] { "serviceId" });
            DropIndex("dbo.OrderServices", new[] { "serviceId" });
            DropIndex("dbo.OrderServices", new[] { "orderId" });
            DropIndex("dbo.Orders", new[] { "clientId" });
            DropTable("dbo.MessageInfoes");
            DropTable("dbo.Deliveries");
            DropTable("dbo.DeliveryResources");
            DropTable("dbo.Resources");
            DropTable("dbo.ServiceResources");
            DropTable("dbo.Services");
            DropTable("dbo.OrderServices");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
