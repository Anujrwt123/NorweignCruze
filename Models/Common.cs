using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{

    public class GroupedData<T>
    {
        public int GroupedBy{get;set;}
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
    }


    public class CommonType
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }
    public class Common
    {
         
        public static List<CabinType> CabinTypes()
        {
            List<CabinType> Cabins = new List<CabinType>()
            {
                new CabinType(){ID="Cabin Type 1",Value="Cabin Type 1"},
                new CabinType(){ID="Cabin Type 2",Value="Cabin Type 2"},
            new CabinType(){ID="Cabin Type 3",Value="Cabin Type 3"},
            new CabinType(){ID="Cabin Type 4",Value="Cabin Type 4"},
            };

            return Cabins.OrderBy(x=>x.Value).ToList();
        }

        public static List<ContractorAgreement> ContractorAgree()
        {

            List<ContractorAgreement> contractagre = new List<ContractorAgreement>()
            {
                new ContractorAgreement() { ID ="ncl", Value="NCL" },
                new ContractorAgreement() { ID ="Oceana", Value="OCEANA" },
                new ContractorAgreement() { ID ="Regent", Value="REGENT" }
            };

            return contractagre.ToList();

        }

        public  static List<AgreementType> Agreements()
        {
            List<AgreementType> agreements = new List<AgreementType>()
            {
                new AgreementType(){ID=1,Value="Independent Contractor Agreement"},
                new AgreementType(){ID=2,Value="Production Agreement"},
            new AgreementType(){ID=3,Value="Production Agreement With Royality"}
            };

            return agreements.OrderBy(x=>x.Value).ToList();
        }

        public static List<StatusType> GetStatus()
        {
            List<StatusType> status= new List<StatusType>()
            {
                new StatusType(){ID=1,Value="Single"},
                new StatusType(){ID=2,Value="Married"},
            new StatusType(){ID=3,Value="Divorced"},
            new StatusType(){ID=3,Value="Widowed"},
            new StatusType(){ID=3,Value="Separated"}
            };

            return status;
        }

    }

    public class ContractorAgreement
    {
        public String ID { get; set; }
        public String Value { get; set; }
    }

    public class AgreementType
    {

        public int ID { get; set; }
        public string Value { get; set; }
    }
    public class CabinType
    {
        public string ID { get; set; }
        public string Value { get; set; }
    }


    public class StatusType
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }
}