using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonModel
{
   public class Service
    {
        public int id { set; get; }

        [Required]
        public string serviceName { get; set; }
        public string description { get; set; }
        [Required]
        public int price { set; get; }

        [ForeignKey("serviceId")]
        public virtual List<ServiceResource> serviceResources { get; set; }
        [ForeignKey("serviceId")]
        public virtual List<OrderService> orderServices { get; set; }
        //public virtual List<Order> Orders { get; set; }
    }
}
