using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonModel
{
   public class Order
    {
        public int id { get; set; }
        public int clientId { get; set; }
        //public string clientName { get; set; }
        public int number { get; set; }
        public OrderStatus status { get; set; }
        public DateTime DateCreate { get; set; }
        //public int serviceId { get; set; }
        //public string serviceName { get; set; }

        public virtual Client client { get; set; }
        //public virtual Service service { get; set; }
        [ForeignKey("orderId")]
        public virtual List<OrderService> orderServices { get; set; }
        //public virtual List<Service> services { get; set; }
    }
}
