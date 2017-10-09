using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class CabinBookingViewModel
    {
        public int ID { get; set; }
        
        public int CabinNo { get; set; }

        public string CabinName { get; set; }

        public string Reservation { get; set; }
        
        public string Name { get; set; }
        
        public int Position { get; set; }
        public string PositionName { get; set; }
        
        public int BookingType { get; set; }
        public string BookingTypeName { get; set; }

        public DateTime BookingFrom { get; set; }
        
        public DateTime BookingTo { get; set; }
        
        public string Hotel { get; set; }
        
        public string Fleet { get; set; }
        
        public string Requestor { get; set; }
        
        
    }
}