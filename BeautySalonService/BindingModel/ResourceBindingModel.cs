using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.BindingModels
{
   public class ResourceBindingModel
    {
        public int id { set; get; }
        public string resourceName { get; set; }
        public int sumCount { get; set; }
        public int price { get; set; }
        public virtual List<DeliveryResourceBindingModel> deliveryResources { get; set; }
    }
}
