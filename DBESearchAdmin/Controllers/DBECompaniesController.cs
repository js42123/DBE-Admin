using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DBESearchAdmin.Helpers;
using DBESearchAdmin.Models;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using PagedList;
using DBESearchAdmin.ViewModel;



namespace DBESearchAdmin.Controllers
{
    public class DBECompaniesController : Controller
    {
        private DBEDirectoryEntities1 db = new DBEDirectoryEntities1();

        public ActionResult Index()
        {
            //int count = db.DBECompanies.OrderBy()
            //var pageSize = new Dictionary<string, string>() { { "5", "5" }, { "10", "10" }, { "25", "25" }, { "50", "50" }, { "100", "100" } };
            //ViewBag.pageSizes = pageSize.Select(m => new SelectListItem { Text = m.Value, Value = m.Key }).ToList(); //Available pageSizes to choose from
            return View(db.DBECompanies.OrderBy(x => x.CompanyName));

        }


        private IQueryable<DBECompany> GetSearchResults(Models.DBECompany data, DBEDirectoryEntities1 db)
        {
            IQueryable<Models.DBECompany> query = db.DBECompanies;

            return query;   
        }

        public ActionResult ExportDatatoExcel()
        {
            try
            {
                IQueryable<Models.DBECompany> query;
                List<Models.DBECompany> CompanyList;
                using (var db = new Models.DBEDirectoryEntities1())
                {
                    Models.DBECompany data = (Models.DBECompany)Session["query"];
                    query = GetSearchResults(data, db);

                    query = query
                        .Select(c => c);
                    CompanyList = query.ToList();
                }
                GridView gv = new GridView();
                gv.DataSource = CompanyList;
                gv.DataBind();

                var fileName = "Company_List-" + DateTime.Now.ToString("yyyyMMdd_hhss") + ".xlsx";
                var file = new FileInfo(fileName);
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("List");
                var totalCols = gv.Rows[0].Cells.Count;
                var totalRows = gv.Rows.Count;
                var headerRow = gv.HeaderRow;
                for (var i =1; i <= totalCols; i++)
                {
                    workSheet.Cells[1, i].Value = headerRow.Cells[i - 1].Text;
                }
                for (var j = 1; j <= totalRows; j++)
                {
                    for (var i = 1; i <= totalCols; i++)
                    {
                        var a = CompanyList.ElementAt(j - 1);
                        workSheet.Cells[j + 1, i].Value = a.GetType().GetProperty(headerRow.Cells[i - 1].Text).GetValue(a);
                    }
                }
                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=" + fileName);
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                return RedirectToAction("Search");
            }
            catch (Exception)
            {
                ViewBag.Message = "Your data is too Large to Export to Excel. Please filter your Search to avoid the error. Thank you!";
                return View();
            }
        }

        //public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.CompanyId = String.IsNullOrEmpty(sortOrder) ? "Company_Id" : "";
        //    ViewBag.CompanyName = String.IsNullOrEmpty(sortOrder) ? "Company_Name" : "";
        //    ViewBag.DBAName = String.IsNullOrEmpty(sortOrder) ? "DBA_Name" : "";
        //    ViewBag.OwnersFirstName = String.IsNullOrEmpty(sortOrder) ? "Owners_First_Name" : "";
        //    ViewBag.OwnersLastName = String.IsNullOrEmpty(sortOrder) ? "Owners_Last_Name" : "";
        //    ViewBag.CompanyAddress = String.IsNullOrEmpty(sortOrder) ? "Company_Address" : "";
        //    ViewBag.City = String.IsNullOrEmpty(sortOrder) ? "City" : "";
        //    ViewBag.State = String.IsNullOrEmpty(sortOrder) ? "State" : "";
        //    ViewBag.Zip = String.IsNullOrEmpty(sortOrder) ? "Zip" : "";
        //    ViewBag.Phone = String.IsNullOrEmpty(sortOrder) ? "Phone" : "";
        //    ViewBag.Fax = String.IsNullOrEmpty(sortOrder) ? "Fax" : "";
        //    ViewBag.Email = String.IsNullOrEmpty(sortOrder) ? "Email" : "";
        //    ViewBag.District = String.IsNullOrEmpty(sortOrder) ? "District" : "";
        //    ViewBag.DBE = String.IsNullOrEmpty(sortOrder) ? "DBE" : "";
        //    ViewBag.ACDBE = String.IsNullOrEmpty(sortOrder) ? "ACDBE" : "";
        //    ViewBag.MBE = String.IsNullOrEmpty(sortOrder) ? "MBE" : "";
        //    ViewBag.WBE = String.IsNullOrEmpty(sortOrder) ? "WBE" : "";
        //    ViewBag.DWBE = String.IsNullOrEmpty(sortOrder) ? "DWBE" : "";
        //    ViewBag.ACWBE = String.IsNullOrEmpty(sortOrder) ? "ACWBE" : "";
        //    ViewBag.MonthofAnnualAffidavit = String.IsNullOrEmpty(sortOrder) ? "Annual_Affidavit_Month" : "";
        //    ViewBag.RenewalDate = String.IsNullOrEmpty(sortOrder) ? "Renewal_Date" : "";
        //    ViewBag.Certified = String.IsNullOrEmpty(sortOrder) ? "Certfied" : "";
        //    ViewBag.CertificationDate = String.IsNullOrEmpty(sortOrder) ? "CertificationDate" : "";
        //    ViewBag.OnSiteReviewDate = String.IsNullOrEmpty(sortOrder) ? "OnSite_Review_Date" : "";
        //    ViewBag.FirmType = String.IsNullOrEmpty(sortOrder) ? "Firm_Type" : "";
        //    ViewBag.DateRequestedLastOnSite = String.IsNullOrEmpty(sortOrder) ? "Last_OnSite_Req_Date" : "";
        //    ViewBag.DeskAuditReview = String.IsNullOrEmpty(sortOrder) ? "Desk_Audit_Review" : "";
        //    ViewBag.SBP = String.IsNullOrEmpty(sortOrder) ? "SBP" : "";
        //    ViewBag.DecertificationDate = String.IsNullOrEmpty(sortOrder) ? "Date_Decertified" : "";
        //    ViewBag.DecertReason = String.IsNullOrEmpty(sortOrder) ? "Decert_Reason" : "";
        //    ViewBag.Suspended = String.IsNullOrEmpty(sortOrder) ? "Suspended" : "";
        //    ViewBag.SuspensionDate = String.IsNullOrEmpty(sortOrder) ? "Suspension_Date" : "";
        //    ViewBag.Race = String.IsNullOrEmpty(sortOrder) ? "Race" : "";
        //    ViewBag.Gender = String.IsNullOrEmpty(sortOrder) ? "Gender" : "";


        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var companies = from s in db.DBECompanies
        //                    select s;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        companies = companies.Where(s => s.CompanyName.Contains(searchString) || s.DBAName.Contains(searchString));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "Company_Id":
        //            companies = companies.OrderBy(s => s.CompanyId);
        //            break;
        //        case "Company_Name":
        //            companies = companies.OrderByDescending(s => s.CompanyName);
        //            break;
        //        case "DBA_Name":
        //            companies = companies.OrderBy(s => s.DBAName);
        //            break;
        //        case "Owners_First_Name":
        //            companies = companies.OrderBy(s => s.OwnersFirstName);
        //            break;
        //        case "Owners_Last_Name":
        //            companies = companies.OrderBy(s => s.OwnersLastName);
        //            break;
        //        case "Company_Address":
        //            companies = companies.OrderBy(s => s.CompanyAddress);
        //            break;
        //        case "City":
        //            companies = companies.OrderBy(s => s.City);
        //            break;
        //        case "State":
        //            companies = companies.OrderBy(s => s.State);
        //            break;
        //        case "Zip":
        //            companies = companies.OrderBy(s => s.Zip);
        //            break;
        //        case "Phone":
        //            companies = companies.OrderBy(s => s.Phone);
        //            break;
        //        case "Fax":
        //            companies = companies.OrderBy(s => s.Fax);
        //            break;
        //        case "Email":
        //            companies = companies.OrderBy(s => s.Email);
        //            break;
        //        case "District":
        //            companies = companies.OrderBy(s => s.District);
        //            break;
        //        case "DBE":
        //            companies = companies.OrderBy(s => s.DBE);
        //            break;
        //        case "ACDBE":
        //            companies = companies.OrderBy(s => s.ACDBE);
        //            break;
        //        case "MBE":
        //            companies = companies.OrderBy(s => s.MBE);
        //            break;
        //        case "WBE":
        //            companies = companies.OrderBy(s => s.WBE);
        //            break;
        //        case "DWBE":
        //            companies = companies.OrderBy(s => s.DWBE);
        //            break;
        //        case "ACWBE":
        //            companies = companies.OrderBy(s => s.ACWBE);
        //            break;
        //        case "Annual_Affidavit_Month":
        //            companies = companies.OrderBy(s => s.MonthofAnnualAffidavit);
        //            break;
        //        case "Renewal_Date":
        //            companies = companies.OrderBy(s => s.RenewalDate);
        //            break;
        //        case "Certified":
        //            companies = companies.OrderBy(s => s.Certified);
        //            break;
        //        case "Certification_Date":
        //            companies = companies.OrderBy(s => s.CertificationDate);
        //            break;
        //        case "OnSite_Review_Date":
        //            companies = companies.OrderBy(s => s.OnSiteReviewDate);
        //            break;
        //        case "Firm_Type":
        //            companies = companies.OrderBy(s => s.TypeofFirm);
        //            break;
        //        case "Last_OnSite_Req_Date":
        //            companies = companies.OrderBy(s => s.DateRequestedLastOnSite);
        //            break;
        //        case "Desk_Audit_Review":
        //            companies = companies.OrderBy(s => s.DeskAuditReview);
        //            break;
        //        case "SBP":
        //            companies = companies.OrderBy(s => s.SBP);
        //            break;
        //        case "Date_Decertified":
        //            companies = companies.OrderBy(s => s.DecertificationDate);
        //            break;
        //        case "Decert_Reason":
        //            companies = companies.OrderBy(s => s.DecertReason);
        //            break;
        //        case "Suspended":
        //            companies = companies.OrderBy(s => s.Suspended);
        //            break;
        //        case "Suspension_Date":
        //            companies = companies.OrderBy(s => s.Suspension_Date);
        //            break;
        //        case "Race":
        //            companies = companies.OrderBy(s => s.Race);
        //            break;
        //        case "Gender":
        //            companies = companies.OrderBy(s => s.Gender);
        //            break;
        //        default:
        //            companies = companies.OrderBy(s => s.CompanyName);
        //            break;
        //    }
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(companies.ToPagedList(pageNumber, pageSize));
        //}
        //// GET: DBECompanies
        //public ActionResult Index()
        //{
        //    return View(db.DBECompany.ToList());
        //}

        // GET: DBECompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DBECompany dBECompany = db.DBECompanies.Find(id);
            if (dBECompany == null)
            {
                return HttpNotFound();
            }
            return View(dBECompany);
        }

        // GET: DBECompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DBECompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create(DBECompany dBECompany)
        {
            if (ModelState.IsValid)
            {
                db.DBECompanies.Add(dBECompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dBECompany);
        }

        //GET: CompanyItemCodes/Add
        public ActionResult Add()

        {
            return View();
        }



        // POST: CompanyItemCodes/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Add(CompanyItemCode companyItemCodes)
        {
            if (ModelState.IsValid)
            {
                db.CompanyItemCodes.Add(companyItemCodes);
                db.SaveChanges();
                return RedirectToAction("Details","DBECompanies", new { id = companyItemCodes.CompanyID });
            }

            return View("Index");
        }

        // GET: DBECompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DBECompany dBECompany = db.DBECompanies.Find(id);
            if (dBECompany == null)
            {
                return HttpNotFound();
            }
            return View(dBECompany);
        }

        // POST: DBECompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit( DBECompany dBECompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dBECompany).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "DBECompanies", new { id = dBECompany.CompanyId });
            }
            return View(dBECompany);
        }

        // GET: DBECompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DBECompany dBECompany = db.DBECompanies.Find(id);
            if (dBECompany == null)
            {
                return HttpNotFound();
            }
            return View(dBECompany);
        }

        // POST: DBECompanies/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            DBECompany dBECompany = db.DBECompanies.Find(id);
            db.DBECompanies.Remove(dBECompany);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ItemSearchResults(int? searchid)
        {

            var depcodes = from d in db.CompanyItemCodes
                           select d;

            if (searchid != null)
            {
                depcodes = depcodes.Where(d => d.CompanyID == (searchid));

            }
            return View(depcodes.ToList());
        }

        // GET: CompanyNaicsCode/Delete/5
        public ActionResult Remove(int? companyid, int? searchid)

        {
            if (searchid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyItemCode companyItemCode = db.CompanyItemCodes.Find(companyid, searchid);
            if (companyItemCode == null)
            {
                return HttpNotFound();
            }
            return View(companyItemCode);
        }

        // POST: CompanyItemCodes/Delete/5
        [HttpPost, ActionName("Remove")]
       
        public ActionResult RemoveConfirmed(int companyid, int searchid)
        {
            CompanyItemCode companyItemCodes = db.CompanyItemCodes.Find(companyid, searchid);
            db.CompanyItemCodes.Remove(companyItemCodes);
            db.SaveChanges();
            return RedirectToAction("Details","DBECompanies", new { id = companyItemCodes.CompanyID });
        }


        public ActionResult NAICSSearchResults(int? searchnaicsid)
        {
            var naicscodes = from n in db.CompanyNAICSCodes
                             select n;
            if (searchnaicsid != null)
            {
                naicscodes = naicscodes.Where(n => n.Companyid == (searchnaicsid));
            }
            return View(naicscodes.ToList());
        }


        //GET: CompanyNaicsCode/Add
        public ActionResult AddNaics()
        {
            {
                var list = db.CompanyNAICSCodes.Distinct().ToList();
                ViewBag.NaicsCodes = from c in list
                                     join d in db.NAICSCodes on c.NAICSCode equals d.NAICSCode1

                                     orderby c.NAICSCode
                                     select new SelectListItem
                                     {
                                         Value = c.NAICSCode.ToString(),
                                         Text = d.Description
                                     };
            };
            return View();
        }



        // POST: CompanyNaicsCode/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult AddNaics(CompanyNAICSCode companyNAICSCode)
        {
            if (ModelState.IsValid)
            {
                db.CompanyNAICSCodes.Add(companyNAICSCode);
                db.SaveChanges();
                return RedirectToAction("Details", "DBECompanies", new { id = companyNAICSCode.Companyid });
            }

            return View(companyNAICSCode);
        }


        // GET: CompanyNaicsCode/Delete/5
        public ActionResult RemoveNaics(string searchid, int? companyid)
        {
            if (searchid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyNAICSCode companyNAICSCode = db.CompanyNAICSCodes.Find(searchid, companyid);
            if (companyNAICSCode == null)
            {
                return HttpNotFound();
            }
            return View(companyNAICSCode); 
        }

        // POST: CompanyNAICSCode/Delete/5
        [HttpPost, ActionName("RemoveNaics")]
      
        public ActionResult RemoveNaicsConfirmed(string searchid, int companyid)
        {

            CompanyNAICSCode companyNAICSCode = db.CompanyNAICSCodes.Find(searchid, companyid);
            db.CompanyNAICSCodes.Remove(companyNAICSCode);
            db.SaveChanges();
            return RedirectToAction("Details", "DBECompanies", new { id = companyNAICSCode.Companyid });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
