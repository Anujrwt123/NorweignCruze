using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class SessionContext<T> where T:class
    {

        public static SessionContext<T> _session;


        public static SessionContext<T> Instance
        {
            get
            {
                return _session ?? (_session = new SessionContext<T>());
            }
        }


        public T GetSession(string sessionname)
        {

            if(HttpContext.Current.Session[sessionname]!=null)
            {
                return HttpContext.Current.Session[sessionname] as T;
            }
            else
            {
                HttpContext.Current.Response.Redirect("/Account/Login");
                return null;
            }
            //return null;
        }


        
        public void SetSession(string SessionName,T obj)
        {
            HttpContext.Current.Session[SessionName] = obj;
        }


    }
}