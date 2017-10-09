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
    public class CabinCategoriesController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        string _returnUrl;
        //

        public CabinCategoriesController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("CabinCategories/Index");
        }
        // GET: /CabinCategories/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CabinCategories()
        {
            var list=db.cabincategories.ToList();

            return PartialView("_CabinCategories",list);
        }
        //
        // GET: /CabinCategories/Details/5

        public ActionResult Details(int id = 0)
        {
            CabinCategories cabincategories = db.cabincategories.Find(id);
            if (cabincategories == null)
            {
                return HttpNotFound();
            }
            return View(cabincategories);
        }

        //
        // GET: /CabinCategories/Create

        public ActionResult Create()
        {
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /CabinCategories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CabinCategories cabincategories)
        {
            if (ModelState.IsValid)
            {
                db.cabincategories.Add(cabincategories);
                db.SaveChanges();
                return RedirectToAction(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabincategories);
        }

        //
        // GET: /CabinCategories/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //string returnUrl = "";
            //if(Request.QueryString["returnUrl"]!=null)
            //{
            //    returnUrl = Request.QueryString["returnUrl"];
            //}
            CabinCategories cabincategories = db.cabincategories.Find(id);
            if (cabincategories == null)
            {
                return HttpNotFound();
            }
            // ViewBag.returnURL=returnUrl;
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabincategories);
        }

        //
        // POST: /CabinCategories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CabinCategories cabincategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cabincategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabincategories);
        }

        //
        // GET: /CabinCategories/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                CabinCategories cabincategories = db.cabincategories.Find(id);
                db.cabincategories.Remove(cabincategories);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /CabinCategories/Delete/5

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CabinCategories cabincategories = db.cabincategories.Find(id);
                db.cabincategories.Remove(cabincategories);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/CabinCategories/CabinCategories"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", "/CabinCategories/CabinCategories"), JsonRequestBehavior.AllowGet);
            }

            
           // return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}