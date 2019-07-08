using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class MemberShip
    {
        [Key]
        public string phone_number { get; set; }
        public string fullname { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public int score { get; set; }
        public bool flag { get; set; }

        public ICollection<TableReservation> reservation { get; set; }

    }
}