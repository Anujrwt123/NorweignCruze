using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class PerformersController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        string _returnUrl;
        public PerformersController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("Performers/index");
        }

        //
        // GET: /Performers/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetPerformers()
        {
            var performerlist = (from p in db.performers
                                 join
                                     g in db.groups on p.PGroup equals g.ID
                                 select new PerformerViewmodel
                                 {
                                     Alias = p.Alias,
                                     GroupName = g.GroupName,
                                     ID = p.ID,
                                     Name = p.Name,
                                     PGroup = p.PGroup
                                 }).ToList();
            return PartialView("_Performers",performerlist);
        }
        //
        // GET: /Performers/Details/5

        public ActionResult Details(int id = 0)
        {
            Performers performers = db.performers.Find(id);
            if (performers == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PGroup = new SelectList(db.groups, "ID", "GroupName");
             var performer = (from G in db.groups
                          join P in db.performers on G.ID equals P.PGroup
                          where P.ID == id
                              select new PerformerViewmodel
                              {
                                  Alias = P.Alias,
                                  GroupName = G.GroupName,
                                  ID = P.ID,
                                  Name = P.Name,
                                  PGroup = P.PGroup
                              }).SingleOrDefault();

             return View(performer);
        }

        //
        // GET: /Performers/Create

        public ActionResult Create()
        {
            ViewBag.PGroup = new SelectList(db.groups.OrderBy(x => x.GroupName).ToList(), "ID", "GroupName");
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /Performers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Performers performers)
        {
            if (ModelState.IsValid)
            {
                db.performers.Add(performers);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }

            ViewBag.PGroup = new SelectList(db.groups.OrderBy(x => x.GroupName).ToList(), "ID", "GroupName", performers.PGroup);
            ViewBag.ReturnUrl = _returnUrl;
            return View(performers);
        }

        //
        // GET: /Performers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Performers performers = db.performers.Find(id);
            if (performers == null)
            {
                return HttpNotFound();
            }

            ViewBag.PGroup = new SelectList(db.groups.OrderBy(x => x.GroupName).ToList(), "ID", "GroupName",performers.PGroup);
            ViewBag.ReturnUrl = _returnUrl;
            return View(performers);
        }

        //
        // POST: /Performers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Performers performers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performers).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.PGroup = new SelectList(db.groups.OrderBy(x => x.GroupName).ToList(), "ID", "GroupName", performers.PGroup);
            ViewBag.ReturnUrl = _returnUrl;
            return View(performers);
        }

        //
        // GET: /Performers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Performers performers = db.performers.Find(id);
                db.performers.Remove(performers);
                db.SaveChanges();


                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }

        }

        //
        // POST: /Performers/Delete/5



        [HttpPost, ActionName("Delete")]       
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Performers performers = db.performers.Find(id);
                db.performers.Remove(performers);
                db.SaveChanges();

                
                return Json(UResponse.Instance.JsonResponse("Done", "/Performers/GetPerformers"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", null),JsonRequestBehavior.AllowGet);
            }
            
            
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}