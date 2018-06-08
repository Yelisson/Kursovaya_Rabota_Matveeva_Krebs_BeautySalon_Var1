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
                        clientName = c.String(),
                        number = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        serviceName = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clients", t => t.clientId, cascadeDelete: true)
                .Index(t => t.clientId);
            
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
                "dbo.ServiceOrders",
                c => new
                    {
                        Service_id = c.Int(nullable: false),
                        Order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_id, t.Order_id })
                .ForeignKey("dbo.Services", t => t.Service_id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_id, cascadeDelete: true)
                .Index(t => t.Service_id)
                .Index(t => t.Order_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceResources", "serviceId", "dbo.Services");
            DropForeignKey("dbo.ServiceResources", "resourceId", "dbo.Resources");
            DropForeignKey("dbo.DeliveryResources", "resourceId", "dbo.Resources");
            DropForeignKey("dbo.DeliveryResources", "deliveryId", "dbo.Deliveries");
            DropForeignKey("dbo.ServiceOrders", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.ServiceOrders", "Service_id", "dbo.Services");
            DropForeignKey("dbo.Orders", "clientId", "dbo.Clients");
            DropIndex("dbo.ServiceOrders", new[] { "Order_id" });
            DropIndex("dbo.ServiceOrders", new[] { "Service_id" });
            DropIndex("dbo.DeliveryResources", new[] { "resourceId" });
            DropIndex("dbo.DeliveryResources", new[] { "deliveryId" });
            DropIndex("dbo.ServiceResources", new[] { "resourceId" });
            DropIndex("dbo.ServiceResources", new[] { "serviceId" });
            DropIndex("dbo.Orders", new[] { "clientId" });
            DropTable("dbo.ServiceOrders");
            DropTable("dbo.Deliveries");
            DropTable("dbo.DeliveryResources");
            DropTable("dbo.Resources");
            DropTable("dbo.ServiceResources");
            DropTable("dbo.Services");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
