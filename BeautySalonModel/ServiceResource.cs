using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
    public class ServiceResource
    {
        public int id { get; set; }
        public int serviceId { get; set; }
        public int resourceId { get; set; }
        public int count { get; set; }


        public virtual Service service { get; set; }

        public virtual Resource resource { get; set; }
    }
}
