using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ViewModels
{
    public class ServiceViewModel
    {
        public int id { get; set; }
        public string serviceName { get; set; }
        public int price { get; set; }
        public List<ServiceResourceViewModel> ServiceResources { get; set; }

       

    }
}
