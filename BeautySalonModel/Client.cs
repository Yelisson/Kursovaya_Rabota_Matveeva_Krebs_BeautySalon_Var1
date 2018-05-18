using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
   public class Client
    {
        public int id { get; set; }

        [Required]
        public string clientFirstName { get; set; }
        [Required]
        public string clientSecondName { get; set; }
        [Required]
        public int number { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }

        [ForeignKey("clientId")]
        public virtual List<Order> Orders { get; set; }
    }
}
