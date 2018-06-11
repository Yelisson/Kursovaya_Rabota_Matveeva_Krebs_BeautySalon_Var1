using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ViewModels
{
   public  class DeliverysLoadViewModel
    {
        public string deliveryName { get; set; }

        //public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Resources { get; set; }
    }
}
