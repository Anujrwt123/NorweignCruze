using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Booking
    {
        public int ID { get; set; }

        [Display(Name="Cabin Number")]
      
        public string Cabinno { get; set; }
        [Display(Name="Booking Date")]
      
        public DateTime BookingDate { get; set; }

        public bool Status { get; set; }
    }
}