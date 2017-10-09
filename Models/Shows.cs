using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Shows
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Ship { get; set; }

        [NotMapped]
        public string Checked{get;set;}

    }
}