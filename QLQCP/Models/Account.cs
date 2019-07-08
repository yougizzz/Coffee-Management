using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class Account
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public string date_create { get; set; }
        public string role { get; set; }
        public bool flag { get; set; }

        public ICollection<Order> order { get; set; }
    }
}