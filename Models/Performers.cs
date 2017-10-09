using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Performers
    {
        public int ID{get;set;} 
        [Required]
        public string Name{get;set;} 
        [Required]
        public string Alias {get;set;}

        [DisplayName("Group")]
        public int PGroup { get; set; }
    }
}