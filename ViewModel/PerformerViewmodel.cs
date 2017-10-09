using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class PerformerViewmodel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }      
        public int PGroup { get; set; }
        public string GroupName { get; set; }
    }
}