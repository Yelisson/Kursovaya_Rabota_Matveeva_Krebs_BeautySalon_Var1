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
        public string clientSecondName { get; set; }
        public int number { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        [ForeignKey("clientId")]
        public virtual List<Order> Orders { get; set; }
    }
}
