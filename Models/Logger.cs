using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class Logger
    {

        string _path;
        public static Logger _Instance;

        public static Logger Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Logger();
                }
                return _Instance;
            }
        }


        public string FilePath
        {
            get
            {
                if(_path==null)
                {
                    _path=HttpContext.Current.Server.MapPath("~/Data/Error.txt");
                }
                
                //FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                return _path;
            }

        }



        public void Log(string message)
        {
            string path = FilePath;
           // FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine("\n-------------------------------Log--------------------------------------\n");
            sw.WriteLine("Message Logged At: " + DateTime.Now);
            sw.WriteLine("Message Details:  " + message);
            sw.Flush();
            sw.Close();

        }


        public void LogException(Exception ex)
        {
            StreamWriter sw = new StreamWriter(FilePath, true);
            sw.WriteLine("\n-------------------------------Log--------------------------------------\n");

            sw.WriteLine("Message Logged At: " + DateTime.Now);
            sw.WriteLine("Exception Details: \n \n \n Message : " + ex.Message);

            sw.WriteLine("\n\nException Source:  " + ex.Source);

            sw.WriteLine("\n\nException StackTrace:  " + ex.StackTrace);
            sw.Flush();
            sw.Close();
        }



    }
}