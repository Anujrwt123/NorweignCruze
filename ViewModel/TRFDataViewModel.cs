using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CruiseEntertainmentManagnmentSystem.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
   public class TRFDataViewModel
    {
        public int ID { get; set; }

        [DisplayName("Departure City")]
        public string DepartureCity { get; set; }
        [DisplayName("Departure Date")]
        public DateTime DepartureDate { get; set; }
        [DisplayName("Arrival City")]
        public string ArrivalCity { get; set; }
        [DisplayName("Arrival Date")]
        public DateTime ArrivalDate { get; set; }
        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        public string Notes { get; set; }
        public int Person { get; set; }
        [NotMapped]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [NotMapped]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [NotMapped]

        public DateTime? DOB { get; set; }
        [NotMapped]
        public int Gender { get; set; }
        [NotMapped]
        public string Nationality { get; set; }
        [NotMapped]
        public int Cruise { get; set; }

       [NotMapped]
        public bool IsFemale { get; set; }
    }
}