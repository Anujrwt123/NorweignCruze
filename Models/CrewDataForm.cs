using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class CrewDataForm
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string MapsID { get; set; }
        [Required]
        public int Ship { get; set; }
        [DisplayName("Hair Color")]
        public string Hair { get; set;}
        [DisplayName("Eye Color")]
        public string Eye { get; set; }
        [DisplayName("Weight")]
        public string Weight { get; set;}
        [DisplayName("Height")]
        public string Height { get; set; }
        [DisplayName("Position/Department")]
        public string Department { get; set; }

        [DisplayName("Alien Resident Card#")]
        public string ARC { get; set; }

        [DisplayName("Name")]
        public string DependentName1 { get; set; }
        [DisplayName("Date of Birth")]
        public Nullable<DateTime> DependentDob1 { get; set; }
        [DisplayName("Name")]
        public string DependentName2 { get; set; }
        [DisplayName("Date of Birth")]
        public Nullable<DateTime> DependentDob2 { get; set; }
        [DisplayName("Name")]
        public string DependentName3 { get; set; }
        [DisplayName("Date of Birth")]
        public Nullable<DateTime> DependentDob3 { get; set; }
        [DisplayName("Name")]
        public string DependentName4 { get; set; }
        [DisplayName("Date of Birth")]
        public Nullable<DateTime> DependentDob4 { get; set; }
        [DisplayName("Name")]
        public string DependentName5 { get; set; }
        [DisplayName("Date of Birth")]
        public Nullable<DateTime> DependentDob5 { get; set; }
        [DisplayName("Sign off Date")]
        public Nullable<DateTime> SignOffDate { get; set; }

        public int position { get; set; }

        public Nullable<DateTime> LastPhyscialExamDate { get; set; }
        public string Languages { get; set; }

        public string HomeAirPort { get; set; }
    }    
}