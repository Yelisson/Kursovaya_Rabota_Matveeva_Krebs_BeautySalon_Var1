using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.BindingModels
{
    public class ClientBindingModel
    {
        public int id { get; set; }
        public string clientFirstName { get; set; }
        public string clientSecondName { get; set; }
        public int number { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public virtual List<OrderBindingModel> Orders { get; set; }
    }
}
