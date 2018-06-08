using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.BindingModels
{
   public class DeliveryBindingModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public string mail { get; set; }
        public DateTime Date { get; set; }
        public virtual List<DeliveryResourceBindingModel> deliveryResources { get; set; }
    }
}
