using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class ContractorViewModel
    {
        public int ID { get; set; }
        public DateTime InitiatedDate { get; set; }
        public string Name { get; set; }
        public int position { get; set; }
        public int ship { get; set; }
        public string Shows { get; set; }
        public string Term { get; set; }
        public double BaseSalary { get; set; }
        public int Days_On_Land { get; set; }
        public int Per_Diem_On_Land { get; set; }
        public int Days_OnBoard { get; set; }
        public double Per_Diem_On_Board { get; set; }
        public double Total_Per_Diem_On_Land { get; set; }
        public double Total_Per_Diem_On_Board { get; set; }
        public double Total_Per_Diem { get; set; }
        public double Total_Fee { get; set; }
        public double? Payment_1 { get; set; }
        public double? Payment_2 { get; set; }
        public DateTime? Payment_2_Date { get; set; }
        public double Day_Rate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TampaDates { get; set; }
        public string shipDates { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string ShipName { get; set; }
        public string PositionName{ get; set; }
        public int ContractorType { get; set; }
        public int AgreementType{ get; set; }
    }
}