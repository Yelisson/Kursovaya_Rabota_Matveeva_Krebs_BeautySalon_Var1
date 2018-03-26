using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonModel
{
   public class Admin
    {
        public int id { get; set; }

        [Required]
        public string adminFirstName { get; set; }
        public string adminSecondName { get; set; }
        public int number { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        [ForeignKey("adminId")]
        public virtual List<Delivery> delivers { get; set; }
    }
}
