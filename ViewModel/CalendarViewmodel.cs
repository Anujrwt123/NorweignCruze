using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CruiseEntertainmentManagnmentSystem.Models;

namespace CruiseEntertainmentManagnmentSystem.ViewModel
{
    public class CalendarViewmodel
    {
        public List<CruiseScheduleViewModel> CruiseViewModel { get; set; }
        public List<CruiseScheduleViewModel> Subtasks { get; set; }
        
        public List<IGrouping<int, CruiseScheduleViewModel>> Notes { get; set; }
        public List<IGrouping<int, CruiseScheduleViewModel>> YearlyCruiseViewModel { get; set; }
        public List<IGrouping<int, CruiseScheduleViewModel>> YearlySubtasks { get; set; }
    }
}