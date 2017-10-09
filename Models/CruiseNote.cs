using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class CruiseNote
    {
        public int ID { get;set; }
        public int CruiseID { get; set; }
        public int TaskID { get; set; }
        public DateTime TaskDate { get; set; }
        
        [NotMapped]
        public string JsonDate { get; set; }
        public string Notes { get; set; }
    }
}