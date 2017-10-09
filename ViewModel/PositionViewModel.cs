using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class PositionViewModel
    {
        public int ID { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public string Payrange { get; set; }

        public string CategoryName { get; set; }
    }
}