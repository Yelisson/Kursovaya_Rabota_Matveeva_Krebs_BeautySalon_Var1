using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.BindingModels
{
   public class DeliveryResourceBindingModel
    {
        public int id { get; set; }
        public int deliveryId { get; set; }
        public int resourceId { get; set; }
        public int count { get; set; }
    }
}
