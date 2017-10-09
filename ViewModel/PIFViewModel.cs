using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class PIFViewModel
    {
        public int ID { get; set; }
        public int PersonID { get; set; }

        public int CurrentAddressStay { get; set; }

        public string SecondaryPassportNumber { get; set; }

        public string SecondaryIssueCountry { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> SecondaryIssueDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString = "{0:d}")]
        public Nullable<DateTime> SecondaryExpireDate { get; set; }


        public bool IsUsCitizen { get; set; }

        public bool IsUsResident { get; set; }
        public int Position { get; set; }
        public int Ship { get; set; }

        public bool IsCriminalRecord { get; set; }

        public string CriminalCaseExplaination { get; set; }

        public bool IsDismissed { get; set; }

        public string DismissCruiseExplaination { get; set; }

        public bool IsVisaAssignToOther { get; set; }

        public string ToWhomCruise { get; set; }

        public bool IsKnowRestrictions { get; set; }

        public string ReasonOfRestriction { get; set; }

        public bool IsSeamanBook { get; set; }

        public string IssuingAuthority { get; set; }

        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> SeamanExpiryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FormCompleteDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }


        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Dob { get; set; }
        public string BirthPlace { get; set; }
        public string Nationality { get; set; }
        public int MaritalStatus { get; set; }
        public string PassPortNumber { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> IssueDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> ExpireDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> DateOfHire { get; set; }

        public string ClosestAirport { get; set; }
        public string ARC { get; set; }
        public string SSN { get; set; }
        public string EmergencyContactName { get; set; }
        [DisplayName("Emergency Relation")]
        public string EmergencyRelation { get; set; }
        [Display(Name = "Emergency Address")]
        public string EmergencyAddress { get; set; }
        [Display(Name = "Emergency Phone")]
        public string EmergencyPhone { get; set; }
        [EmailAddress]
        [Display(Name = "Emergency Email")]
        public string EmergencyEmail { get; set; }

        public string EveningTelePhone { get; set; }
        public string BestTimetoCall { get; set; }
        public string PositionName { get; set; }
        public List<Models.Cruises> Ships { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> VisaIssueDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> VisaIssueExpireDate { get; set; }
    }



}
