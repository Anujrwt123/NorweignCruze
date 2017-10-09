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
    public class BookingTypeController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        string _returnUrl;
        public BookingTypeController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("BookingType.Index");
        }
        
        //
        // GET: /BookingType/

        public ActionResult Index()
        {
            return View();

        }


        public ActionResult BookingTypes()
        {

            var list=db.bookingtype.ToList();

            return PartialView("_BookingType", list);
        }

        //
        // GET: /BookingType/Details/5

        public ActionResult Details(int id = 0)
        {
            BookingType bookingtype = db.bookingtype.Find(id);
            if (bookingtype == null)
            {
                return HttpNotFound();
            }
            return View(bookingtype);
        }

        //
        // GET: /BookingType/Create

        public ActionResult Create()
        {
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /BookingType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingType bookingtype)
        {
        
            if (ModelState.IsValid)
            {
                db.bookingtype.Add(bookingtype);
                db.SaveChanges();
                return RedirectToAction(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(bookingtype);
        }

        //
        // GET: /BookingType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BookingType bookingtype = db.bookingtype.Find(id);
            if (bookingtype == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(bookingtype);
        }

        //
        // POST: /BookingType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingType bookingtype)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(bookingtype).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.ReturnUrl = _returnUrl;
            return View(bookingtype);
        }

        //
        // GET: /BookingType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                BookingType bookingtype = db.bookingtype.Find(id);
                db.bookingtype.Remove(bookingtype);
                db.SaveChanges();

                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /BookingType/Delete/5

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                BookingType bookingtype = db.bookingtype.Find(id);
                db.bookingtype.Remove(bookingtype);
                db.SaveChanges();

                return Json(UResponse.Instance.JsonResponse("Done", "/BookingType/BookingTypes"),JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", "/BookingType/BookingTypes"), JsonRequestBehavior.AllowGet);
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