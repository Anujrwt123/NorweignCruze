using System;
using System.Linq;
using System.Web.Mvc;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;
using CruiseEntertainmentManagnmentSystem.Filters;
using System.Text;
using System.Data.Entity;
using System.Data.SqlClient;

namespace CruiseEntertainmentManagnmentSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [InitializeSimpleMembership]
    public class ContractorsController : Controller
    {
        private CemsDbContext db = new CemsDbContext();
        private ViewResult _viewResult;
        private string _returnUrl;

        public ContractorsController()
        {
            _returnUrl = ShrdMaster.Instance.GetReturnUrl("/Contractors/Index");
        }

        //
        // GET: /Contractors/

        public ActionResult Index(int Type = 1)

        {
            if (Request.QueryString["option"] != null)
            {
                Type = Convert.ToInt32(Request.QueryString["option"]);
            }
            ViewBag.Option = Type;

            return View();
        }


        public ActionResult Contractors(int option)
        {

            //var list = (from C in db.Contractors join CR in db.cruises on C.ship equals CR.ID join p in db.positions on C.position equals p.ID select C).ToList(); 

            //var list = (from C in db.Contractors
            //            join CR in db.cruises on C.ship equals CR.ID into ship
            //            from shipt in ship.DefaultIfEmpty()
            //            join p in db.positions on C.position equals p.ID into pos
            //            from post in pos.DefaultIfEmpty()
            //            join per in db.persons on C.Person equals per.ID into pers
            //            from perst in pers.DefaultIfEmpty()
            //            select new ContractorViewModel
            //            {
            //                ID = C.ID,
            //                Name = perst.FirstName + " " + perst.LastName,
            //                InitiatedDate = C.InitiatedDate,
            //                Email = C.Email,
            //                Address = C.Address,
            //                City = C.City,
            //                Zip = C.Zip,
            //                Phone = C.Phone,
            //                BaseSalary = C.BaseSalary,
            //                Day_Rate = C.Day_Rate,
            //                Days_On_Land = C.Days_On_Land,
            //                Days_OnBoard = C.Days_OnBoard,
            //                EndDate = C.EndDate,
            //                Payment_1 = C.Payment_1,
            //                Payment_2 = C.Payment_2,
            //                Payment_2_Date = C.Payment_2_Date.Value,
            //                position = C.position,
            //                PositionName = post.Name,
            //                Shows = C.Shows,
            //                SSN = C.SSN,
            //                Term = C.Term,
            //                Total_Fee = C.Total_Fee,
            //                //Total_Per_Diem = C.Total_Per_Diem,
            //                Total_Per_Diem_On_Board = C.Total_Per_Diem_On_Board,
            //                Total_Per_Diem_On_Land = C.Total_Per_Diem_On_Land,
            //                ship = C.ship,
            //                ShipName = shipt.Name,
            //                shipDates = C.shipDates,
            //                ContractorType = C.ContractorType,
            //                AgreementType = C.AgreementType
            //            }).Where(x => x.ContractorType == option).ToList();

            var list = db.Database.SqlQuery<ContractorViewModel>("exec sp_GetContractors @option", new SqlParameter("@option", option)).ToList();
            return PartialView($"_ContractorsView", list);
        }
        //
        // GET: /Contractors/Details/5

        public ActionResult Details(int id = 0)
        {
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            _viewResult = View(contractor);
            return _viewResult;
        }

        //
        // GET: /Contractors/Create

        public ActionResult Create(int option)
        {
            ViewBag.ContractorAgreement = new SelectList(Common.ContractorAgree(), "ID", "Value");
            ViewBag.ship = new SelectList(db.cruises.OrderBy(x=>x.Name).ToList(), "id", "name");
            ViewBag.position = new SelectList(db.positions.OrderBy(x=>x.Name).ToList(), "ID", "Name");
            ViewBag.AgreementType = new SelectList(Common.Agreements(), "ID", "Value");
            ViewBag.MPerson = new SelectList(ShrdMaster.Instance.GetPersons(), "ID", "FullName");
            ViewBag.Brands = new SelectList(ShrdMaster.Instance.GetBrands(), "Name", "Name");
           // ViewBag.Shows = new SelectList(null, null, null);
            ViewBag.ReturnUrl = _returnUrl;
            ViewBag.Option = option;
            return View();
        }

        //
        // POST: /Contractors/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contractor contractor)
        {
            

            if (ModelState.IsValid)
            {
                db.Contractors.Add(contractor);
                if (contractor.AgreementType != 1)
                {
                    contractor.ContractorAgreement = null;
                }
                int type = contractor.ContractorType;
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("exec sp_SaveShowsByContractor @input,@contractorID", new SqlParameter("@input", contractor.ShowsList), new SqlParameter("@contractorID", contractor.ID));
                //db.SaveChanges();
                return Redirect(_returnUrl);
            }

            ViewBag.ReturnUrl = _returnUrl;
            ViewBag.IniitaitedDate = contractor.InitiatedDate.ToShortDateString();
            ViewBag.Payment1Date = contractor.Payment_1_Date.ToShortDateString();
            if (contractor.Payment_2_Date != null)
                ViewBag.Payment2Date = contractor.Payment_2_Date.Value.ToShortDateString();
            ViewBag.Option = contractor.ContractorType;
            if (contractor.Payment_3_Date != null)
            {
                ViewBag.Payment3Date = contractor.Payment_3_Date.Value.ToShortDateString();
            }
            else
            {
                ViewBag.Payment3Date = "";
            }

            if (contractor.Payment_4_Date != null)
            {
                ViewBag.Payment4Date = contractor.Payment_4_Date.Value.ToShortDateString();
            }
            else
            {
                ViewBag.Payment4Date = "";
            }

            if (contractor.Payment_5_Date != null)
            {
                ViewBag.Payment5Date = contractor.Payment_5_Date.Value.ToShortDateString();
            }
            else
            {
                ViewBag.Payment5Date = "";
            }
            ViewBag.ContractorAgreement = new SelectList(Common.ContractorAgree(), "ID", "Value");
            ViewBag.Brands = new SelectList(ShrdMaster.Instance.GetBrands(), "Name", "Name");
            ViewBag.ship = new SelectList(db.cruises.OrderBy(x=>x.Name).ToList(), "id", "name");
            ViewBag.position = new SelectList(db.positions.OrderBy(x=>x.Name).ToList(), "ID", "Name");
            ViewBag.AgreementType = new SelectList(Common.Agreements(), "ID", "Value");
            ViewBag.MPerson = new SelectList(ShrdMaster.Instance.GetPersons(), "ID", "FullName");
            ViewBag.ReturnUrl = _returnUrl;
            ViewBag.Errors ="1";
            return View(contractor);
        }

        //
        // GET: /Contractors/Edit/5

        public ActionResult Edit(int id = 0)
        {

            Contractor contractor = db.Contractors.Find(id);
            var shipinfo = db.cruises.FirstOrDefault(x => x.ID == contractor.ship);
            //var shows = db.shows.Where(x => x.Ship == contractor.ship).OrderBy(x=>x.Name).ToList();
            ViewBag.ship = new SelectList(db.cruises.Where(x=>x.ShipBrand==shipinfo.ShipBrand).OrderBy(x=>x.Name).ToList(), "id", "name", contractor.ship);
            ViewBag.position = new SelectList(db.positions.OrderBy(x=>x.Name).ToList(), "ID", "Name", contractor.position);
            ViewBag.AgreementType = new SelectList(Common.Agreements(), "ID", "Value", contractor.AgreementType);
            ViewBag.MPerson = new SelectList(ShrdMaster.Instance.GetPersons(), "ID", "FullName", contractor.Person);
            ViewBag.ContractorAgreement = new SelectList(Common.ContractorAgree(), "ID", "Value", contractor.ContractorAgreement);
            ViewBag.Brands = new SelectList(ShrdMaster.Instance.GetBrands(), "Name", "Name",shipinfo.ShipBrand);
            ViewBag.Shows = ShrdMaster.Instance.GetShowsByContractorID(contractor.ID,contractor.ship);

            ViewBag.ReturnUrl = _returnUrl;
            if (contractor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Option = contractor.ContractorType;
            return View(contractor);
        }

        //
        // POST: /Contractors/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contractor contractor)
        {
          //  _returnUrl= ShrdMaster.Instance.GetReturnUrl("/Contractors/Index"); 
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                if (contractor.AgreementType != 1)
                {
                    contractor.ContractorAgreement = null;
                }


                db.SaveChanges();
                db.Database.ExecuteSqlCommand("exec sp_SaveShowsByContractor @input,@contractorID", new SqlParameter("@input", contractor.ShowsList), new SqlParameter("@contractorID", contractor.ID));
                return Redirect(_returnUrl);
            }
            var shipinfo = db.cruises.FirstOrDefault(x => x.ID == contractor.ship);
            ViewBag.ship = new SelectList(db.cruises.Where(x => x.ShipBrand == shipinfo.ShipBrand).OrderBy(x => x.Name).ToList(), "id", "name", contractor.ship);
            ViewBag.position = new SelectList(db.positions.OrderBy(x => x.Name).ToList(), "ID", "Name", contractor.position);
            ViewBag.AgreementType = new SelectList(Common.Agreements(), "ID", "Value", contractor.AgreementType);
            ViewBag.ReturnUrl = _returnUrl;
            ViewBag.MPerson = new SelectList(ShrdMaster.Instance.GetPersons(), "ID", "FullName", contractor.Person);            
            ViewBag.Brands = new SelectList(ShrdMaster.Instance.GetBrands(), "Name", "Name", shipinfo.ShipBrand);
            ViewBag.Shows = new SelectList(db.shows.OrderBy(x => x.Name).ToList(), "ID", "Name");
            ViewBag.ContractorAgreement = new SelectList(Common.ContractorAgree(), "ID", "Value", contractor.ContractorAgreement);
            ViewBag.Option = contractor.ContractorType;
            return View(contractor);
        }

        //
        // GET: /Contractors/Delete/5

        public ActionResult Delete(int id = 0)
        {
            try
            {
                Contractor contractor = db.Contractors.Find(id);
                db.Contractors.Remove(contractor);
                db.SaveChanges();
                // return RedirectToAction("Index");
                return Json(UResponse.Instance.JsonResponse("Done",_returnUrl), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", null), JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Contractors/Delete/5

        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int Option)
        {
            try
            {
                Contractor contractor = db.Contractors.Find(id);
                db.Contractors.Remove(contractor);
                db.SaveChanges();
                // return RedirectToAction("Index");
                return Json(UResponse.Instance.JsonResponse("Done", "/Contractors/Contractors?option=" + Option), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(UResponse.Instance.JsonResponse("NotDone", null), JsonRequestBehavior.AllowGet);
            }
        }

        

        public ActionResult GenerateAgreementView()
        {
            return PartialView("AgreementView");
        }


        public void GenerateAgreement(int ID, int val, int AgreementType)
        {


            var contractor = db.Contractors.Find(ID);

            string[] months = new string[] { "Januray", "February", "March", "April", "May", "June", "July", "August", "september", "october", "November", "December" };

            Session["Contractor"] = contractor;
            //  string Url = "http://localhost:60291/Contractors/GenerateAgreementView";

            //SelectPdf.PdfDocument document=ShrdMaster.Instance.GeneratePDF(Url);
            // string path = Server.MapPath("/Agreement");
            // string name = contractor.Name;// +"- " + contractor.ID + DateTime.Now.Month + "-" + DateTime.Now.Date + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".pdf";
            // path = Path.Combine(path, name);
            //document.Save(path);
            Persons person = null;
            if (contractor.Person > 0)
            {
                person = db.persons.Find(contractor.Person);
            }


            string name = "download";
            if (person != null)
            {
                name = person.FirstName + " " + person.LastName;
            }
            string tempPath = Server.MapPath("/Templates");

            System.IO.StreamReader sr = null;

            if (AgreementType == 1)
            {

                if (contractor.ContractorAgreement == "ncl")
                {
                    sr = new System.IO.StreamReader(tempPath + "//NCL.txt");

                }
                else if (contractor.ContractorAgreement == "Oceana")
                {
                    sr = new System.IO.StreamReader(tempPath + "//OCEANA.txt");

                }
                else if (contractor.ContractorAgreement == "Regent")
                {
                    sr = new System.IO.StreamReader(tempPath + "//REGENT.txt");

                }
                else
                {
                    sr = new System.IO.StreamReader(tempPath + "//AgreementTemplate.txt");
                }
                
            }
            else if (AgreementType == 2)
            {
                sr = new System.IO.StreamReader(tempPath + "//ProductionAgreementTemplate.txt");
            }
            //else if (AgreementType == 4)
            //{
            //    sr = new System.IO.StreamReader(tempPath + "//NCL.txt");
            //}
            //else if (AgreementType == 5)
            //{
            //    sr = new System.IO.StreamReader(tempPath + "//OCEANA.txt");
            //}
            //else if (AgreementType == 6)
            //{
            //    sr = new System.IO.StreamReader(tempPath + "//REGENT.txt");
            //}
            else 
            {
                sr = new System.IO.StreamReader(tempPath + "//ProductionAgreementWithRoyaltyTemplate.txt");

            }
            var position = db.positions.Find(contractor.position);
            var ship = db.cruises.Find(contractor.ship);
            string htmlString = sr.ReadToEnd();

            DateTime InitiateDate = contractor.InitiatedDate;
            string month = months[InitiateDate.Month - 1];
            string date = InitiateDate.Day.ToString();

            string effectivedat = contractor.Term;
            string[] termdata = null;
            string effectivedate = "";
            string effectiveEnddate = "";
            if (!string.IsNullOrEmpty(effectivedat))
            {
                termdata = effectivedat.Split('-');
                effectivedate = termdata[0];
                effectiveEnddate = termdata[1];
            }

            // string effectivedate=termdata[0]+", "+InitiateDate.Year;

            string[] tampaDate = null;
            string tampadat = contractor.TampaDates;
            string tampaStartDate = "";
            if (!string.IsNullOrEmpty(tampadat))
            {
                tampaDate = tampadat.Split('-');
                tampaStartDate = tampaDate[0] + ", " + InitiateDate.Year;
            }

            // string effectivedate=termdata[0]+", "+InitiateDate.Year;




            string point3 = "Compensation.&nbsp;&nbsp; As compensation for the Services performed by the Consultant,";
            point3 += "Norwegian will pay CONSULTANT a consulting fee of <b><%option%> $" + contractor.Total_Fee.ToString() + "";
            point3 += " for a total project fee plus per diem </b>pursuant to the terms outlined in Exhibit B.";



            double weeklyrate = Convert.ToDouble(contractor.Day_Rate * 7);
            htmlString = htmlString.Replace("<%weeklyrate%>", weeklyrate.ToString());
            htmlString = htmlString.Replace("<%name%>", name);
            htmlString = htmlString.Replace("<%getdate%>", date);
            htmlString = htmlString.Replace("<%effectivedate%>", effectivedate);
            htmlString = htmlString.Replace("<%month%>", month);
            htmlString = htmlString.Replace("<%address%>", contractor.Address);
            htmlString = htmlString.Replace("<%city%>", contractor.City);
            htmlString = htmlString.Replace("<%zip%>", contractor.Zip);
            htmlString = htmlString.Replace("<%phone%>", contractor.Phone);
            htmlString = htmlString.Replace("<%SSN%>", contractor.SSN);
            htmlString = htmlString.Replace("<%Email%>", contractor.Email);
            htmlString = htmlString.Replace("<%shows%>", contractor.Shows);
            htmlString = htmlString.Replace("<%tampdates%>", contractor.TampaDates);
            htmlString = htmlString.Replace("<%shipdates%>", contractor.shipDates);
            htmlString = htmlString.Replace("<%enddate%>", contractor.EndDate.ToShortDateString());
            htmlString = htmlString.Replace("<%term%>", contractor.Term);
            htmlString = htmlString.Replace("<%payment1%>", contractor.Payment_1.ToString());
            htmlString = htmlString.Replace("<%payment2%>", contractor.Payment_2.ToString());

            /////
            htmlString = htmlString.Replace("<%InitiatedDate%>", InitiateDate.ToShortDateString());
            htmlString = htmlString.Replace("<%TotalFee%>", contractor.Total_Fee.ToString());
            htmlString = htmlString.Replace("<%effectiveEnddate%>", effectiveEnddate);
            htmlString = htmlString.Replace("<%tampaStartDate%>", tampaStartDate);
            htmlString = htmlString.Replace("<%payment1Date%>", contractor.Payment_1_Date.ToShortDateString());







            ///




            if (contractor.Payment_2_Date != null)
            {
                htmlString = htmlString.Replace("<%payment2date%>", contractor.Payment_2_Date.Value.ToShortDateString());
            }
            else
            {
                htmlString = htmlString.Replace("<%payment2date%>", "");
            }
            htmlString = htmlString.Replace("<%totalfee%>", contractor.Total_Fee.ToString());
            htmlString = htmlString.Replace("<%totaldiemonland%>", contractor.Total_Per_Diem_On_Land.ToString());
            htmlString = htmlString.Replace("<%totladiemonboard%>", contractor.Total_Per_Diem_On_Board.ToString());
            htmlString = htmlString.Replace("<%perdiemonboard%>", contractor.Per_Diem_On_Board.ToString());
            htmlString = htmlString.Replace("<%perdiemonland%>", contractor.Per_Diem_On_Land.ToString());
            htmlString = htmlString.Replace("<%daysonland%>", contractor.Days_On_Land.ToString());
            htmlString = htmlString.Replace("<%daysonboard%>", contractor.Days_OnBoard.ToString());
            htmlString = htmlString.Replace("<%totaldiem%>", contractor.Total_Per_Diem.ToString());
            htmlString = htmlString.Replace("<%daysonland%>", contractor.Days_On_Land.ToString());
            htmlString = htmlString.Replace("<%dayrate%>", contractor.Day_Rate.ToString());
            if (val == 1)
            {
                point3 = point3.Replace("<%option%>", "$" + contractor.Day_Rate.ToString() + " Daily Rate=");
            }
            else if (val == 2)
            {
                point3 = point3.Replace("<%option%>", "$" + weeklyrate + " Weekly Rate=");
            }
            else
            {
                point3 = point3.Replace("<%option%>", "");
            }

            if (ship != null)
            {
                htmlString = htmlString.Replace("<%ship%>", ship.Name);
            }
            if (position != null)
            {
                htmlString = htmlString.Replace("<%position%>", position.Name);
            }
            htmlString = htmlString.Replace("<%point3%>", point3);

















            StringBuilder strBody = new StringBuilder();
            strBody.Append("<html " + "xmlns:o='urn:schemas-microsoft-com:office:office' " + "xmlns:w='urn:schemas-microsoft-com:office:word'" +
                    "xmlns='http://www.w3.org/TR/REC-html40'>" +
                        "<head><title>Agreement</title>");



            strBody.Append("<!--[if gte mso 9]>" +
                                     "<xml>" +
                                     "<w:WordDocument>" +
                                     "<w:View>Print</w:View>" +
                                     "<w:Zoom>90</w:Zoom>" +
                                     "<w:DoNotOptimizeForBrowser/>" +
                                     "</w:WordDocument>" +
                                     "</xml>" +
                                     "<![endif]-->");

            strBody.Append("<style>" +
                                    "<!-- /* Style Definitions */" +
                                    "@page Section1" +
                                    "   {size:8.5in 11.0in; " +
                                    "   margin:1.0in 1.0in 1.0in 1.0in ; text-align:justify;font-family: 'Times New Roman', Georgia, Serif; " +
                                    "   mso-header-margin:.5in; " +
                                    "   mso-footer-margin:.5in; mso-paper-source:0;}" +
                                    "@page indent{text-indent: 40px;}" +
                                    "p.indent{page:indent}" +
                                     "@page indentdouble{text-indent: 40px;}" +
                                    "p.indentdouble{page:indentdouble}" +
                                    " div.Section1" +
                                    "   {page:Section1;}" +
                                    "-->" +
                                   "</style></head>");

            strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" +
                                    "<div class=Section1>" +
                                    htmlString +
                                    "</div></body></html>");

            name = name.Replace("'", "");
            name = name.Replace("\\", "");
            name = name.Replace("//", "");
            name = name.Replace("\\", "");
            name = name.Replace(",", "");
            name = name.Replace("\"", "");
            Response.AddHeader("Content-Disposition", "filename=" + name + ".doc");
            Response.ContentType = "application/msword;charset=utf-8";
            Response.Write(strBody);
            // return Json("1",JsonRequestBehavior.AllowGet);
        }




        public ActionResult GetPersonByID(int ID)
        {
            var person = ShrdMaster.Instance.GetPersonByID(ID);
            if (person == null)
            {
                return Json("-1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(person, JsonRequestBehavior.AllowGet);

            }

        }


        public ActionResult GetShipsByBrand(string Brand)
        {
            var data=db.cruises.Where(x => x.ShipBrand.ToUpper() == Brand.ToUpper()).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetShowsByShip(int id)
        {
            var data = db.shows.Where(x => x.Ship == id).OrderBy(x=>x.Name).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}