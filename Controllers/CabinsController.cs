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
    [Authorize(Roles="Admin")]
    public class CabinsController : Controller
    {
        private CemsDbContext db = new CemsDbContext();

        //
        // GET: /Cabins/

        string _returnUrl;
        public CabinsController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("Cabins/Index");
        }

        public ActionResult Index()
        {
            return View();
        }




        public ActionResult Cabins()
        {
            var cabinslist = (from c in db.cabincategories
                              join cb in db.cabins on c.ID equals cb.CatName
                              select new CabinsViewModel
                              {
                                  ID = cb.ID,
                                  CabinName = cb.CabinName,
                                  CatName = cb.CatName,
                                  CabinType = cb.CabinType,
                                  CategoryName = c.CategoryName

                              }).ToList();
            return PartialView("_Cabins",cabinslist);
        }

        //
        // GET: /Cabins/Details/5

        public ActionResult Details(int id = 0)
        {
            Cabins cabins = db.cabins.Find(id);
            if (cabins == null)
            {
                return HttpNotFound();
            }
            var cabin = (from c in db.cabincategories
                         join cb in db.cabins on c.ID equals cb.CatName
                         where cb.ID == id
                         select new CabinsViewModel
                         {
                             ID = cb.ID,
                             CabinName = cb.CabinName,
                             CatName = cb.CatName,
                             CabinType = cb.CabinType,
                             CategoryName = c.CategoryName

                         }).SingleOrDefault();
            return View(cabin);
        }

        //
        // GET: /Cabins/Create

        public ActionResult Create()
        {
           
            
            ViewBag.catName = new SelectList(db.cabincategories.OrderBy(x=>x.CategoryName).ToList(), "ID", "CategoryName");
            ViewBag.CabinType = new SelectList(Common.CabinTypes(), "ID", "Value");
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /Cabins/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cabins cabins)
        {
            if (ModelState.IsValid)
            {
                db.cabins.Add(cabins);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.catName = new SelectList(db.cabincategories.OrderBy(x => x.CategoryName).ToList(), "ID", "CategoryName", cabins.CatName);
            
            ViewBag.CabinType = new SelectList(Common.CabinTypes(), "ID", "Value",cabins.CabinType);
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabins);
        }

        //
        // GET: /Cabins/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cabins cabins = db.cabins.Find(id);
            if (cabins == null)
            {
                return HttpNotFound();
            }
            ViewBag.catName = new SelectList(db.cabincategories.OrderBy(x => x.CategoryName).ToList(), "ID", "CategoryName", cabins.CatName);
            ViewBag.CabinType = new SelectList(Common.CabinTypes(), "ID", "Value",cabins.CabinType);
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabins);
        }

        //
        // POST: /Cabins/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cabins cabins)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cabins).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.catName = new SelectList(db.cabincategories.OrderBy(x => x.CategoryName).ToList(), "ID", "CategoryName", cabins.CatName);
            ViewBag.CabinType = new SelectList(Common.CabinTypes(), "ID", "Value",cabins.CabinType);
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabins);
        }

        //
        // GET: /Cabins/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cabins cabins = db.cabins.Find(id);
            if (cabins == null)
            {
                return HttpNotFound();
            }
            var cabin = (from c in db.cabincategories
                         join cb in db.cabins on c.ID equals cb.CatName
                         where cb.ID == id
                         select new CabinsViewModel
                         {
                             ID = cb.ID,
                             CabinName = cb.CabinName,
                             CatName = cb.CatName,
                             CabinType = cb.CabinType,
                             CategoryName = c.CategoryName

                         }).SingleOrDefault();
            return View(cabin);
        }

        //
        // POST: /Cabins/Delete/5

        [HttpPost, ActionName("Delete")]
     //   [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cabins cabins = db.cabins.Find(id);
                db.cabins.Remove(cabins);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/Cabins/Cabins"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone",null), JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}