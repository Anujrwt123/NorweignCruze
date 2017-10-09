using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Category
    {
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
        [DisplayName("Pay Rate")]
        public double Payrate { get; set; }

        [NotMapped]
        public string Checked { get; set; }
    }
}