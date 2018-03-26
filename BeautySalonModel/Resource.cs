using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
   public class Resource
    {
        public int id { set; get; }

        [Required]
        public string resourceName { get; set; }
        public int sumCount { get; set; }
        public int price { get; set; }
        public int serviceId { get; set; }

        [ForeignKey("resourceId")]
        public virtual List<DeliveryResource> deliveryResources { get; set; }

    }
}
