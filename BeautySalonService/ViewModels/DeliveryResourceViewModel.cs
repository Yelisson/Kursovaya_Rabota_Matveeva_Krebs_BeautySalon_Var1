using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ViewModels
{
    public class DeliveryResourceViewModel
    {
        public int id { get; set; }
        public int deliveryId { get; set; }
        public int resourceId { get; set; }
        public int price { get; set; }
        public int count { get; set; }
        public string resourceName { get; set; }
    }
}
