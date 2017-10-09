using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{
    [Authorize]
    public class ShowsController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        string _returnUrl;

        public ShowsController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("/Shows/Index");
        }


        //
        // GET: /Shows/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Shows()
        {
            var list = (from s in db.shows
                        join c in db.cruises on s.Ship equals c.ID
                        select new ShowsViewModel {
                            ID=s.ID,Name=s.Name,Ship=c.Name,Description=c.Description                          
                        }).ToList();
            return PartialView("_shows", list);
        }

        //
        // GET: /Shows/Details/5

        public ActionResult Details(int id = 0)
        {
            Shows shows = db.shows.Find(id);
            if (shows == null)
            {
                return HttpNotFound();
            }
            return View(shows);
        }

        //
        // GET: /Shows/Create

        public ActionResult Create()
        {
            ViewBag.ship = new SelectList(db.cruises.OrderBy(x => x.Name).ToList(), "ID","name");
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /Shows/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shows shows)
        {
            if (ModelState.IsValid)
            {
                db.shows.Add(shows);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ship = new SelectList(db.cruises.OrderBy(x => x.Name).ToList(), "ID","name",shows.Ship);
            ViewBag.ReturnUrl = _returnUrl;
            return View(shows);
        }

        //
        // GET: /Shows/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Shows shows = db.shows.Find(id);
            if (shows == null)
            {
                return HttpNotFound();
            }
            ViewBag.ship = new SelectList(db.cruises.OrderBy(x => x.Name).ToList(), "ID","name",shows.Ship);
            ViewBag.ReturnUrl = _returnUrl;
            return View(shows);
        }

        //
        // POST: /Shows/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shows shows)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shows).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ship = new SelectList(db.cruises.OrderBy(x => x.Name).ToList(), "ID","name",shows.Ship);
            ViewBag.ReturnUrl = _returnUrl;
            return View(shows);
        }

        //
        // GET: /Shows/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Shows shows = db.shows.Find(id);
                db.shows.Remove(shows);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }

         
        }

        //
        // POST: /Shows/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Shows shows = db.shows.Find(id);
                db.shows.Remove(shows);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/Shows/Shows"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", null), JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}