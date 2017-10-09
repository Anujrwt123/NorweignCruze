using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CruiseEntertainmentManagnmentSystem.Models;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class GroupsController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        string _returnUrl;
        public GroupsController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("Groups/Index");
        }


        //
        // GET: /Groups/

        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Groups()
        {
            var list = db.groups.ToList();
            return PartialView("_groups",list);
        }
        //
        // GET: /Groups/Details/5

        public ActionResult Details(int id = 0)
        {
            Groups groups = db.groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            return View(groups);
        }

        //
        // GET: /Groups/Create

        public ActionResult Create()
        {
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /Groups/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.groups.Add(groups);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(groups);
        }

        //
        // GET: /Groups/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Groups groups = db.groups.Find(id);
            if (groups == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(groups);
        }

        //
        // POST: /Groups/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Groups groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groups).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(groups);
        }

        //
        // GET: /Groups/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Groups groups = db.groups.Find(id);
                db.groups.Remove(groups);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }

        }

        //
        // POST: /Groups/Delete/5

        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            

            try
            {
                Groups groups = db.groups.Find(id);
                db.groups.Remove(groups);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/Groups/Groups"),JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
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