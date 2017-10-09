using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CruiseEntertainmentManagnmentSystem.Models;
using CruiseEntertainmentManagnmentSystem.ViewModel;
using Microsoft.AspNet.Identity;
using SelectPdf;
using System.Data.Entity;
using CruiseEntertainmentManagnmentSystem.Filters;

namespace CruiseEntertainmentManagnmentSystem.Controllers.User
{
    [Authorize(Roles="User")]
    [InitializeSimpleMembership]
    public class UserHomeController : Controller
    {
        //
        // GET: /UserHome/
        CemsDbContext db = new CemsDbContext();
        PersonalInformation information;
        public ActionResult Index()
        {
          
            var Person = ShrdMaster.Instance.GetPersonByUserName(User.Identity.Name);
            Session["Person"] = Person;
            return View();
        }

        public Persons GetPerson()
        {
            Persons person;

            person = SessionContext<Persons>.Instance.GetSession("User");
            if(person==null)
            {
                Response.Redirect("/account/login");
            }
            return person;
        }


        public ActionResult ProfileView()
        {
            ViewBag.Countries = new SelectList(ShrdMaster.Instance.GetCountries(), "Name", "Name");
            Persons userdata;
            information=null;
            userdata = ShrdMaster.Instance.GetPersonByUserName(User.Identity.Name);
            ViewBag.Categories = new SelectList(db.categories.ToList(),"ID","Name");
            
            SessionContext<Persons>.Instance.SetSession("User", userdata);
            //PersonalInformation vm = new PersonalInformation();
            if(userdata != null)
            {
                ViewBag.FullName= userdata.FirstName+" "+ userdata.LastName;
                ViewBag.Email = userdata.Email;
                Session["LoggedUser"] = userdata;

                information = ShrdMaster.Instance.GetInformation(userdata.ID);
                SessionContext<PersonalInformation>.Instance.SetSession("PersonalInformation", information);

                //vm.LastName = Userdata.LastName;
                //vm.Email = Userdata.Email;
            }
            else
            {
                RedirectToAction("Login", "Account");
            }

            if(information != null)
            {
                ViewBag.Categories = new SelectList(db.categories.ToList(), "ID", "Name", information.CategoryID);

                ViewBag.Positions = ShrdMaster.Instance.GetPositionsBYPersonIdAndCategoryID(userdata.ID, information.CategoryID);
                if(information.Sex==0)
                {
                    information.IsFemale = true;
                }
                return View(information);
            }
            return View();
        }

        public ActionResult GetPositions(int ID)
        {
            var data = db.positions.Where(x => x.CategoryID == ID).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProfileView(PersonalInformation model)
        {
            Persons person = GetPerson();
            if (ModelState.IsValid)
            {
                
                if(person!=null)
                {
                    if(model.ID>0)
                    {
                        db.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        model.PersonID = person.ID;
                        db.PersonalInformations.Add(model);
                    }
                    if(model.PositionList!=null && model.PositionList.Count()>0)
                    {
                        db.Database.ExecuteSqlCommand("exec sp_SavePositionsForContractorProfile @input,@personID,@categoryID", new SqlParameter("@input", model.PositionList),
                        new SqlParameter("@personID", model.PersonID),
                        new SqlParameter("@CategoryID", model.CategoryID));
                    }                    
                }                                
                db.SaveChanges();
                ViewBag.Success = "1";
            }

            ViewBag.Categories = new SelectList(db.categories.ToList(), "ID", "Name", model.CategoryID);
            ViewBag.Positions = ShrdMaster.Instance.GetPositionsBYPersonIdAndCategoryID(person.ID,model.CategoryID);
            
            return View(model);
        }

        public ActionResult UploadImages()
        {
            int ID=0;
            if(Request.QueryString["id"]!=null)
            {
                int.TryParse(Request.QueryString["id"], out ID);
            }
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var person = GetPerson();
                    string Filename = file.FileName;
                    string ext = Filename.Substring(Filename.LastIndexOf('.'), (Filename.Length - Filename.LastIndexOf('.')));
                    Filename = person.ID + ext;

                    string path = Server.MapPath("~/ProfileImages");
                    path = Path.Combine(path, Filename);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    file.SaveAs(path);
                string imagePath = "/ProfileImages/" + Filename;
                if(ID>0)
                {
                    db.Database.ExecuteSqlCommand("exec SP_SaveProfilePhoto @imagePath,@personID", new SqlParameter("@Imagepath", imagePath), new SqlParameter("@personID", ID));
                }
                return Json(imagePath, JsonRequestBehavior.AllowGet);           
                }
            return Json("-1", JsonRequestBehavior.AllowGet);
            

            
        }


        #region CrewData
        [AllowAnonymous]
        public ActionResult CrewDataForm(int option=0,int personID=0)
        {

            Persons person=null;            
            
            //information = SessionContext<PersonalInformation>.Instance.GetSession("PersonalInformation");
           
            if(personID<=0)
            {
                person = GetPerson();
                personID = person.ID ;
            }
           
            //ViewBag.SSN = information.SSN;
            ViewBag.Ships = new SelectList(ShrdMaster.Instance.GetShips("Norwegian"), "ID", "Name");

            ViewBag.Department = new SelectList(ShrdMaster.Instance.GetDepartmentforCrewDataForm(), "ID", "Name");
            ViewBag.Positions = new SelectList(ShrdMaster.Instance.GetPostionsforPIF(personID), "ID", "Name");
            var CrewDataFormInfo = db.Database.SqlQuery<CrewDataFormViewModel>("exec sp_getCrewDataFormsByPersonID @personID", new SqlParameter("@personID", personID)).FirstOrDefault();
            if(option==1)
            {
                return PartialView("_CrewDataForm", CrewDataFormInfo);
            }
            else if(option==2)
            {
                return PartialView("_CrewDataForm");
            }
            
            return View(CrewDataFormInfo);
        }

        [HttpPost]
        public ActionResult CrewDataForm(CrewDataForm model)
        {
            Persons person;
            person = GetPerson();
            model.PersonID = person.ID;
            if (ModelState.IsValid)
            {
                if (model.ID > 0)
                {
                    db.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.PersonID = person.ID;
                    db.CrewdataForms.Add(model);
                }
                db.SaveChanges();
            }

            Session["Person"] = model;
            //ViewBag.Ship = new SelectList(db.cruises, "ID", "Name", person.Ship);
            //ViewBag.Status = new SelectList(Common.GetStatus(), "ID", "Value", person.Status);
            //ViewBag.Position = new SelectList(db.positions, "ID", "Name", person.Position);
            return RedirectToAction("CrewDataForm");
        }

        public void CrewDataFormPDF(int option=0)
        {
            string url;
            var person = GetPerson();
            ViewBag.Positions = new SelectList(ShrdMaster.Instance.GetPostionsforPIF(person.ID), "ID", "Name");
            ViewBag.Ships = new SelectList(ShrdMaster.Instance.getShipsForPIF("Regent", "Oceania"), "ID", "Name");
             url = "http://cemsapp.infodatixhosting.com/UserHome/W9?option=1&personID=" + person.ID;
            //url = "http://localhost:64819/UserHome/CrewDataForm?option=" + option + "&personID=" + person.ID;
            string fileName = "CrewDataForm_" + person.ID + ".pdf";
            string path = Server.MapPath("~/PDF");
            path = Path.Combine(path, fileName);
            GeneratePDF(url, path, fileName);
        }
        #endregion CrewData

        #region PersonalInformation
        [AllowAnonymous]
        public ActionResult PersonalInformationForm(int option=0,int personID=0)
        {
            Persons person;
            if(personID<=0)
            {
                person = GetPerson();
                personID = person.ID;
            }


            var data = db.Database.SqlQuery<PIFViewModel>("exec sp_getPersonalInformationFormsByPersonID @personID", new SqlParameter("@personID", personID)).FirstOrDefault();
            ViewBag.Positions = new SelectList(ShrdMaster.Instance.GetPostionsforPIF(personID), "ID", "Name");
            
                ViewBag.Ships = new SelectList(ShrdMaster.Instance.getShipsForPIF("Regent", "Oceania"), "ID", "Name");
            
            if (option==1)
            {
                data.Ships = ShrdMaster.Instance.getShipsForPIF("Regent", "Oceania");
                return PartialView("_Personalnfo",data);
            }
            else if(option==2)
            {
                return PartialView("_Personalnfo");
            }
            //data.Ships = ShrdMaster.Instance.getShipsForPIF("Regent", "Oceania");
          
            return View(data);
        }

        [HttpPost]
        public ActionResult PersonalInformationForm(PersonalInformationForm model)
        {
            var person = GetPerson();

            if (ModelState.IsValid)
            {
                if (model.ID > 0)
                {
                    db.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.PersonID = person.ID;
                    db.PersonalInformationForms.Add(model);
                }
                db.SaveChanges();
            }
            ViewBag.Positions = new SelectList(ShrdMaster.Instance.GetPostionsforPIF(person.ID), "ID", "Name");
            ViewBag.Ships = new SelectList(ShrdMaster.Instance.getShipsForPIF("Regent", "Oceania"), "ID", "Name");
            return RedirectToAction("PersonalInformationForm");

        }

        public void PersonalInformationFormPDF(int option=0)
        {
            string url;
            var person = GetPerson();
            ViewBag.Positions = new SelectList(ShrdMaster.Instance.GetPostionsforPIF(person.ID), "ID", "Name");
            ViewBag.Ships = new SelectList(ShrdMaster.Instance.getShipsForPIF("Regent", "Oceania"), "ID", "Name");
             url = "http://cemsapp.infodatixhosting.com/UserHome/W9?option=1&personID=" + person.ID;
            //url = "http://localhost:64819/UserHome/PersonalInformationForm?option="+option+"&personID=" + person.ID;
            string fileName = "PersonalInformationForm_" + person.ID+".pdf";
            string path=Server.MapPath("~/PDF");
            path = Path.Combine(path, fileName);
            GeneratePDF(url,path,fileName);            
        }

        #endregion PersonalInformation


        public ActionResult ShipsAndShows()
        {
            ViewBag.Ships = new SelectList(db.cruises.OrderBy(x => x.Name).ToList(), "ID", "Name");            
            return View();
        }

        public ActionResult GetShows(int ShipID)
        {
            var data = db.shows.Where(x => x.Ship == ShipID).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #region TravelREquestForm
        public ActionResult TravelRequestForm()
        {
            ViewBag.Cruises = new SelectList(db.cruises.OrderBy(x => x.Name).ToList(), "ID", "Name");
            return View();
        }

        public ActionResult GetAllTRF()
        {
            Persons person;
            person = GetPerson();
            List<TRFModel> list=new List<TRFModel>();
            if (person != null)
            {
                //db.TRFs.Where(x => x.Person == person.ID);
                //var data=db.CruiseSubSchedules.Where(x => x.PersonID == person.ID && x.CruiseID == person.Ship).ToList();
                list = db.Database.SqlQuery<TRFModel>("exec GetTrfsByPersonID @personID", new SqlParameter("@personID", person.ID)).ToList();
                //foreach(CruiseSubSchedule )

            }
            return PartialView("_TRFPartial",list);
        }



        public ActionResult TRFEdit(int cruiseID)
        {
            Persons person;
            person = GetPerson();
           // var data = db.TRFs.ToList();
            TRFDataViewModel list = null;
            list = db.Database.SqlQuery<TRFDataViewModel>("execute GetTRFforPerson @personID, @CruiseID ", new SqlParameter("@personID", person.ID), new SqlParameter("@CruiseID", cruiseID)).FirstOrDefault();


           
            
            if (list == null)
            {
                list = new TRFDataViewModel();
                //list.Gender = person.Sex;
                //list.FirstName = person.FirstName;
                //list.LastName = person.LastName;
                //list.Nationality = person.CitizenShip;
                //list.Cruise = person.Ship;
                //if (person.DOB != null)
                //{
                //    list.DOB= person.DOB.Value;
                //}
                TRFModel lst = new TRFModel();

                lst = db.Database.SqlQuery<TRFModel>("exec GetTrfsByPersonID @personID", new SqlParameter("@personID", person.ID)).Where(x => x.CruiseID == cruiseID).FirstOrDefault();
                if(lst!=null)
                {
                    list.ArrivalDate = lst.EndDate;
                    list.DepartureDate = lst.StartDate;
                
                }
                var cruise = db.cruises.Where(x => x.ID == cruiseID).FirstOrDefault();
                if(cruise!=null)
                {
                    list.AccountNumber = cruise.AccountNumber;
                    list.Cruise = cruise.ID;                    
                }
                
            }
            
            if (list.Gender == 0)
            {
                list.IsFemale = true;
            }
            else
            {
                list.IsFemale = false;
            }
            

           
           
           

            ViewBag.Cruise = new SelectList(db.cruises, "ID", "Name",cruiseID);

            return View(list);
        }

        [HttpPost]
        public ActionResult TRFEdit(TRF model)
        {
                Persons person;
            person = GetPerson();
            if (model.ID == 0)
                {


                model.Person = person.ID;
                db.TRFs.Add(model);
            }
            else
            {
                db.Entry(model).State = EntityState.Modified;
            }
            db.SaveChanges();
            ViewBag.Cruise = new SelectList(db.cruises, "ID", "Name",model.Cruise);
            return RedirectToAction("TRF");
        }


        [HttpPost]
        public ActionResult TravelRequestFormCreate(TRF model)
        {
            Persons person;
            if(ModelState.IsValid)
            {
                person = GetPerson();
                model.Person = person.ID;
                db.TRFs.Add(model);
                db.SaveChanges();
                return RedirectToAction("TravelRequestForm");                
            }
            return RedirectToAction("TravelRequestForm",model);
            
        }
        #endregion TravelREquestForm

        public ActionResult FinanceAndPayments()
        {
            return View();
        }

        #region W9
        [AllowAnonymous]
        public ActionResult w9(int option=0,int personID=0)
        {
            Persons person;
            if (personID >0)
            {
                person=ShrdMaster.Instance.GetPersonByID(personID);
            } 
            else
            {
                person = GetPerson();
            }
            
            WNineViewModel list = null;

            list = db.Database.SqlQuery<WNineViewModel>("exec sp_GetPersonsWithInformation @personID",new SqlParameter("@personID",person.ID)).FirstOrDefault();
            //list = db.WNines.Where(x => x.Person == person.ID).Select(x => new WNineViewModel
            //{
                
            //    ID= x.ID,
            //    CCorporation=x.CCorporation,
            //    SCorporation=x.SCorporation,
            //    SoleProprietor=x.SoleProprietor,
            //    Trust=x.Trust,
            //    TaxClassification=x.TaxClassification,
            //    Other=x.Other,
            //    OtherText=x.OtherText,
            //    PartnerShip=x.PartnerShip,
            //    RequestorName=x.RequestorName,
            //    EmployerIdentificationNumber=x.EmployerIdentificationNumber,
            //    ExemptPayeeCode=x.ExemptPayeeCode,
            //    FATCACode=x.FATCACode ,
            //    BusinessName=x.BusinessName,
            //    AccountNumber=x.AccountNumber,
            //    Person=x.Person
            //}).FirstOrDefault();
            if (list == null)
            {
                list = new WNineViewModel();
            }
            
          //  list.Name = person.FirstName+" "+person.LastName;
            //list.Address = person.Address;
            //list.City = person.City;
            //list.State = person.State;
            //list.Zip = person.Zip;
            //list.Person = person.ID;
            //list.SSN = person.SSN;
            if(option==1)
            {
                return View("w9pdf",list);
            }
            else if(option==2)
            {
                return View("w9pdf");
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult w9(WNine model)
        {
            Persons person;
            person = GetPerson();
            if (model.ID == 0)
            {
                model.Person = person.ID;
                db.WNines.Add(model);
            }
            else
            {
                db.Entry(model).State = EntityState.Modified;
            }
            db.SaveChanges();
            
            return Json("1",JsonRequestBehavior.AllowGet);
        }

        public void GeneratePDF(string url,string path,string filename)
        {
            HtmlToPdf Convertor = new HtmlToPdf();
            Logger.Instance.Log("Entered in ok part......2.3");

            Logger.Instance.Log("Entered in ok part......3");
            string htmlCode;
            using (WebClient client = new WebClient())
            {

                // Download as a string.
                Logger.Instance.Log("Entered in ok part......3.1");
                htmlCode = client.DownloadString(url);
                Logger.Instance.Log("Entered in ok part......4");
            }

            Logger.Instance.Log("Entered in ok part......5");



            Convertor.Options.MarginTop = 10;
            Convertor.Options.MarginBottom = 10;
            Convertor.Options.MarginLeft = 5;
            Convertor.Options.MarginRight = 5;
            // Convertor.Options.WebPageHeight = 1000;
            PdfDocument doc = Convertor.ConvertHtmlString(htmlCode);
           
            doc.Save(path);
            Logger.Instance.Log("Entered in ok part......1");
            // close pdf document
            doc.Close();
            MemoryStream Stream = new MemoryStream();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode(filename));
            Response.ContentType = "APPLICATION/pdf";
            Stream writeToServer = new FileStream(path, FileMode.OpenOrCreate);
            Stream.WriteTo(writeToServer);
            Stream.Close();
            writeToServer.Close();
            Response.WriteFile(path, false);
        }
       
        public void GenerateW9PDF(int option)
        {
            Persons person;
            person = GetPerson();
            Logger.Instance.Log("Entered in ok part......2");
            /// convert to PDF
          
            Logger.Instance.Log("Entered in ok part......2.1");
            //// create a new pdf document converting an url
            string url;
            //url = "http://localhost:64819/UserHome/W9?option=" + option + "&personID=" + person.ID;
             url = "http://cemsapp.infodatixhosting.com/UserHome/W9?option=1&personID=" + person.ID;
            string filename = person.ID + "_W9Form.Pdf";
            string path = Server.MapPath("/PDF//");
            path += filename;

            GeneratePDF(url, path, filename);
            //  return File(path,"application/PDF");
        }
        #endregion W9



        #region FASTPAY
        [AllowAnonymous]
        public ActionResult FastPay(int option = 0,int personID=0)
        {
            Persons person;
                   
            if(personID <= 0)
            {
                person = GetPerson();
                personID = person.ID;
            }

            var data = db.Database.SqlQuery<FastPayFormViewModel>("exec sp_GetFastPayFormByPersonID @personID", new SqlParameter("@personID", personID)).FirstOrDefault();
            if(option==1)
            {
                return View("_FastPaypdf",data);
            }
            else if(option==2)
            {
                return View("_FastPaypdf");
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult FastPay(FastPayForm model)
        {
            Persons person;
            person = GetPerson();
            if(ModelState.IsValid)
            {
                if(model.ID>0)
                {
                    db.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.PersonID = person.ID;
                    db.FastPayForms.Add(model);
                }
                
                db.SaveChanges();
                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("-1", JsonRequestBehavior.AllowGet);
        }

        public void FastPayFormPDF(int option)
        {
            Persons person;
            person = GetPerson();
            Logger.Instance.Log("Entered in ok part......2");
            /// convert to PDF

            Logger.Instance.Log("Entered in ok part......2.1");
            //// create a new pdf document converting an url
            string url;
            //url = "http://localhost:64819/UserHome/FastPay?option=" + option + "&personID=" + person.ID;
             url = "http://cemsapp.infodatixhosting.com/UserHome/W9?option=1&personID=" + person.ID;
            string filename = person.ID + "_FastPayForm.Pdf";
            string path = Server.MapPath("/PDF/");
            path += filename;

            GeneratePDF(url, path,filename);
        }




        #endregion FASTPAY

        #region NEWVENDOR
        [AllowAnonymous]
        public ActionResult NewVendor(int option=0,int personID=0)
        {
            Persons person;
            if(personID<=0)
            {
                person = GetPerson();
                personID = person.ID;
            }
            var data = db.Database.SqlQuery<NewVendorViewModel>("exec sp_GetNewVendorFormsByPersonID @personID ",new SqlParameter("@personID",personID)).FirstOrDefault();

            if(option == 1)
            {
                return View("_NewVendorpdf", data);
            }
            else if(option==2)
            {
                return View("_NewVendorpdf");
            }

            return View(data);
        }

        [HttpPost]
       
        public ActionResult NewVendor(NewVendorForm model)
        {
            Persons person;
            person = GetPerson();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ID > 0)
                    {
                        db.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        model.PersonID = person.ID;
                        db.NewVendorForms.Add(model);

                    }
                    db.SaveChanges();
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
            return Json("-1", JsonRequestBehavior.AllowGet);
        }



        public void NewVendorFormPDF(int option)
        {
            Persons person;
            person = GetPerson();
            Logger.Instance.Log("Entered in ok part......2");
            /// convert to PDF

            Logger.Instance.Log("Entered in ok part......2.1");
            //// create a new pdf document converting an url
            string url;
            //url = "http://localhost:64819/UserHome/NewVendor?option=" + option + "&personID=" + person.ID;
             url = "http://cemsapp.infodatixhosting.com/UserHome/W9?option=1&personID=" + person.ID;
            string filename = person.ID + "_NewVendorForm.Pdf";
            string path = Server.MapPath("/PDF/");
            path += filename;

            GeneratePDF(url, path, filename);
        }
        #endregion NEWVENDOR


        #region VENDORPAYMENT        
        public ActionResult VendorPayment()
        {
            return View();
        }
        #endregion


        public ActionResult Personalnfo()
        {
            return View();
        }
        //public ActionResult GetTRFData()
        //{
        //    var data=db.persons.ToList();
        //    return PartialView("TRFData",data);
        //}
        public ActionResult ContactNCL()
        {
            //var data=db.persons.ToList();
            //return PartialView("TRFData",data);
            return View();
        }

        public ActionResult GetContacts()
        {
            var data = db.ContactLists.Where(x=>x.Active==true).ToList();

            return PartialView("_ContactNCL", data);
        }


        public ActionResult GetPDF()
        {
            return View("NewVendor_pdf");
        }
    }
}
