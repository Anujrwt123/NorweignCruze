using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class TRFModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CruiseID { get; set; }
        public string CruiseName { get; set; }
    }
}