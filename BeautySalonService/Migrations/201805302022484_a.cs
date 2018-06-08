namespace BeautySalonService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceOrders", "Service_id", "dbo.Services");
            DropForeignKey("dbo.ServiceOrders", "Order_id", "dbo.Orders");
            DropIndex("dbo.ServiceOrders", new[] { "Service_id" });
            DropIndex("dbo.ServiceOrders", new[] { "Order_id" });
            CreateTable(
                "dbo.OrderServices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        orderId = c.Int(nullable: false),
                        serviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.serviceId, cascadeDelete: true)
                .Index(t => t.orderId)
                .Index(t => t.serviceId);
            
            DropColumn("dbo.Orders", "clientName");
            DropColumn("dbo.Orders", "serviceName");
            DropTable("dbo.ServiceOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceOrders",
                c => new
                    {
                        Service_id = c.Int(nullable: false),
                        Order_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Service_id, t.Order_id });
            
            AddColumn("dbo.Orders", "serviceName", c => c.String());
            AddColumn("dbo.Orders", "clientName", c => c.String());
            DropForeignKey("dbo.OrderServices", "serviceId", "dbo.Services");
            DropForeignKey("dbo.OrderServices", "orderId", "dbo.Orders");
            DropIndex("dbo.OrderServices", new[] { "serviceId" });
            DropIndex("dbo.OrderServices", new[] { "orderId" });
            DropTable("dbo.OrderServices");
            CreateIndex("dbo.ServiceOrders", "Order_id");
            CreateIndex("dbo.ServiceOrders", "Service_id");
            AddForeignKey("dbo.ServiceOrders", "Order_id", "dbo.Orders", "id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceOrders", "Service_id", "dbo.Services", "id", cascadeDelete: true);
        }
    }
}
