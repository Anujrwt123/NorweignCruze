using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class BookingType
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
      
        public string Description { get; set; }
    }
}