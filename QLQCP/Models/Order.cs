using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public string id_user { get; set; }
        public double total { get; set; }
        public DateTime date_create  { get; set; }

        [ForeignKey("id_user")]
        public virtual Account account { get; set; }
        public ICollection<Bill> bill { get; set; }

    }
}