using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class PersonalInformation
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        [Display(Name = "Nick Name")]
        public string NickName { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Work Phone")]
        public string WorkPhone { get; set; }
        [Display(Name = "SSN")]
        public string SSN { get; set; }
        [EmailAddress]
        [Display(Name = "Secondary Email")]
        public string SecondaryEmail { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip{ get; set; }

        [Display(Name = "Pay Rate")]
        public string PayRate { get; set; }
        [Display(Name = "DOB")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:d}")]
        public DateTime DOB { get; set; }
        [Display(Name = "Sex")]
        public int Sex { get; set; }
        [Display(Name = "Marital Status")]
        public int MaritalStatus { get; set; }
        [Display(Name = "Birth Place")]
        public string BirthPlace { get; set; }
        [Display(Name = "CitizenShip")]
        public string CitizenShip { get; set; }
        [Display(Name = "Other CitizenShip")]
        public string OtherCitizenShip { get; set; }
        [Display(Name = "Languages")]
        public string Languages { get; set; }
        [Display(Name = "Is Medical Current")]
        public bool IsMedicalCurrent { get; set; }
        [Display(Name = "Last Medical Exam")]
        public string LastMedicalExam { get; set; }
        [Display(Name = "First Hire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime FirstHireDate { get; set; }
        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContactName { get; set; }
        [Display(Name = "Emergency Relation")]
        public string EmergencyRelation { get; set; }
        [Display(Name = "Emergency Address")]
        public string EmergencyAddress { get; set; }
        [Display(Name = "Emergency Phone")]
        public string EmergencyPhone { get; set; }
        [EmailAddress]
        [Display(Name = "Emergency Email")]
        public string EmergencyEmail { get; set; }
        [Display(Name = "Other Info")]
        public string OtherInfo { get; set; }
        [Display(Name = "Home Air Port")]
        public string HomeAirPort { get; set; }
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }
        [Display(Name = "Issue Country")]
        public string IssueCountry { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate{ get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Expire Date")]
        public DateTime ExpireDate{ get; set; }
        [Display(Name = "Visa Number")]
        public string VisaNumber { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Visa Issue Date")]
        public DateTime VisaIssueDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Visa Expire Date")]
        public DateTime VisaExpireDate { get; set; }
        //[Display(Name = "Booking Date")]
        public string ImagePath { get; set; }
        public int CategoryID { get; set; }

        public string Nationality { get; set; }

        [Required(ErrorMessage="Day rate is required.")]
        [Display(Name ="Day rate")]
        public double DayRate { get; set; }


        
        [Required(ErrorMessage = "Weekly salary is required.")]
        [Display(Name = "Weekly salary")]
        public double WeeklySalary { get; set; }

        [NotMapped]
        public string PositionList { get; set; }
        [NotMapped]
        public bool IsFemale { get; set; }

        

        
        
        
    }
}