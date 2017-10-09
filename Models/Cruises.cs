using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Cruises
    {
        public int ID{get;set;}
        [Required]
        public string Name {get;set;}      
        [DisplayName("Ship Brand")] 
        public string ShipBrand { get; set; }
        public string Description { get; set; }
        public string AccountNumber { get; set; }
    }
}