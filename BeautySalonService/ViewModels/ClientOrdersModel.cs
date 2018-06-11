using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ViewModels
{
   public class ClientOrdersModel
    {
        public string clientFirstName { get; set; }
        public string clientSecondName { get; set; }

        public string DateCreate { get; set; }

        //public string serviceNames { get; set; }
        public string serviceListStr { get; set; }
        public string resourceListStr { get; set; }

        public string status { get; set; }


        public List<ServiceViewModel> serviceList { get; set; }

        public List<ResourceViewModel> resourceList { get; set; }
      
    }
}
