using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Groups
    {
       public int ID{get;set;}


       //[Display(Name="Group Name")]
        [Required]
       public string GroupName{get;set;}
       
       public string Description { get; set; }
    }
}