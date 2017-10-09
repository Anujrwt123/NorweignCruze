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
    public class CategoriesController : Controller
    {
        private CemsDbContext db = new CemsDbContext();

        //
        // GET: /Categories/

        string _returnUrl;
        public CategoriesController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("Categories/Index");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            var list = db.categories.OrderBy(x=>x.Name).ToList();

            return PartialView("_categories", list);
        }

        public ActionResult SelectCategories()
        {
            var data=db.categories.OrderBy(x=>x.Name).ToList();
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Categories/Details/5

        public ActionResult Details(int id = 0)
        {
            Category categories = db.categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            ViewBag.ReturnUrl = _returnUrl;
            return View();
            
        }

        //
        // POST: /Categories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category categories)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(categories);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(categories);
        }

        //
        // GET: /Categories/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category categories = db.categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(categories);
        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(categories);
        }

        //
        // GET: /Categories/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Category categories = db.categories.Find(id);
                db.categories.Remove(categories);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Category categories = db.categories.Find(id);
                db.categories.Remove(categories);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/Categories/Categories"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", null), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CopyCategories(int From, int To)
        {
            
                var list = db.PersonMappings.Where(x => x.CategoryID == From).ToList();

                list.ForEach(x => { x.CategoryID = To; db.PersonMappings.Add(x); });
                db.SaveChanges();
            

            return Json("done", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}