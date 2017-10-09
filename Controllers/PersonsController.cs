using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private CemsDbContext db = new CemsDbContext();

        string _returnUrl;
        public PersonsController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("Persons/Index");
        }

        //
        // GET: /Persons/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Persons()
        {

            //var list = (from p in db.persons
            //            join map in db.PersonMappings 
            //            on p.ID equals map.PersonID
            //            join cat in db.categories on map.CategoryID equals cat.ID
            //            select new PersonsViewModel{
            //                ID=p.ID,
            //                Name=p.Name,
            //                Alias=p.Alias,
            //                Color=p.Color,
            //                Category=cat.Name
            //            }).ToList();


            //var list = (from p in db.persons
            //            from map in db.PersonMappings
            //                .Where(m=>m.PersonID==p.ID).DefaultIfEmpty()
            //             from cat in db.categories 
            //             .Where(x=>x.ID==map.CategoryID).DefaultIfEmpty()                        
            //            select new PersonsViewModel
            //            {
            //                ID = p.ID,
            //                Name = p.Name,
            //                Alias = p.Alias,
            //                Color = p.Color,
            //                Category = cat.Name
            //            }).OrderBy(x=>x.Name).Distinct().ToList();


            var d=db.persons.AsQueryable();

            var list = db.Database.SqlQuery<PersonsViewModel>("exec sp_GetPersons").AsQueryable();
                
            //    db.persons.Select(x=>new PersonsViewModel{
            //                ID = x.ID,
            //                FirstName=x.FirstName,
            //                LastName= x.LastName,
            //                Alias = x.Alias,
            //                Color = x.Color,
            //                Email=x.Email,
            //                Address=x.Address,
            //                City=x.City,
            //                State=x.State,
            //                Zip=x.Zip,
            //                Phone=x.Phone                            
            //}).ToList();
           // var restpersons = db.persons.ToList().Except(list).ToList();

            return PartialView("_persons", list);
        }

        //
        // GET: /Persons/Details/5

        public ActionResult Details(int id = 0)
        {
            Persons persons = db.persons.Find(id);
            if (persons == null)
            {
                return HttpNotFound();
            }
            return View(persons);
        }

        //
        // GET: /Persons/Create
        [AllowAnonymous]
        public ActionResult Create(int option=0)
        {
            string username = User.Identity.Name;
           
            if (option == 1)
            {
                if (string.IsNullOrEmpty(username))
                {
                    return Redirect("/Account/Login");
                }
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            }
            else
            {
                ViewBag.Layout = "~/Views/Account/_Layout.cshtml";                
            }
            Session["Option"] = option;

            //ViewBag.Position = new SelectList(db.positions.OrderBy(x => x.Name).ToList(), "ID", "Name");
            //ViewBag.Categories = ShrdMaster.Instance.GetCategoryIdByPersonID(0);
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }


        public ActionResult SelectPersons(int ID)
        {
            List<Persons> persons=null;
            if(ID>0)
            {
                persons = ShrdMaster.Instance.GetPersonsByCategoryID(ID);
            }

            return Json(persons,JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Persons/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create(Persons persons,int option=0)
        {
            
            if(Session["Option"]!=null)
            {
                int.TryParse(Session["Option"].ToString(), out option);
                
                if (option==1)
                {
                    ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
                    
                }
                else
                {
                    _returnUrl = "/Account/Login";
                    ViewBag.Layout = "~/Views/Account/_Layout.cshtml";
                }
            }
            else
            {
                _returnUrl = "/Account/Login";
                ViewBag.Layout = "~/Views/Account/_Layout.cshtml";
            }           
            ViewBag.Categories = ShrdMaster.Instance.GetCategoryIdByPersonID(0);
            if (ModelState.IsValid)
            {
               if(ShrdMaster.Instance.CheckUserName(persons.Email))
               {
                   ModelState.AddModelError("Email", "Email already exists. Please enter a different Email.");
                   return View(persons);
               }
                db.persons.Add(persons);
                db.SaveChanges();
                UserRole userrole = new UserRole();
                userrole.RoleID = 2;
                userrole.UserID = persons.ID;
                db.UserRoles.Add(userrole);
                db.SaveChanges();
                
                //if (!string .IsNullOrEmpty(persons.CategoryList))
                //{
                //    ShrdMaster.Instance.SavePersonMapping(persons.CategoryList, persons.ID);

                //}
                //else
                //{
                //    ModelState.AddModelError("Category", "category is not selected");
                //    return View(persons);
                //}
                return Redirect(_returnUrl);
            }
          //  ViewBag.Position = new SelectList(db.positions.OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.ReturnUrl = _returnUrl;
            return View(persons);
        }

        //
        // GET: /Persons/Edit/5


        [AllowAnonymous]
        public ActionResult CheckUserName(string username)
        {
            bool result = ShrdMaster.Instance.CheckUserName(username);
            if(result)
            {
                return Json("1",JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit(int id = 0)
        {
            Persons persons = db.persons.Find(id);
            if (persons == null)
            {
                return HttpNotFound();
            }
            //var category = db.PersonMappings.Where(x => x.PersonID == persons.ID).SingleOrDefault();
            ViewBag.Position = new SelectList(db.positions.OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.Categories = ShrdMaster.Instance.GetCategoryIdByPersonID(persons.ID);
            ViewBag.ReturnUrl = _returnUrl;
            
            return View(persons);
        }

        //
        // POST: /Persons/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Persons persons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persons).State = EntityState.Modified;
                //if(!string.IsNullOrEmpty(persons.CategoryList))
                //{
                //    ShrdMaster.Instance.SavePersonMapping(persons.CategoryList, persons.ID);
                //}
                //else
                //{
                //    ModelState.AddModelError("Category","category is not selected");
                //    return View(persons);
                //}
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            //ViewBag.Position = new SelectList(db.positions.OrderBy(x => x.Name).ToList(), "ID", "Name",persons.Position);
            //ViewBag.Categories = ShrdMaster.Instance.GetCategoryIdByPersonID(persons.ID);
            ViewBag.ReturnUrl = _returnUrl;
            return View(persons);
        }

        //
        // GET: /Persons/Delete/5       
        public ActionResult Delete(int id = 0)
        {
            try
            {
                Persons persons = db.persons.Find(id);
                db.persons.Remove(persons);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Persons/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try 
            {
                Persons persons = db.persons.Find(id);
                db.persons.Remove(persons);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/Persons/Persons"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", null), JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public ActionResult MapToCategory(List<PersonMapping> persons)
        {
            int CategoryID = persons[0].CategoryID;
            var prevmap = db.PersonMappings.Where(x => x.CategoryID == CategoryID).ToList();


            foreach(PersonMapping person in prevmap)
            {
                PersonMapping temp = (PersonMapping)persons.Where(x => x.CategoryID == person.CategoryID && x.PersonID == person.PersonID).FirstOrDefault(); 
                if(temp==null)
                {
                    db.PersonMappings.Remove(person);
                    db.SaveChanges();
                }
                else
                {
                    persons.Remove(temp);
                }
            }
           

            //var list = prevmap.Except(persons).ToList();

            //list.ForEach(x => { db.PersonMappings.Remove(x); db.SaveChanges(); });

            foreach(PersonMapping map in persons)
            {
                //var temp = prevmap.Where(x => x.CategoryID == map.CategoryID && x.PersonID == map.PersonID);
                //if(temp==null)
                //{
                    db.PersonMappings.Add(map);
                //}
                    
                
            }

            db.SaveChanges();

            //var list=prevmap.Except(persons).ToList();
            //if(list.Count>0)
            //{
            //    list.ForEach(x => { db.PersonMappings.Remove(x); db.SaveChanges(); });
            //}          
            return Json("", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}