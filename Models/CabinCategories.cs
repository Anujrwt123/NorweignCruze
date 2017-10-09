using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class CabinCategories
    {
       public int ID {get;set;}
       [Display(Name="Category Name")]
        [Required]
       public string CategoryName {get;set;}
       
       public string Description { get; set; }
    }
}