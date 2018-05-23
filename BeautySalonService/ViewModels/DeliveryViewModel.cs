using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ViewModels
{
    public class DeliveryViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime Date { get; set; }

        public List<DeliveryResourceViewModel> deliveryResource { get; set; }
    }
}
