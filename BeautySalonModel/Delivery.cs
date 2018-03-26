using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
   public class Delivery
    {
        public int id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int adminId { set; get; }

        [ForeignKey("deliveryId")]
        public virtual List<DeliveryResource> deliveryResources { get; set; }
    }
}
