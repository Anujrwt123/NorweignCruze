using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class CruiseSubSchedule
    {
        public int ID { get; set; }
        public string TaskID { get; set; }
        public DateTime TaskDate { get; set; }
        public int CruiseID { get; set; }
        public int PersonID { get; set; }
        public int ScheduleNo { get; set; }
    }
}