using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.BindingModels
{
   public class OrderBindingModel
    {
        public int id { get; set; }
        public int clientId { get; set; }
        public string clientName { get; set; }
        public int serviceId { get; set; }
        public string serviceName { get; set; }
        public int number { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
