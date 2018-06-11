using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ViewModels
{
   public class OrderViewModel
    {
        public int id { get; set; }
        public int clientId { get; set; }
        public string clientName { get; set; }
        public int number { get; set; }
        public string status { get; set; }
        public string DateCreate { get; set; }
        public List<ServiceViewModel> services { get; set; }
        public String serviceList { get; set; }
    }
}
