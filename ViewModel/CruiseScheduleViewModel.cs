using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CruiseEntertainmentManagnmentSystem.Models;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class CruiseScheduleViewModel
    {
        public DateTime Date { get; set; }
        //public string Task { get; set; }
        public string TaskName { get; set; }
        public int CruiseID { get; set; }
        public string  CruiseName { get; set; }
        public string Color { get; set; }
        public string SubColor { get; set; }
        public string Person { get; set; }
        public int ShceduleNo { get; set; }
        public int? PersonID { get; set; }
        public int TaskID { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string Notes { get; set; }
      

    }
}