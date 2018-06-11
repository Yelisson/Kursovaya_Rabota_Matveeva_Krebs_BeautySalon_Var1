using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonModel
{
   public class Order
    {
        public int id { get; set; }
        public int clientId { get; set; }
        public int number { get; set; }
        public OrderStatus status { get; set; }
        public DateTime DateCreate { get; set; }

        public virtual Client client { get; set; }
        [ForeignKey("orderId")]
        public virtual List<OrderService> orderServices { get; set; }
    }
}
