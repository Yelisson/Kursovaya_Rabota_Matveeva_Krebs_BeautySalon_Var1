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
        public DateTime Date { get; set; }
        public int adminId { set; get; }
        public virtual List<DeliveryResourceBindingModel> deliveryResources { get; set; }
    }
}
