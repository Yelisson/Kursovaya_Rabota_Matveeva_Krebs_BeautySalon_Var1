using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.BindingModels
{
   public class OrderServiceBindingModel
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int serviceId { get; set; }
        public int count { get; set; }
    }
}
