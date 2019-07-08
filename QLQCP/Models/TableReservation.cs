using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class TableReservation
    {
        [Key]
        public int id { get; set; }
        public int id_table { get; set; }
        public int seats { get; set; }
        public string customer_telephone { get; set; }
        public string fullname { get; set; }
        public DateTime time_create { get; set; }
        public bool checkin { get; set; }
        public bool cancel { get; set; }
        public bool flag { get; set; }


        [ForeignKey("id_table")]
        public virtual Table table { get; set; }


        [ForeignKey("customer_telephone")]
        public virtual MemberShip membership { get; set; }
    }
}