using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
   public class DeliveryResource
    {
        public int id { get; set; }
        public int deliveryId { get; set; }
        public int resourceId { get; set; }
        public int count { get; set; }


        public virtual Delivery delivery { get; set; }

        public virtual Resource resource { get; set; }
    }
}
