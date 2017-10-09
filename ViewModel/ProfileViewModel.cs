using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class ProfileViewModel
    {

        public int ID { get; set; }
        public int PersonID { get; set; }        
        public string NickName { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string WorkPhone { get; set; }
        public string SSN { get; set; }
        public string SecondaryEmail { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PayRate { get; set; }
        public DateTime DOB { get; set; }
        public int Sex { get; set; }
        public int MaritalStatus { get; set; }
        public string BirthPlace { get; set; }
        public string CitizenShip { get; set; }
        public string OtherCitizenShip { get; set; }
        public string Languages { get; set; }
        public bool IsMedicalCurrent { get; set; }
        public string LastMedicalExam { get; set; }
        public DateTime FirstHireDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyRelation { get; set; }
        public string EmergencyAddress { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyEmail { get; set; }
        public string OtherInfo { get; set; }
        public string HomeAirPort { get; set; }
        public string PassportNumber { get; set; }
        public string IssueCountry { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string VisaNumber { get; set; }
        public DateTime VisaIssueDate { get; set; }
        public DateTime VisaExpireDate { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}