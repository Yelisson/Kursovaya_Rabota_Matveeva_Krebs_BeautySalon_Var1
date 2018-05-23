using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
  public  class OrderService
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int serviceId { get; set; }
        public int count { get; set; }
        
        public virtual Order order { get; set; }

        public virtual Service Service { get; set; }
    }
}
