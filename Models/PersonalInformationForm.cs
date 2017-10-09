using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class PersonalInformationForm
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        [DisplayName(" How long have you lived at your current address?")]
        public int CurrentAddressStay { get; set; }
        [DisplayName("Passport#")]
        public string SecondaryPassportNumber { get; set; }
        [DisplayName("Issuing Country")]
        public string SecondaryIssueCountry { get; set;}
        [DisplayName("Issue Date")]
        public Nullable<DateTime> SecondaryIssueDate { get; set; }
        [DisplayName("Expire Date")]
        public Nullable<DateTime> SecondaryExpireDate { get; set; }

        [DisplayName("Are you a US Citizen?")]
        public bool IsUsCitizen{ get; set; }
        [DisplayName("Are you a US Resident?")]
        public bool IsUsResident { get; set; }
        public int Position { get; set; }
        public int Ship { get; set; }
        [DisplayName("Have you ever been convicted of a felony or criminal misdemeanor?")]
        public bool IsCriminalRecord{get;set;}
        [DisplayName("If yes, please provide a brief explanation of the conviction")]
        public string CriminalCaseExplaination { get; set; }
        [DisplayName("Have you ever been dismissed from a cruise vessel?")]
        public bool IsDismissed { get; set; }
        [DisplayName("If yes, please provide a brief explanation and dates")]
        public string DismissCruiseExplaination { get; set;}        
        [DisplayName("Is your visa assigned to another cruise line?")]
        public bool IsVisaAssignToOther { get; set; }
        [DisplayName("If so, to whom?")]
        public string ToWhomCruise { get; set; }
        [DisplayName("Do you have any known restrictions as to entry into countries?")]
        public bool IsKnowRestrictions{ get; set; }
        [DisplayName("If yes, which countries and why?")]
        public string ReasonOfRestriction { get; set; }
        [DisplayName("Do you have a Seaman’s Book?")]
        public bool IsSeamanBook { get; set; }
        [DisplayName("If yes, what is the issuing Authority?")]
        public string IssuingAuthority { get; set; }
        [DisplayName("Seaman’s Expiry Date")]
        public DateTime SeamanExpiryDate { get; set; }

        [DisplayName("Alien card number")]
        public string ARC { get; set; }


    }
}