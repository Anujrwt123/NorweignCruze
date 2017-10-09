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
    public class PositionsController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        string _returnUrl;
        public PositionsController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("Positions/Index");
        }
        //
        // GET: /Positions/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Positions()
        {
            var list = db.Database.SqlQuery<PositionViewModel>("exec sp_GetPositions").ToList();
                //db.positions.ToList();

            return PartialView("_positions", list);
        }

        //
        // GET: /Positions/Details/5

        public ActionResult Details(int id = 0)
        {
            Position position = db.positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        //
        // GET: /Positions/Create

        public ActionResult Create()
        {
            ViewBag.ReturnUrl = _returnUrl;
            ViewBag.Categories = new SelectList(db.categories.OrderBy(x => x.Name).ToList(), "ID", "Name");
            return View();
        }

        //
        // POST: /Positions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Position position)
        {
            if (ModelState.IsValid)
            {
                db.positions.Add(position);
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.Categories = new SelectList(db.categories.OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.ReturnUrl = _returnUrl;
            return View(position);
        }

        //
        // GET: /Positions/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Position position = db.positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = new SelectList(db.categories.OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.ReturnUrl = _returnUrl;
            return View(position);
        }

        //
        // POST: /Positions/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(_returnUrl);
            }
            ViewBag.Categories = new SelectList(db.categories.OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.ReturnUrl = _returnUrl;
            return View(position);
        }

        //
        // GET: /Positions/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Position position = db.positions.Find(id);
                db.positions.Remove(position);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", _returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", _returnUrl), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Positions/Delete/5

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Position position = db.positions.Find(id);
                db.positions.Remove(position);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/Positions/Positions"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", null), JsonRequestBehavior.AllowGet);
            }


            
          //  return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}