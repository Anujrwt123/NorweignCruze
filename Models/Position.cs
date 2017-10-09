using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Position
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
     
        public string Description { get; set; }

        public int CategoryID { get; set; }

        public string Payrange { get; set; }


        [NotMapped]
        public string Checked { get; set; }

    }
}