using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.BindingModels
{
   public class AdminBindingModel
    {
        public int id { get; set; }
        public string adminFirstName { get; set; }
        public string adminSecondName { get; set; }
        public int number { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public virtual List<DeliveryBindingModel> delivers { get; set; }
    }
}
