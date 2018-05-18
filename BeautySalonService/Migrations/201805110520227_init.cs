namespace BeautySalonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        adminFirstName = c.String(nullable: false),
                        adminSecondName = c.String(),
                        number = c.Int(nullable: false),
                        login = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        adminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Admins", t => t.adminId, cascadeDelete: true)
                .Index(t => t.adminId);
            
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
                "dbo.Resources",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        resourceName = c.String(nullable: false),
                        sumCount = c.Int(nullable: false),
                        price = c.Int(nullable: false),
                        serviceId = c.Int(nullable: false),
                        deliveryId = c.Int(nullable: false),
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
                "dbo.Services",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        serviceName = c.String(nullable: false),
                        description = c.String(nullable: false),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        clientFirstName = c.String(nullable: false),
                        clientSecondName = c.String(nullable: false),
                        number = c.Int(nullable: false),
                        login = c.String(nullable: false),
                        password = c.String(nullable: false),
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
                        adminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Admins", t => t.adminId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.clientId, cascadeDelete: true)
                .Index(t => t.clientId)
                .Index(t => t.adminId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderServices", "serviceId", "dbo.Services");
            DropForeignKey("dbo.OrderServices", "orderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "clientId", "dbo.Clients");
            DropForeignKey("dbo.Orders", "adminId", "dbo.Admins");
            DropForeignKey("dbo.Deliveries", "adminId", "dbo.Admins");
            DropForeignKey("dbo.ServiceResources", "serviceId", "dbo.Services");
            DropForeignKey("dbo.ServiceResources", "resourceId", "dbo.Resources");
            DropForeignKey("dbo.DeliveryResources", "resourceId", "dbo.Resources");
            DropForeignKey("dbo.DeliveryResources", "deliveryId", "dbo.Deliveries");
            DropIndex("dbo.OrderServices", new[] { "serviceId" });
            DropIndex("dbo.OrderServices", new[] { "orderId" });
            DropIndex("dbo.Orders", new[] { "adminId" });
            DropIndex("dbo.Orders", new[] { "clientId" });
            DropIndex("dbo.ServiceResources", new[] { "resourceId" });
            DropIndex("dbo.ServiceResources", new[] { "serviceId" });
            DropIndex("dbo.DeliveryResources", new[] { "resourceId" });
            DropIndex("dbo.DeliveryResources", new[] { "deliveryId" });
            DropIndex("dbo.Deliveries", new[] { "adminId" });
            DropTable("dbo.OrderServices");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceResources");
            DropTable("dbo.Resources");
            DropTable("dbo.DeliveryResources");
            DropTable("dbo.Deliveries");
            DropTable("dbo.Admins");
        }
    }
}
