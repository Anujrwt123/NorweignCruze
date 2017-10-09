using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SelectPdf;
using static System.Web.HttpContext;
using System.Web;
using System.Data.SqlClient;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class ShrdMaster
    {

        private static ShrdMaster _instance;
        private CemsDbContext db = new CemsDbContext();

        public static ShrdMaster Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShrdMaster();
                }

                return _instance;
            }
        }



        public string GenerateWord(string URL)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(URL);
            }

            return htmlCode;
        }



        public Persons GetPersonByUserName(string username)
        {
            var Person = db.persons.Where(x => x.Email == username).FirstOrDefault();

            return Person;
        }

        public bool CheckTRF(int ID)
        {
            var data = db.TRFs.Where(x => x.Person == ID).SingleOrDefault();
            if (data == null)
            {
                return false;
            }

            return true;

        }

        public IEnumerable<Persons> GetPersons()
        {

            var data = db.persons.ToList();
            data.ForEach(person => { person.FullName = person.FirstName + " " + person.LastName; });
            //var data =from person in db.persons
            //            select new Persons
            //            {
            //                FullName = person.FirstName + " " + person.LastName,
            //                Password = person.Password,
            //                Phone = person.Phone,
            //                Email = person.Email,
            //                DayRate = person.DayRate,
            //                WeeklySalary = person.WeeklySalary,
            //                SSN = person.SSN,
            //                ID = person.ID
            //            };

            return data.OrderBy(x => x.FullName);
            //db.persons.Select(x => new { }).ToList();
        }

        public PdfDocument GeneratePDF(string URL)
        {
            /// convert to PDF
            HtmlToPdf converter = new HtmlToPdf();

            //// create a new pdf document converting an url
            //string url = "http://localhost:51369/Customer/InvoicePrint?orderID=" + orderID + "&PDF=1";


            string htmlCode;
            using (WebClient client = new WebClient())
            {

                // Download as a string.
                htmlCode = client.DownloadString(URL);
            }
            PdfDocument doc = converter.ConvertHtmlString(htmlCode);


            return doc;

            //string path = Server.MapPath("/PDF//");
            //path += order.ID + "_Receipt.Pdf";
            //doc.Save(path);
            //// close pdf document
            //doc.Close();
            ////sending mail 
            //var student = db.Students.Find(order.StudentID);
            //var org = db.Organizations.Find(order.SchoolID);
            //EmailService email = new EmailService(path);
            //IdentityMessage details = new IdentityMessage();
            //details.Destination = order.EmailAddress;
            //details.Subject = "Receipt! Fundraisingshop.com";
            //Dictionary<string, string> param = new Dictionary<string, string>();
            //if (org != null)
            //{
            //    param.Add("<%Student%>", student.StudentName);
            //    param.Add("<%School%>", org.Name);
            //}
            //else
            //{
            //    param.Add("<%Student%>", " ");
            //    param.Add("<%School%>", " ");
            //}

            //param.Add("<%customer%>", order.FullName);
            //details.Body = ShrdMaster.Instance.buildEmailBody("InvoiceEmailTemplate.txt", param);
            //string attachment = path;
            //await email.SendAsync(details);
        }




        public List<Persons> GetPersonsByCategoryID(int ID)
        {
            var list = db.persons.Join(db.PersonMappings, pr => pr.ID, pm => pm.PersonID, (pr, pm) => new { Persons = pr, PersonMapping = pm })
                .Where(x => x.PersonMapping.CategoryID == ID).Select(x => x.Persons).ToList();
            //var l = list.ToList();
            list.ForEach(x => x.Checked = "checked");

            var personsList = db.persons.ToList().Except(list).AsEnumerable();
            personsList.ToList().ForEach(x => x.Checked = null);

            var joinlist = list.Union(personsList).OrderByDescending(x => x.Checked).ToList();
            var finalList = joinlist.Select(x => new Persons
            {
                FullName = x.FirstName + " " + x.LastName,
                ID = x.ID,
                Checked = x.Checked
            }).ToList();

            return finalList;
        }


        public List<Category> GetCategoryIdByPersonID(int ID)
        {
            var list = db.categories.Join(db.PersonMappings, pr => pr.ID, pm => pm.CategoryID, (pr, pm) => new { Category = pr, PersonMapping = pm }).Where(x => x.PersonMapping.PersonID == ID).Select(x => x.Category).ToList();
            //var l = list.ToList();
            list.ForEach(x => x.Checked = "checked");

            var categoryList = db.categories.ToList().Except(list).AsEnumerable();
            categoryList.ToList().ForEach(x => x.Checked = null);

            var finalList = list.Union(categoryList).OrderByDescending(x => x.Checked).ToList();

            return finalList;
        }

        public void SavePersonMapping(string list, int PersonID)
        {
            if (!string.IsNullOrEmpty(list))
            {
                string[] catList = list.Split(',');
                PersonMapping map = null;
                int catID = 0;
                var prevList = db.PersonMappings.Where(x => x.PersonID == PersonID).ToList();
                if (prevList.Count > 0)
                {
                    prevList.ForEach(x => { db.PersonMappings.Remove(x); db.SaveChanges(); });
                }



                foreach (string str in catList)
                {
                    map = new PersonMapping();
                    map.PersonID = PersonID;
                    int.TryParse(str, out catID);
                    map.CategoryID = catID;
                    db.PersonMappings.Add(map);
                    db.SaveChanges();
                }


            }
        }



        public bool CheckUserName(string username)
        {
            var data = db.persons.Where(x => x.Email == username).SingleOrDefault();

            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public Persons GetPersonByID(int ID)
        {
            var person = db.persons.Find(ID);
            return person;
        }

        public bool IsUserAdmin(string username)
        {
            var users = db.persons.FirstOrDefault(x => x.Email == username);
            UserProfile adminusers = null;
            UserRole roles = null;
            if (users == null)
            {
                adminusers = db.UserProfiles.FirstOrDefault(x => x.UserName == username);
                roles = db.UserRoles.FirstOrDefault(x => x.UserID == adminusers.UserId && x.RoleID == 1);
            }
            else
            {
                roles = db.UserRoles.FirstOrDefault(x => x.UserID == users.ID && x.RoleID == 1);
            }

            if (roles != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        public string GetReturnUrl(string defaultUrl)
        {
            //if (defaultUrl == null) throw new ArgumentNullException(nameof(defaultUrl));


            if (Current.Request.QueryString["ReturnUrl"] != null)
            {
                string url = Current.Request.Url.AbsoluteUri;
                int index = url.IndexOf("returnUrl");
                string returnUrl = url.Substring(index, (url.Length - index));
                returnUrl = returnUrl.Replace("returnUrl=", "");
                defaultUrl = returnUrl;
                //Current.Request.QueryString[$"ReturnUrl"].ToString();
            }

            return defaultUrl;
        }


        public List<ShipBrand> GetBrands()
        {
            var data = db.Database.SqlQuery<ShipBrand>("exec sp_GetShipBrands").ToList();

            return data;
        }


        public List<Cruises> GetShips(string ShipBrand = "")
        {
            if (string.IsNullOrEmpty(ShipBrand))
            {
                return db.cruises.ToList();
            }
            return db.cruises.Where(x => x.ShipBrand.ToUpper() == ShipBrand.ToUpper()).ToList();
        }

        public List<Shows> GetShowsByContractorID(int id, int shipID)
        {
            var list = db.shows.Join(db.ContractorShows, pr => pr.ID, pm => pm.ShowID, (pr, pm) => new { Show = pr, ContractorShow = pm })
                .Where(x => x.ContractorShow.ContractorID == id && x.Show.Ship == shipID).Select(x => x.Show).ToList();
            //var l = list.ToList();
            list.ForEach(x => x.Checked = "checked");

            var showsList = db.shows.Where(x => x.Ship == shipID).ToList().Except(list).AsEnumerable();
            showsList.ToList().ForEach(x => x.Checked = null);

            var finalList = list.Union(showsList).OrderByDescending(x => x.Checked).ToList();

            return finalList;
        }


        public List<CrewDepartment> GetDepartmentforCrewDataForm()
        {
            List<CrewDepartment> list = new List<CrewDepartment>() {
                new CrewDepartment() { ID="Entertainment Contractor",Name="Entertainment Contractor"}
            };

            return list;
        }

        public List<Cruises> getShipsForPIF(string ShipBrand, string ShipBrand1)
        {
            if (string.IsNullOrEmpty(ShipBrand))
            {
                return db.cruises.ToList();
            }
            return db.cruises.Where(x => x.ShipBrand.ToUpper() == ShipBrand.ToUpper() || x.ShipBrand.ToUpper() == ShipBrand1.ToUpper()).ToList();
        }

        public List<Position> GetPostionsforPIF(int personID)
        {
            var positons = db.PositionMappings.Where(x => x.PersonID == personID).AsEnumerable();
            var data = db.positions.Join(positons, p => p.ID, pm => pm.PositionID, (p, pm) => new { Position = p, PositionMapping = pm })
                .Select(x => x.Position).ToList();
            return data;
        }


        public List<Position> GetPositionsBYPersonIdAndCategoryID(int personID, int categoryID)
        {
            var positions = db.PositionMappings.Where(x => x.PersonID == personID && x.CategoryID == categoryID).AsEnumerable();
            var list = db.positions.Join(positions, pr => pr.ID, pm => pm.PositionID, (pr, pm) => new { Position = pr, PositionMapping = pm }).Select(x => x.Position).ToList();
            //var l = list.ToList();
            list.ForEach(x => x.Checked = "checked");

            var positionsList = db.positions.Where(x => x.CategoryID == categoryID).ToList().Except(list).AsEnumerable();
            positionsList.ToList().ForEach(x => x.Checked = null);

            var finalList = list.Union(positionsList).OrderByDescending(x => x.Checked).ToList();

            return finalList;
        }

        public PersonalInformation GetInformation(int ID)
        {
            var data = db.Database.SqlQuery<PersonalInformation>("exec sp_GetpersonalInformationByPersonID @personID", new SqlParameter("@personID", ID)).FirstOrDefault();
            return data;//db.PersonalInformations.FirstOrDefault(x => x.PersonID == ID);
        }


        #region Users
        public void ChangePassword(string newPassword, int ID)
        {
            db.Database.ExecuteSqlCommand("sp_ChangePassword @id,@newPassword", new SqlParameter("@id", ID), new SqlParameter("@newPassword", newPassword));
        }
        #endregion

        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>()
            {
                    new Country() { ID=1, Name="Afghanistan"},
                    new Country() { ID=2, Name="Albania"},
                    new Country() { ID=3, Name="Algeria"},
                    new Country() { ID=4, Name="Andorra"},
                    new Country() { ID=5, Name="Angola"},
                    new Country() { ID=6, Name="Antigua and Barbuda"},
                    new Country() { ID=7, Name="Argentina"},
                    new Country() { ID=8, Name="Armenia"},
                    new Country() { ID=9, Name="Australia"},
                    new Country() { ID=10, Name="Austria"},
                    new Country() { ID=11, Name="Azerbaijan"},
                    new Country() { ID=12, Name="Bahamas"},
                    new Country() { ID=13, Name="Bahrain"},
                    new Country() { ID=14, Name="Bangladesh"},
                    new Country() { ID=15, Name="Barbados"},
                    new Country() { ID=16, Name="Belarus"},
                    new Country() { ID=17, Name="Belgium"},
                    new Country() { ID=18, Name="Belize"},
                    new Country() { ID=19, Name="Benin"},
                    new Country() { ID=20, Name="Bhutan"},
                    new Country() { ID=21, Name="Bolivia"},
                    new Country() { ID=22, Name="Bosnia and Herzegovina"},
                    new Country() { ID=23, Name="Botswana"},
                    new Country() { ID=24, Name="Brazil"},
                    new Country() { ID=25, Name="Brunei"},
                    new Country() { ID=26, Name="Bulgaria"},
                    new Country() { ID=27, Name="Burkina Faso"},
                    new Country() { ID=28, Name="Burundi"},
                    new Country() { ID=29, Name="Cabo Verde"},
                    new Country() { ID=30, Name="Cambodia"},
                    new Country() { ID=31, Name="Cameroon"},
                    new Country() { ID=32, Name="Canada"},
                    new Country() { ID=33, Name="Central African Republic (CAR)"},
                    new Country() { ID=34, Name="Chad"},
                    new Country() { ID=35, Name="Chile"},
                    new Country() { ID=36, Name="China"},
                    new Country() { ID=37, Name="Colombia"},
                    new Country() { ID=38, Name="Comoros"},
                    new Country() { ID=39, Name="Democratic Republic of theCongo"},
                    new Country() { ID=40, Name="Republic of the Congo"},
                    new Country() { ID=41, Name="Costa Rica"},
                    new Country() { ID=42, Name="Cote d'Ivoire"},
                    new Country() { ID=43, Name="Croatia"},
                    new Country() { ID=44, Name="Cuba"},
                    new Country() { ID=45, Name="Cyprus"},
                    new Country() { ID=46, Name="Czech Republic"},
                    new Country() { ID=47, Name="Denmark"},
                    new Country() { ID=48, Name="Djibouti"},
                    new Country() { ID=49, Name="Dominica"},
                    new Country() { ID=50, Name="Dominican Republic"},
                    new Country() { ID=51, Name="Ecuador"},
                    new Country() { ID=52, Name="Egypt"},
                    new Country() { ID=53, Name="El Salvador"},
                    new Country() { ID=54, Name="Equatorial Guinea"},
                    new Country() { ID=55, Name="Eritrea"},
                    new Country() { ID=56, Name="Estonia"},
                    new Country() { ID=57, Name="Ethiopia"},
                    new Country() { ID=58, Name="Fiji"},
                    new Country() { ID=59, Name="Finland"},
                    new Country() { ID=60, Name="France"},
                    new Country() { ID=61, Name="Gabon"},
                    new Country() { ID=62, Name="Gambia"},
                    new Country() { ID=63, Name="Georgia"},
                    new Country() { ID=64, Name="Germany"},
                    new Country() { ID=65, Name="Ghana"},
                    new Country() { ID=66, Name="Greece"},
                    new Country() { ID=67, Name="Grenada"},
                    new Country() { ID=68, Name="Guatemala"},
                    new Country() { ID=69, Name="Guinea"},
                    new Country() { ID=70, Name="Guinea-Bissau"},
                    new Country() { ID=71, Name="Guyana"},
                    new Country() { ID=72, Name="Haiti"},
                    new Country() { ID=73, Name="Honduras"},
                    new Country() { ID=74, Name="Hungary"},
                    new Country() { ID=75, Name="Iceland"},
                    new Country() { ID=76, Name="India"},
                    new Country() { ID=77, Name="Indonesia"},
                    new Country() { ID=78, Name="Iran"},
                    new Country() { ID=79, Name="Iraq"},
                    new Country() { ID=80, Name="Ireland"},
                    new Country() { ID=81, Name="Israel"},
                    new Country() { ID=82, Name="Italy"},
                    new Country() { ID=83, Name="Jamaica"},
                    new Country() { ID=84, Name="Japan"},
                    new Country() { ID=85, Name="Jordan"},
                    new Country() { ID=86, Name="Kazakhstan"},
                    new Country() { ID=87, Name="Kenya"},
                    new Country() { ID=88, Name="Kiribati"},
                    new Country() { ID=89, Name="Kosovo"},
                    new Country() { ID=90, Name="Kuwait"},
                    new Country() { ID=91, Name="Kyrgyzstan"},
                    new Country() { ID=92, Name="Laos"},
                    new Country() { ID=93, Name="Latvia"},
                    new Country() { ID=94, Name="Lebanon"},
                    new Country() { ID=95, Name="Lesotho"},
                    new Country() { ID=96, Name="Liberia"},
                    new Country() { ID=97, Name="Libya"},
                    new Country() { ID=98, Name="Liechtenstein"},
                    new Country() { ID=99, Name="Lithuania"},
                    new Country() { ID=100, Name="Luxembourg"},
                    new Country() { ID=101, Name="Macedonia"},
                    new Country() { ID=102, Name="Madagascar"},
                    new Country() { ID=103, Name="Malawi"},
                    new Country() { ID=104, Name="Malaysia"},
                    new Country() { ID=105, Name="Maldives"},
                    new Country() { ID=106, Name="Mali"},
                    new Country() { ID=107, Name="Malta"},
                    new Country() { ID=108, Name="Marshall Islands"},
                    new Country() { ID=109, Name="Mauritania"},
                    new Country() { ID=110, Name="Mauritius"},
                    new Country() { ID=111, Name="Mexico"},
                    new Country() { ID=112, Name="Micronesia"},
                    new Country() { ID=113, Name="Moldova"},
                    new Country() { ID=114, Name="Monaco"},
                    new Country() { ID=115, Name="Mongolia"},
                    new Country() { ID=116, Name="Montenegro"},
                    new Country() { ID=117, Name="Morocco"},
                    new Country() { ID=118, Name="Mozambique"},
                    new Country() { ID=119, Name="Myanmar (Burma)"},
                    new Country() { ID=120, Name="Namibia"},
                    new Country() { ID=121, Name="Nauru"},
                    new Country() { ID=122, Name="Nepal"},
                    new Country() { ID=123, Name="Netherlands"},
                    new Country() { ID=124, Name="New Zealand"},
                    new Country() { ID=125, Name="Nicaragua"},
                    new Country() { ID=126, Name="Niger"},
                    new Country() { ID=127, Name="Nigeria"},
                    new Country() { ID=128, Name="North Korea"},
                    new Country() { ID=129, Name="Norway"},
                    new Country() { ID=130, Name="Oman"},
                    new Country() { ID=131, Name="Pakistan"},
                    new Country() { ID=132, Name="Palau"},
                    new Country() { ID=133, Name="Palestine"},
                    new Country() { ID=134, Name="Panama"},
                    new Country() { ID=135, Name="Papua New Guinea"},
                    new Country() { ID=136, Name="Paraguay"},
                    new Country() { ID=137, Name="Peru"},
                    new Country() { ID=138, Name="Philippines"},
                    new Country() { ID=139, Name="Poland"},
                    new Country() { ID=140, Name="Portugal"},
                    new Country() { ID=141, Name="Qatar"},
                    new Country() { ID=142, Name="Romania"},
                    new Country() { ID=143, Name="Russia"},
                    new Country() { ID=144, Name="Rwanda"},
                    new Country() { ID=145, Name="Saint Kitts and Nevis"},
                    new Country() { ID=146, Name="Saint Lucia"},
                    new Country() { ID=147, Name="Saint Vincent and the Grenadines"},
                    new Country() { ID=148, Name="Samoa"},
                    new Country() { ID=149, Name="San Marino"},
                    new Country() { ID=150, Name="Sao Tome and Principe"},
                    new Country() { ID=151, Name="Saudi Arabia"},
                    new Country() { ID=152, Name="Senegal"},
                    new Country() { ID=153, Name="Serbia"},
                    new Country() { ID=154, Name="Seychelles"},
                    new Country() { ID=155, Name="Sierra Leone"},
                    new Country() { ID=156, Name="Singapore"},
                    new Country() { ID=157, Name="Slovakia"},
                    new Country() { ID=158, Name="Slovenia"},
                    new Country() { ID=159, Name="Solomon Islands"},
                    new Country() { ID=160, Name="Somalia"},
                    new Country() { ID=161, Name="South Africa"},
                    new Country() { ID=162, Name="South Korea"},
                    new Country() { ID=163, Name="South Sudan"},
                    new Country() { ID=164, Name="Spain"},
                    new Country() { ID=165, Name="Sri Lanka"},
                    new Country() { ID=166, Name="Sudan"},
                    new Country() { ID=167, Name="Suriname"},
                    new Country() { ID=168, Name="Swaziland"},
                    new Country() { ID=169, Name="Sweden"},
                    new Country() { ID=170, Name="Switzerland"},
                    new Country() { ID=171, Name="Syria"},
                    new Country() { ID=172, Name="Taiwan"},
                    new Country() { ID=173, Name="Tajikistan"},
                    new Country() { ID=174, Name="Tanzania"},
                    new Country() { ID=175, Name="Thailand"},
                    new Country() { ID=176, Name="Timor-Leste"},
                    new Country() { ID=177, Name="Togo"},
                    new Country() { ID=178, Name="Tonga"},
                    new Country() { ID=179, Name="Trinidad and Tobago"},
                    new Country() { ID=180, Name="Tunisia"},
                    new Country() { ID=181, Name="Turkey"},
                    new Country() { ID=182, Name="Turkmenistan"},
                    new Country() { ID=183, Name="Tuvalu"},
                    new Country() { ID=184, Name="Uganda"},
                    new Country() { ID=185, Name="Ukraine"},
                    new Country() { ID=186, Name="United Arab Emirates (UAE)"},
                    new Country() { ID=187, Name="United Kingdom (UK)"},
                    new Country() { ID=188, Name="United States of America (USA)"},
                    new Country() { ID=189, Name="Uruguay"},
                    new Country() { ID=190, Name="Uzbekistan"},
                    new Country() { ID=191, Name="Vanuatu"},
                    new Country() { ID=192, Name="Vatican City (Holy See)"},
                    new Country() { ID=193, Name="Venezuela"},
                    new Country() { ID=194, Name="Vietnam"},
                    new Country() { ID=195, Name="Yemen"},
                    new Country() { ID=196, Name="Zambia"},
                    new Country() { ID=197, Name="Zimbabwe"}

            };

            return countries;
        }
    }

    public class ShipBrand
    {
        //public string ID { get; set; }
        public string Name { get; set; }
    }

    public class CrewDepartment
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }



    public class Country
    { 
        public int ID { get; set; }
        public string Name { get; set; }
    }
}


