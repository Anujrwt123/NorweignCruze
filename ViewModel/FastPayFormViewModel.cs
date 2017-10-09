using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class FastPayFormViewModel
    {
        public int ID { get; set; }
        public string ContactName { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankPhoneNumber { get; set; }
        public string BankAccount { get; set; }
        public string BankRouting { get; set; }
        public string NameInBank { get; set; }
        public string Email { get; set; }
        public string PrintedName { get; set; }
        public int PersonID { get; set; }


        public string FullName { get; set; }
        public string Address { get; set; }
        public string CityState { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

    }
}