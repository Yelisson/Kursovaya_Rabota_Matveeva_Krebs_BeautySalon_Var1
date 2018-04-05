using BeautySalonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService
{
    [Table("AbstractDatabase")]
   public class AbstractDataBaseContext:DbContext
    {
        public AbstractDataBaseContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<DeliveryResource> DeliveryResources { get; set; }

        public virtual DbSet<Delivery> Deliverys { get; set; }

        public virtual DbSet<OrderService> OrderServices { get; set; }
        public virtual DbSet<ServiceResource> ServiceResources { get; set; }
    }
}
