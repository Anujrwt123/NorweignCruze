using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class UResponse
    {


         static UResponse _instance;

        public static UResponse Instance
        {
            get
            {
                if(_instance==null)
                {
                    _instance = new UResponse();
                }

                return _instance;
            }
        }

        public string Result {get; set;}
        public string Data { get; set; }


        public string JsonResponse(string _Result, string _Data)
        {
            UResponse ur = new UResponse();
            ur.Result = _Result;
            ur.Data = _Data;
            var obj = new JavaScriptSerializer<UResponse>().Serialize(ur);
            return obj;
        }


        


    }
}