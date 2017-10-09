using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class TRF
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
        
        public int Cruise { get; set; }
        public string Notes { get; set; }
        public int Person { get; set; }
       
    }
}