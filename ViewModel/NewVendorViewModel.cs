using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class NewVendorViewModel
    {
        public int ID { get; set; }
        public int IsNewVendor { get; set; }
        public Nullable<DateTime> DateOfRequest { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string TaxIdNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string PaymentTerms { get; set; }
        public int RequestMode { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string ABANumber { get; set; }
        public string SwiftCode { get; set; }
        public string IBANNumber { get; set; }
        public int IsSignedOff { get; set; }
        public int IsIRSW9Attached { get; set; }
        public string PreparedBy { get; set; }
        public string Department { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string Signature { get; set; }
        public int IsVendorNameChecked { get; set; }
        public int IsVendorAddressChecked { get; set; }
        public string ReviewedByForName { get; set; }
        public Nullable<DateTime> DateForName { get; set; }
        public string ForNameSignature { get; set; }
        public int IsCreatedinPplSoft { get; set; }
        public string ReviewedByforPPlsoft { get; set; }
        public Nullable<DateTime> ReviewDateforPPlsoft { get; set; }
        public string ForPPLSoftSignature { get; set; }
        public string ReviewedByForAuthT { get; set; }
        public Nullable<DateTime> DateForAuthT { get; set; }
        public string ForAuthTsignature { get; set; }
        public int PersonID { get; set; }
    }
}