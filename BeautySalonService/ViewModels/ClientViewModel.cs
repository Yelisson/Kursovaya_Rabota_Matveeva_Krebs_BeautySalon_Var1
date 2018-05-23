using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ViewModels
{
    public class ClientViewModel
    {
        public int id { get; set; }
        public string clientFirstName { get; set; }
        public string clientSecondName { get; set; }
        public int number { get; set; }
        public virtual List<OrderViewModel> Orders { get; set; }
    }
}
