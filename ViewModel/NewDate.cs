using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CruiseEntertainmentManagnmentSystem.Models;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class NewDate
    {
        public List<DateTime> dates { get; set; }
        public List<CabinBooking> booking { get; set; }
        public List<Cabins> Cabins { get; set; }
        public List<DateTime> dt { get; set;}
    }
}