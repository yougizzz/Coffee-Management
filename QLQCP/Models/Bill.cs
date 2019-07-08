using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class Bill
    {
        [Key]
        public int id { get; set; }
        public int id_order { get; set; }
        public DateTime date_create  { get; set; }
        public int id_product { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double total { get; set; }

        [ForeignKey("id_order")]
        public virtual Order order { get; set; }

        [ForeignKey("id_product")]
        public virtual Product product { get; set; }

    }
}