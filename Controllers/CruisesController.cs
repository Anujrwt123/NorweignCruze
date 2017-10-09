using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class CruisesController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        string _returnUrl;
        public CruisesController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("Cruises/Index");
        }
        //
        // GET: /Cruises/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Cruises()
        {
            var list = db.cruises.ToList();

            return PartialView("_Cruises",list);
        }

        //
        // GET: /Cruises/Details/5

        public ActionResult Details(int id = 0)
        {
            Cruises cruises = db.cruises.Find(id);
            if (cruises == null)
            {
                return HttpNotFound();
            }
            return View(cruises);
        }

        //
        // GET: /Cruises/Create

        public ActionResult Create()
        {
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /Cruises/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cruises cruises)
        {
            if (ModelState.IsValid)
            {
                db.cruises.Add(cruises);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(cruises);
        }

        //
        // GET: /Cruises/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cruises cruises = db.cruises.Find(id);
            if (cruises == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(cruises);
        }

        //
        // POST: /Cruises/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cruises cruises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cruises).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(cruises);
        }

        //
        // GET: /Cruises/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Cruises cruises = db.cruises.Find(id);
                db.cruises.Remove(cruises);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Cruises/Delete/5

        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                Cruises cruises = db.cruises.Find(id);
                db.cruises.Remove(cruises);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl),JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }

           
          //  return RedirectToAction("Index");
        }

        public ActionResult ShowSchedule(int ID=0)
        {
            ViewBag.ReturnUrl = _returnUrl;
            ViewBag.Cruise = new SelectList(db.cruises, "ID", "Name");
            ViewBag.CruiseID = ID;
            return View();
        }

        public ActionResult Schedule(int ID)
        {

            var d = db.CruiseTasks.Where(x=>x.ID!=0).ToList();
            var CruiseData = db.cruises.Where(c => c.ID == ID).SingleOrDefault();
            ViewBag.TaskName = new SelectList(d, "ID", "Name");
            ViewBag.Shows = new SelectList(db.shows.Where(x => x.Ship == ID), "ID", "Name");
            if (CruiseData != null)
            {
                ViewBag.CruiseName = CruiseData.Name;
                ViewBag.CruiseID = CruiseData.ID;
            }
            else
            {
                // ViewBag.CruiseName = CruiseData.Name;
            }

            return PartialView("_AddSchedule");
        }


        [HttpPost]
        public ActionResult SubmitCruiseSchedule(string model,int CruiseID)
        {
            List<CruiseSchedule> schedules = JavaScriptSerializer<List<CruiseSchedule>>.Instance.Deserialize<List<CruiseSchedule>>(model);

            var list=db.CruiseSchedules.Where(x => x.CruiseID == CruiseID).Select(x=>x.ScheduleNo).ToList();
            int scheduleno=0;
            if(list.Count>0)
            {
                scheduleno= list.Max();
            }
          
            foreach (CruiseSchedule c in schedules)
            {
                c.CruiseID = CruiseID;                
                c.ScheduleNo=scheduleno+1; 
                db.CruiseSchedules.Add(c);
            }
            db.SaveChanges();

            //List<CruiseSchedule> data = GetSchedules(CruiseID);
            //return RedirectToAction("GetSchedules", new { cruiseID = CruiseID });

            return Json("done", JsonRequestBehavior.AllowGet);

        }


        public List<CruiseNote> GetNotes(int CruiseID,int Year)
        {
            var data = db.Notes.Where(x => x.CruiseID == CruiseID && x.TaskDate.Year == Year).ToList();

            return data;
        }


        public JsonResult GetSchedules(int cruiseID,int Year=0)
        {
            if(Year==0)
            {
                Year = DateTime.Now.Year;
            }
            //var data = db.CruiseSchedules.Where(c => c.CruiseID == cruiseID && c.Date.Year==Year).ToList();

            var CruiseScheduleList = db.Database.SqlQuery<CruiseScheduleViewModel>("exec sp_GetSchedules @Year,@cruiseID", new SqlParameter("@Year", Year), new SqlParameter("@cruiseID", cruiseID)).ToList();

            //var CruiseScheduleList = (from d in db.CruiseSchedules
            //                          join
            //                              CT in db.CruiseTasks
            //                              on d.TaskID equals CT.ID                                    
            //                          orderby d.Date
            //                          select new CruiseScheduleViewModel
            //                          {
            //                              Color = CT.Color,
            //                              CruiseID = d.CruiseID,
            //                              TaskName = CT.Name,
            //                              Date = d.Date,   
            //                              ShceduleNo=d.ScheduleNo,                                          
            //                              TaskID=d.TaskID                                        
            //                          }).Where(x=>x.Date.Year==Year && x.CruiseID==cruiseID).OrderBy(x=>x.Date).ToList();

            var SubTasks = (from CS in db.CruiseSubSchedules where CS.CruiseID==cruiseID  join p in db.persons on CS.PersonID equals p.ID select new CruiseScheduleViewModel{
            PersonID=p.ID,
            Person=p.Alias,
            SubColor=p.Color,
            Date=CS.TaskDate,
            CruiseID=CS.CruiseID
            }).Where(x=>x.Date.Year==Year).OrderBy(x=>x.PersonID).ToList();

            CalendarViewmodel model = new CalendarViewmodel();
            model.CruiseViewModel = CruiseScheduleList;
            model.Subtasks = SubTasks;
            //var CruiseScheduleList = data.Join(db.CruiseTasks, s => s.TaskID, t => t.ID, (s, t) => new { CruiseTask = t, Cruiseschedule = s })
            //    .Select(x => new CruiseScheduleViewModel { TaskName = x.CruiseTask.Name, Color = x.CruiseTask.Color, Date = x.Cruiseschedule.Date }).OrderBy(x => x.Date).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        


        public ActionResult getYearlySchedule(int Year=0)
        {
           // var cruise = db.CruiseSchedules.GroupBy(c => c.CruiseID).ToList();
            if(Year==0)
            {
                Year = DateTime.Now.Year;
            }


            var list = db.Database.SqlQuery<CruiseScheduleViewModel>("exec sp_GetSchedulesForYearlyView @year", new SqlParameter("@year", Year))
                .AsEnumerable()
                .OrderBy(x => x.Date)
                .GroupBy(x => x.CruiseID).ToList();

            //var list = (from cs in db.CruiseSchedules
            //            join ct in db.CruiseTasks on cs.TaskID equals ct.ID
            //            join c in db.cruises on cs.CruiseID equals c.ID
            //            select new CruiseScheduleViewModel
            //            {
            //                CruiseID = cs.CruiseID,
            //                CruiseName = c.Name,
            //                Date = cs.TaskDate,
            //                TaskName = ct.Name,
            //                Color = ct.Color
            //            }
            //            ).Where(x => x.Date.Year == Year)
            //            .OrderBy(x => x.Date).GroupBy(x => x.CruiseID).ToList();

            var Notes = (from cs in db.Notes
                         join ct in db.CruiseTasks on cs.TaskID equals ct.ID
                         join c in db.cruises on cs.CruiseID equals c.ID
                         select new CruiseScheduleViewModel { CruiseID = cs.CruiseID, CruiseName = c.Name, Date = cs.TaskDate, TaskName = ct.Name, Color = ct.Color,Notes=cs.Notes }).Where(x => x.Date.Year == Year)
                        .OrderBy(x => x.Date).GroupBy(x => x.CruiseID).ToList();


              
            var SubTasks = db.Database.SqlQuery<CruiseScheduleViewModel>("exec SP_GetSubTasks @year", new SqlParameter("@year", Year)).ToList();
            //var SubTasks = (from CS in db.CruiseSubSchedules                            
            //                join p in db.persons on CS.PersonID equals p.ID
            //                join pm in db.PersonMappings on p.ID equals pm.PersonID
            //                join cat in db.categories on pm.CategoryID equals cat.ID                            
            //                select new CruiseScheduleViewModel
            //                {
            //                    PersonID = p.ID,
            //                    Person = p.Alias,
            //                    SubColor = p.Color,
            //                    Date = CS.TaskDate,
            //                    CruiseID = CS.CruiseID,
            //                    CategoryName = cat.Name
            //                }).Where(x => x.Date.Year == Year).OrderBy(x => x.Date).GroupBy(x=>x.CruiseID).ToList();

            

            CalendarViewmodel model = new CalendarViewmodel();

            //model.YearlySubtasks = SubTasks.GroupBy(x => x.CruiseID);
            model.YearlyCruiseViewModel = list;
            model.Subtasks= SubTasks;
            model.Notes = Notes;
            //var list = db.CruiseSchedules.OrderBy(x => x.CruiseID).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitSubSchedules(string model,int Scheduleno,int CruiseID)
        {

            //TaskId is CategoryId for subschedules....... in database


            List<CruiseSubSchedule> schedules = JavaScriptSerializer<List<CruiseSubSchedule>>.Instance.Deserialize<List<CruiseSubSchedule>>(model);

            //var list = db.CruiseSchedules.Where(x => x.CruiseID == CruiseID).Select(x => x.ScheduleNo).ToList();
            //int scheduleno = 0;
            //if (list.Count > 0)
            //{
            //    scheduleno = list.Max();
            //}
            var subSchedules = db.CruiseSubSchedules.Where(x => x.ScheduleNo == Scheduleno && x.CruiseID == CruiseID).ToList();
           
            if(subSchedules.Count>0)
            {
                
                    foreach (CruiseSubSchedule cs in schedules)
                    {
                        var sch=subSchedules.Where(x => x.TaskDate == cs.TaskDate).FirstOrDefault() ;
                        if(sch!=null)
                        {
                            db.CruiseSubSchedules.Remove(sch);
                            db.SaveChanges();
                        }
                        
                        db.CruiseSubSchedules.Add(cs);
                       // break;
                    }
               
               
            }
            else
            {
                schedules.ForEach(x => { db.CruiseSubSchedules.Add(x); });
            }

                //foreach (CruiseSubSchedule c in schedules)
                //{
                //    c.CruiseID = CruiseID;
                //    c.ScheduleNo =Scheduleno;                
                //    db.CruiseSubSchedules.Add(c);
                //}

            db.SaveChanges();
            return Json("done", JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult AddSubschedule(int cruiseID)
        {

            var cruise = db.cruises.Find(cruiseID);
            List<CommonType> schedule=new List<CommonType> ();

             schedule = db.Database.SqlQuery<CommonType>("sp_GetDataForSubSchedules @cruiseID", new SqlParameter("@cruiseID", cruiseID)).ToList();

            //var cruiseTasks = (from tasks in db.CruiseSchedules.Where(x=>x.CruiseID==cruiseID) group tasks by tasks.ScheduleNo into groupdata 
            //                  select new GroupedData<CruiseSchedule> { GroupedBy = groupdata.Key, 
            //                      Data = groupdata, 
            //                      Count = groupdata.Count() }).ToList();
                              
                              //db.CruiseSchedules.Where(x => x.CruiseID == cruiseID).GroupBy(x => x.ScheduleNo).ToList();
            //foreach (GroupedData<CruiseSchedule> GData in cruiseTasks)
            //{
            //    var CSchedule = GData.Data.ToList();
            //    var max = CSchedule.Max(x => x.TaskDate).ToShortDateString();
            //    var min = CSchedule.Min(x => x.TaskDate).ToShortDateString();
            //    string val = min + " - " + max;
            //    CommonType comn = new CommonType();
            //    comn.Value = val;
            //    comn.ID = GData.GroupedBy;
            //    schedule.Add(comn);
               
            //}


            ViewBag.CruiseName = cruise.Name;
            ViewBag.CruiseID = cruise.ID;
            ViewBag.Schedules = new SelectList(schedule,"ID","Value");
            //if(schedule.Count>0)
            //{
            //    //var data=schedule[0].Value.Split('-');
            //   // ViewBag.ShipStartDate = data[0];
            //    //ViewBag.ShipEndDate = data[1];
            //}

            ViewBag.Persons = new SelectList(db.persons.OrderBy(x=>x.FirstName).ToList(), "ID", "FirstName");
            ViewBag.TaskName = new SelectList(db.categories.OrderBy(x=>x.Name).ToList(), "ID", "Name");
            
           // ViewBag
            return PartialView("_SubSchedule");
        }





        public ActionResult Persons(int ID)
        {
            List<Persons> persons = null;
            if(ID>=0)
            {
                persons = db.Database.SqlQuery<Persons>("sp_GetPersonsByCategoryID @CategoryID", new SqlParameter("@CategoryID", ID)).ToList();
                //persons = db.persons.Join(db.PositionMappings, x => x.ID, pm => pm.PersonID, (x, pm) => new { Person = x, PersonMapping = pm })
                //    .Where(x => x.PersonMapping.CategoryID == ID)
                //    .Select(x => x.Person).ToList();
                ////persons = db.persons.Where(x => x.Category == ID).OrderBy(x=>x.Name).ToList();
            }
            
            return Json(persons, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveNotes(List<CruiseNote> model)
        {
            //var jsonSerializerSettings = new JsonSerializerSettings()
            //{
            //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            //    DateParseHandling = DateParseHandling.DateTimeOffset,
            //    DateTimeZoneHandling = DateTimeZoneHandling.Local
            //};
            
            //var list=JsonConvert.DeserializeObject<List<CruiseNote>>(model,jsonSerializerSettings);

            if (model != null && model.Count>0)
            {
                model.ForEach(x =>
                {
                    var data = db.Notes.Where(c => c.CruiseID == x.CruiseID && c.TaskDate == x.TaskDate).FirstOrDefault();
                    if(data!=null)
                    {
                        db.Notes.Remove(data);
                        db.SaveChanges();
                    }
                    db.Notes.Add(x);
                    db.SaveChanges();
                });
            }

            return Json("done", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void ExportToExcel(HtmlString data)
        {

            
            
            //var cruise = db.CruiseSchedules.GroupBy(c => c.CruiseID).ToList();
            //var list = (from cs in db.CruiseSchedules
            //            join ct in db.CruiseTasks on cs.TaskID equals ct.ID
            //            join c in db.cruises on cs.CruiseID equals c.ID
            //            select new CruiseScheduleViewModel { CruiseID = cs.CruiseID, CruiseName = c.Name, Date = cs.Date, TaskName = ct.Name, Color = ct.Color }).OrderBy(x => x.Date).GroupBy(x => x.CruiseID).ToList();
               
            //DataGrid dgGrid = new DataGrid();
            //dgGrid.DataSource = list;
            //dgGrid.DataBind();

            //string filename = "data.xls";
            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);


            ////Get the HTML for the control.
            //dgGrid.RenderControl(hw);
            ////Write the HTML back to the browser.
            ////Response.ContentType = application/vnd.ms-excel;
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AppendHeader("Content-Disposition",
            //                      "attachment; filename=" + filename + "");
            ////this.EnableViewState = false;
            //Response.Write(tw.ToString());
            //Response.End();
        
        }


    }
}