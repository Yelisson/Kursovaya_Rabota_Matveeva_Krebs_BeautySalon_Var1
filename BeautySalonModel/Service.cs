﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
   public class Service
    {
        public int id { set; get; }

        [Required]
        public string serviceName { get; set; }
        public string description { get; set; }
        public int price { set; get; }

        [ForeignKey("serviceId")]
        public virtual List<OrderService> OrderServices { get; set; }

        [ForeignKey("serviceId")]
        public virtual List<Resource> Resources { get; set; }
    }
}
