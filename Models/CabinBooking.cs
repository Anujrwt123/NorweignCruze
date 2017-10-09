using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class CabinBooking
    {
        public int ID { get; set;}

        [Display(Name="Cabin Number")]
        [Required]
        public int CabinNo { get; set; }
        [Required]
        public string Reservation { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Position { get; set; }
        
        [Display(Name="Booking Type")]
        [Required]
        public int BookingType { get; set; }

        [Display(Name="Booking Start Date")]
        [Required]
        public DateTime BookingFrom { get; set; }
        [Display(Name="Booking End Date")]
        [Required]
        public DateTime BookingTo { get; set; }
        [Required]
        public string Hotel { get; set; }
        [Required]
        public string Fleet { get; set; }
        [Required]
        public string Requestor { get; set; }

    }
}