using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLQCP.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        
        public string username { get; set; }
        public string fullname { get; set; }
        public string date_of_birth { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string id_card { get; set; }
        public bool flag { get; set; }

        [ForeignKey("username")]
        public virtual Account account { get; set; }

    }
}