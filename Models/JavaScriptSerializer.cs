using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class JavaScriptSerializer<T>
    {

        static JavaScriptSerializer _instance;

        public static JavaScriptSerializer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JavaScriptSerializer();
                }

                return _instance;
            }
        }


        public string Serialize(T obj)
        {
            var result = new JavaScriptSerializer().Serialize(obj);

            return result;
        }


        public T  DeSerialize(string input)
        {
            var result = new JavaScriptSerializer().Deserialize<T>(input);

            return result;
        }

    }
}