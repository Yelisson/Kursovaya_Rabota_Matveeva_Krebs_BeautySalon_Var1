using BeautySalonModel;
using System.Data.Entity;

namespace BeautySalonService
{
   // [Table("AbstractDatabase")]
   public class AbstractDataBaseContext:DbContext
    {
        public AbstractDataBaseContext(): base("BeautySalon")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<DeliveryResource> DeliveryResources { get; set; }

        public virtual DbSet<Delivery> Deliverys { get; set; }

        public virtual DbSet<ServiceResource> ServiceResources { get; set; }

        public virtual DbSet<OrderService> OrderServices { get; set; }
        public virtual DbSet<MessageInfo> MessageInfos { get; set; }
    }
}
