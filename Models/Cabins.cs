using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Cabins
    {
       public int ID{get;set;} 
        [Display(Name="Cabin Name")]
        [Required]
       public string CabinName{get;set;}
        [Display(Name="Cabin Category")]
        [Required]
       public int CatName{get;set;}

       [Display(Name="Cabin Type")]
       [Required]
       public string CabinType {get; set; }
    }
}