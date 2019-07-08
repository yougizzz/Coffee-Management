using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date_create { get; set; }
        public double price { get; set; }
        public string img_product { get; set; }
        public bool flag { get; set; }

        public ICollection<Bill> bill { get; set; }

    }
}