using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonModel
{
   public class Client
    {
        public int id { get; set; }

        [Required]
        public string clientFirstName { get; set; }
        [Required]
        public string clientSecondName { get; set; }
        public int number { get; set; }
        public string password { get; set; }
        public string mail { get; set; }

        [ForeignKey("clientId")]
        public virtual List<Order> Orders { get; set; }
    }
}
