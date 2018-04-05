﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
   public class Order
    {
        public int id { get; set; }
        public int clientId { get; set; }
        public string clientName { get; set; }
        public int serviceId { get; set; }
        public string serviceName { get; set; }
        public int number { get; set; }
        public OrderStatus status { get; set; }
        public DateTime DateCreate { get; set; }
        public int adminId { get; set; }

        public virtual Client client { get; set; }

        public virtual Service service { get; set; }

        public virtual Admin admin { get; set; }

    }
}
