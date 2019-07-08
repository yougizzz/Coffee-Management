using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class Table
    {
        [Key]
        public int id { get; set; }
        public int seats { get; set; }
        public bool flag { get; set; }

        public ICollection<TableReservation> reservation { get; set; }
    }
}