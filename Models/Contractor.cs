using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Contractor
    {

        public int ID { get; set; }
        [DisplayName("Initiated Date")]
        public DateTime InitiatedDate{get;set;}

        //[DisplayName("Name")]
        //public string Name { get; set; }
        [DisplayName("position")]
        public int position { get; set; }
        
        [DisplayName("ship")]
        public int ship { get; set; }
        
        [DisplayName("Shows")]
        public string Shows { get; set; }
        
        [DisplayName("Term")]
        public string Term { get; set; }
        
        [DisplayName("Weekly Salary")]
        public double BaseSalary { get; set; }
        
        [DisplayName("Days On Land")]
        public int Days_On_Land { get; set; }
        
        [DisplayName("Per Diem On Land/Travel")]
        public int Per_Diem_On_Land { get; set; }
        
        [DisplayName("Days On Board")]
        public int Days_OnBoard { get; set; }
        
        [DisplayName("Per Diem On Board")]
        public double Per_Diem_On_Board { get; set; }
        
        [DisplayName("Total Per Diem On Land")]
        public double Total_Per_Diem_On_Land { get; set; }
        
        [DisplayName("Total Per Diem On Board")]
        public double Total_Per_Diem_On_Board { get; set; }
        
        [DisplayName("Total Per Diem")]
        public double Total_Per_Diem { get; set; }
        
        [DisplayName("Total Fee")]
        public double Total_Fee { get; set; }
        
        
        [DisplayName("Payment 1")]
        public double Payment_1 { get; set; }
        
        [DisplayName("Payment 2")]
        public double Payment_2 { get; set; }
        
        [DisplayName("Payment2 Date")]
        public DateTime? Payment_2_Date { get;set; }
        
        [DisplayName("Payment1 Date")]
         public DateTime Payment_1_Date { get; set; }
        
        [DisplayName("Day Rate")]
        public double Day_Rate { get; set; }
        
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        
        [DisplayName("Tampa Dates")]
        public string TampaDates { get; set; }
        
        [DisplayName("Ship Dates")]
        public string shipDates { get; set; }
        
        [DisplayName("Address")]
        public string Address { get; set; }
        
        [DisplayName("State")]
        public string State { get; set; }
         
        [DisplayName("City")]
        public string City { get; set; }
        
        [DisplayName("Zip")]
        public string Zip { get; set; }
        
        [DisplayName("Phone")]
        public string Phone { get; set; }
        
        [DisplayName("SSN")]
        
        public string SSN { get; set; }
        
        [DisplayName("Email")]
        public string Email { get; set; }

        
        [DisplayName("Payment 3")]
        public double Payment_3 { get; set; }
         
        [DisplayName("Payment 4")]
        public double Payment_4 { get; set; }
        
        [DisplayName("Payment 5")]
        public double Payment_5 { get; set; }
           
        [DisplayName("Payment3 Date")]
        public DateTime? Payment_3_Date
           {
               get;
               set;
           }
            
        [DisplayName("Payment4 Date")]
        public DateTime? Payment_4_Date { get; set; }
           
        [DisplayName("Payment5 Date")]
        public DateTime? Payment_5_Date { get; set; }


        [DisplayName("Days Travel")]
       public int Days_On_Travel { get; set; }
        
        [DisplayName("Total Per Diem Travel")]
        public double Total_Per_Diem_On_Travel { get; set; } 
      


        public int ContractorType { get; set; }


        [DisplayName("Hours Per Week")]
        public int? Hours_per_week { get; set; }
        [DisplayName("Hours Rate")]
        public double? Hourly_Rate { get; set; }
        [DisplayName("Hours Per Day")]
        public int? Hours_per_day { get; set; }

        //public int TermDateYear { get;set; }
        //public int ShipDateYear { get; set; }
        //public int TampaDateYear { get; set; }

        [DisplayName("Agreement Type")]
        public int AgreementType { get; set; }
        public int Person { get;set;}

        public string ContractorAgreement { get; set; }

        [NotMapped]
        public string ShowsList { get; set; }


    }
}