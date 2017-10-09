using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class CrewDataFormViewModel
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public string MapsID { get; set; }
        [Required]
        public int Ship { get; set; }
        [DisplayName("Hair Color")]
        public string Hair { get; set; }
        [DisplayName("Eye Color")]
        public string Eye { get; set; }
        [DisplayName("Weight")]
        public string Weight { get; set; }
        [DisplayName("Height")]
        public string Height { get; set; }
        [DisplayName("Position/Department")]
        public string Department { get; set; }

        [DisplayName("Alien Resident Card#")]
        public string ARC { get; set; }

        [DisplayName("Name")]
        public string DependentName1 { get; set; }
        [DisplayName("Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> DependentDob1 { get; set; }
        [DisplayName("Name")]
        public string DependentName2 { get; set; }
        [DisplayName("Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> DependentDob2 { get; set; }
        [DisplayName("Name")]
        public string DependentName3 { get; set; }
        [DisplayName("Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> DependentDob3 { get; set; }
        [DisplayName("Name")]
        public string DependentName4 { get; set; }
        [DisplayName("Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> DependentDob4 { get; set; }
        [DisplayName("Name")]
        public string DependentName5 { get; set; }
        [DisplayName("Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> DependentDob5 { get; set; }
        [DisplayName("Sign off Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> SignOffDate { get; set; }

        [Required]
        public int position { get; set; }

        [DisplayName("Last Physical Exam Date")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:d}")]
        public Nullable<DateTime> LastPhyscialExamDate { get; set; }

        [DisplayName("Languages (other than English)")]
        public string Languages { get; set; }

        [DisplayName("Home Airport")]
        public string HomeAirPort { get; set; }


        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string SSN { get; set; }        
        public int Sex{get;set;}
        public int MaritalStatus { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Dob { get; set; }
        public string BirthPlace { get; set; }
        public string PassportNumber { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime IssueDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime ExpireDate { get; set; }
        public string IssueCountry { get; set; }
        public string VisaNumber { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime VisaExpireDate { get; set; }       
        public string PositionName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime HireDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyAddress{ get; set; }
        public string EmergencyEmail { get; set; }
        public string EmergencyRelation { get; set; }
        public string EmergencyPhone { get; set; }


        public string ShipName { get; set; }

        public string Zip { get; set; }

    }
}