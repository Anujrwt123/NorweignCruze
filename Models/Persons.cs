using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Persons
    {
        public int ID { get; set; }
        [DisplayName("Legal First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Legal Last Name")]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Alias { get; set; }

        public string Color { get; set; }
        //public double WeeklySalary { get; set; }
        //public double DayRate { get; set; }
        [NotMapped]
        [Required]
        [DisplayName("Confirm Password")]
        [Compare("Password",ErrorMessage ="password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string FullName { get; set;}
      
        //applied to run the code only need to change
        [NotMapped]
        public string Checked { get; set; }
        
       

    }
}