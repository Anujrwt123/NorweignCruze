using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;
using System.Data.Entity;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{

    [Authorize(Roles="Admin")]
    public class CabinBookingsController : Controller
    {
        CemsDbContext db = new CemsDbContext();
        string _returnUrl;
        //
        // GET: /CabinBookings/
        public CabinBookingsController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("CabinBookings/Index");
        }

        public ActionResult Index()
        {

            //Logger.Instance.Log("In Index MEthod of Cabin Bookings");
            return View();
        }


        public ActionResult CabinBookings()
        {
          //  Logger.Instance.Log("In Cabin Categories method of Cabin Bookings");
            var list = (from CB in db.CabinBookings
                       join C in db.cabins on CB.CabinNo equals C.ID
                       join P in db.positions on CB.Position equals P.ID
                       join B in db.bookingtype on CB.BookingType equals B.ID
                       select new CabinBookingViewModel {
                            ID = CB.ID,
                            CabinNo=CB.CabinNo,
                            CabinName=C.CabinName,
                            BookingType=CB.BookingType,
                            BookingTypeName=B.Name,
                            Position=CB.Position,
                            PositionName=P.Name,
                            Name=CB.Name,
                            Requestor=CB.Requestor,
                            Fleet=CB.Fleet,
                            Hotel=CB.Hotel,
                            BookingFrom=CB.BookingFrom,
                            BookingTo=CB.BookingTo,
                            Reservation=CB.Reservation
                       }).ToList();



            return PartialView("_CabinBookings",list);
        }



        public ActionResult Create(int ID=0,string returnUrl="")
        {
            var cabins = db.cabins.OrderBy(x=>x.CabinName).ToList();
            SelectList list =null;
            if(ID>0)
            {
                list= new SelectList(cabins, "ID", "CabinName",ID);
            }
            else
            {
                list = new SelectList(cabins, "ID", "CabinName");
            }
            
            ViewBag.CabinNo = list;
            string number = AutoGenerateNumber();
            ViewBag.Reservation = number;
            ViewBag.returnURL = returnUrl;
            ViewBag.Position = new SelectList(db.positions.OrderBy(x=>x.Name).ToList(), "ID", "Name");
            ViewBag.BookingType = new SelectList(db.bookingtype.OrderBy(x=>x.Name).ToList(), "ID", "Name");
            ViewBag.ReturnUrl = _returnUrl;
            return View();
        }

        //
        // POST: /CabinBooking/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CabinBooking cabinbooking)
        {
           // string returnUrl="" ;
            DateTime fromdate;
            DateTime todate;
            List<CabinBooking> Bookings = null;
            //DateTime bookfrom;
            //DateTime bookto;
            //string bookf;
            //string bookt;
            //int cabinno;
            //if (Request.QueryString["returnUrl"] != null)
            //{
            //    returnUrl = Request.QueryString["returnUrl"];
            //   // return RedirectToAction(returnUrl);
            //}
            var cabins = db.cabins.OrderBy(x=>x.CabinName).ToList();
            SelectList list = new SelectList(cabins, "ID", "CabinName", cabinbooking.CabinNo);
            ViewBag.CabinNo = list;
            ViewBag.Reservation = cabinbooking.Reservation;
            //ViewBag.CabinNo = new SelectList(db.cabins, "ID", "CabinName", cabinbooking.CabinNo);
            ViewBag.Position = new SelectList(db.positions.OrderBy(x=>x.Name).ToList(), "ID", "Name", cabinbooking.Position);
            ViewBag.BookingType = new SelectList(db.bookingtype.OrderBy(x=>x.Name).ToList(), "ID", "Name", cabinbooking.BookingType);
            ViewBag.ReturnURL = _returnUrl;
            
            if (ModelState.IsValid)
            {
                fromdate = cabinbooking.BookingFrom;
                todate = cabinbooking.BookingTo;                
                Bookings = db.CabinBookings.Where(x => (x.BookingFrom <= fromdate && x.BookingTo >= todate) || (x.BookingFrom <= fromdate && x.BookingTo <= todate && fromdate <= x.BookingTo) || (x.BookingFrom >= fromdate && x.BookingTo >= todate && todate >= x.BookingFrom)).Where(x => x.CabinNo == cabinbooking.CabinNo).ToList();
                if(Bookings.Count>0)
                {
                 ViewBag.Message = "Cabin is occupied on these dates.";
                 return View(cabinbooking);
                }
                else
                {
                db.CabinBookings.Add(cabinbooking);
                db.SaveChanges();
                //if(!string.IsNullOrEmpty(returnUrl))
                //{
                //    return Redirect(returnUrl);
                //}
                }
               
                return Redirect(_returnUrl);
            }

           

            return View(cabinbooking);
        }
        
        
        
        
        //GET:/Bookings/Edit/5

        public ActionResult Edit(int id = 0)
        {
            string returnUrl = "";
            if (Request.QueryString["returnUrl"] != null)
            {
                returnUrl = Request.QueryString["returnUrl"];
                // return RedirectToAction(returnUrl);
            }

            CabinBooking cabinbooking = db.CabinBookings.Find(id);
            if (cabinbooking == null)
            {
                return HttpNotFound();
            }
            var cabins = db.cabins.OrderBy(x=>x.CabinName).ToList();
            SelectList list = new SelectList(cabins, "ID", "CabinName", cabinbooking.CabinNo);
            ViewBag.CabinNo = list;
           // ViewBag.Reservation = cabinbooking.Reservation;
            //ViewBag.CabinNo = new SelectList(db.cabins, "ID", "CabinName", cabinbooking.CabinNo);
            ViewBag.Position = new SelectList(db.positions.OrderBy(x=>x.Name).ToList(), "ID", "Name", cabinbooking.Position);
            ViewBag.BookingType = new SelectList(db.bookingtype.OrderBy(x=>x.Name).ToList(), "ID", "Name", cabinbooking.BookingType);
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabinbooking);
        }

        //
        // POST: /CabinBooking/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CabinBooking cabinbooking)
        {
            string returnUrl = "";
            //if (Request.QueryString["returnUrl"] != null)
            //{
            //    returnUrl = Request.QueryString["returnUrl"];
            //    //return Redirect(returnUrl);
            //}
            if (ModelState.IsValid)
            {
                db.Entry(cabinbooking).State = EntityState.Modified;
                db.SaveChanges();
                //if (!string.IsNullOrEmpty(returnUrl))
                //{
                //    return Redirect(returnUrl);
                //}
                return RedirectToAction(_returnUrl);
            }
            var cabins = db.cabins.OrderBy(x=>x.CabinName).ToList();
            SelectList list = new SelectList(cabins, "ID", "CabinName", cabinbooking.CabinNo);
            ViewBag.CabinNo = list;
          //  ViewBag.Reservation = cabinbooking.Reservation;
            //ViewBag.CabinNo = new SelectList(db.cabins, "ID", "CabinName", cabinbooking.CabinNo);
            ViewBag.Position = new SelectList(db.positions.OrderBy(x=>x.Name).ToList(), "ID", "Name", cabinbooking.Position);
            ViewBag.BookingType = new SelectList(db.bookingtype.OrderBy(x=>x.Name).ToList(), "ID", "Name", cabinbooking.BookingType);
            ViewBag.ReturnUrl = _returnUrl;
            return View(cabinbooking);
        }


        [HttpPost, ActionName("Delete")]
     //   [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                CabinBooking cabinbooking = db.CabinBookings.Find(id);
                db.CabinBookings.Remove(cabinbooking);
                db.SaveChanges();
                return Json(UResponse.Instance.JsonResponse("Done", "/CabinBookings/CabinBookings"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", "/CabinBookings/CabinBookings"), JsonRequestBehavior.AllowGet);
            }

            
            //return RedirectToAction("Index");
        }


        public ActionResult GetBooking(DateTime? From, DateTime? To,int option=0)
        {

            List<CabinBooking> Bookings = null;
            NewDate dt = new NewDate();
           DateTime fromdate=DateTime.MinValue ;
           DateTime todate = DateTime.MinValue;

            if(From==null && To==null)
            {
                fromdate=db.CabinBookings.ToList().Min(x=>x.BookingFrom);
                todate = db.CabinBookings.ToList().Max(x => x.BookingTo);
            }
            else if(From==null && To!=null)
            {
                fromdate = db.CabinBookings.ToList().Min(x => x.BookingFrom);
                todate = To.Value;
            }
            else if(From!=null && To==null)
            {

                fromdate = From.Value;
                todate = db.CabinBookings.ToList().Max(x => x.BookingTo); ;
            }
            else
            {
                if (From.Value != null && To.Value != null)
                {
                    fromdate = From.Value;
                    todate = To.Value;
                }
            }
            if(option==0)
            {
                Bookings = db.CabinBookings.Where(x => (x.BookingFrom <= fromdate && x.BookingTo >= todate) || (x.BookingFrom <= fromdate && x.BookingTo <= todate) || (x.BookingFrom >= fromdate && x.BookingTo >= todate)).ToList();
            }
            else
            {
                Bookings = db.CabinBookings.ToList();
            }
                    //Bookings = db.CabinBookings.ToList();
                    List<DateTime> date = null;
                    date = DateRange(fromdate, todate).ToList();
                    dt.dates = date;
                    dt.booking = Bookings;
                    dt.Cabins = db.cabins.ToList();
           
            return PartialView("_NewBooking", dt);

        }
        
        public string AutoGenerateNumber()
        {    
            //generate Random number between 1 to 100000
            Random r = new Random();
            int num = r.Next(100000, 999999);
            //concatenate both
            string number = "R" + num.ToString();
            if (CheckReservationNumber(number))
            {
                AutoGenerateNumber();
            }
            //Return the generated number
            return number;
        }

        public IEnumerable<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d));
        }

        

        public bool CheckReservationNumber(string Number)
        {
            var result = db.CabinBookings.Where(x => x.Reservation == Number).ToList();
            if(result.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
