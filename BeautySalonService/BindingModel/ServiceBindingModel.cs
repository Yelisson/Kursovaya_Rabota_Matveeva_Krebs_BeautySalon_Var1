using BeautySalonService.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySalonService.ViewModels;

namespace BeautySalonService.BindingModels
{
    public class ServiceBindingModel
    {
        public int id { set; get; }
        public string serviceName { get; set; }
        public string description { get; set; }
        public int price {get; set;}
        public int count { get; set; }
        public virtual List<OrderServiceBindingModel> OrderServices { get; set; }

        public virtual List<ResourceBindingModel> Resources { get; set; }
        public virtual List<ServiceResourceBindingModel> serviceResources { get; set; }
        public virtual List<ServiceResourceBindingModel> ServiceResources { get; set; }
    }
}
