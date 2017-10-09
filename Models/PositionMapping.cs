using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class PositionMapping
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int PositionID { get; set; }
        public int CategoryID { get; set; }
    }
}