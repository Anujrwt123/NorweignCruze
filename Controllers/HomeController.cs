using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;
using System.Collections;
using System.Data.Entity;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class HomeController : Controller
    {
        CemsDbContext db = new CemsDbContext();
        public ArrayList arr = new ArrayList();
        string _returnUrl = "";
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";


            //ViewBag.Schedule = db.CruiseSchedules.OrderBy(x=>x.CruiseID).ToList();

            return View();
        }

      

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ContactCreate()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ContactCreate(ContactList model)
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("/Home/Contact");
            if(ModelState.IsValid)
            {
                db.ContactLists.Add(model);
                db.SaveChanges();

                return Redirect(_returnUrl);
            }
            return View();
        }

        public ActionResult ContactEdit(int? ID)
        {
            if(ID==null)
            {
                return HttpNotFound();
            }
            var data = db.ContactLists.Find(ID);
            if(data==null)
            {
                return HttpNotFound();
                
            }

            return View(data);

        }

        [HttpPost]
        public ActionResult ContactEdit(ContactList model)
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("/Home/Contact");
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                //db.ContactLists.Add(model);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }           
            return View(model);

        }



        public ActionResult ContactDelete(int ID)
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("/Home/Contact");
            var data=db.ContactLists.Find(ID);
            db.ContactLists.Remove(data);
            db.SaveChanges();
            return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
        }




        public ActionResult GetContactLists()
        {
            var data = db.ContactLists.ToList();
            return PartialView("_ContactListView",data);
        }


        [HttpPost]
        public ActionResult About(HttpPostedFileBase file)
        {                    
                          
                if (file.ContentLength > 0)
                {
                    //read data from input stream
                    using (var csvReader = new System.IO.StreamReader(file.InputStream))
                    {
                        string inputLine = "";

                        int jan = 0;
                        //read each line
                         
                        while ((inputLine = csvReader.ReadLine()) != null)
                        {                           
                          
                            //get lines values
                            string[] values = inputLine.Split(new char[] { ',' });

                            //for (int x = 0; x < values.Length; x++)
                            //{
                                if (values[1] != "")
                                {
                                    if (values[1].Contains("Jan"))
                                    {
                                        int i = 1;
                                        while(values[i]!="")
                                        {

                                        }
                                        jan = 1;
                                    }
                                    else if(jan==1)
                                    {
                                        jan=0;
                                    }
                                }
                          //  }
                        
                        }
                        csvReader.Close();
                    }
                }

                return View();
        }

    }
}
