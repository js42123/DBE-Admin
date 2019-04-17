using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBESearchAdmin.Models;
using PagedList;

namespace DBE.Views
{
    public class NAICSCodesController : Controller
    {
        private DBEDirectoryEntities1 db = new DBEDirectoryEntities1();
        public ActionResult Index()
        {
            return View(db.NAICSCodes.OrderBy(i => i.NAICSCode1));
        }
        //
        // GET: /NAICSCodes/

        //public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "NAICS_Code" : "";

        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        page = 1;

        //    }
        //    else
        //    {
        //        search = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = search;

        //    var naics = from i in db.NAICSCodes
        //                select i;

        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        naics = naics.Where(i => i.NAICSCode1.Contains(search));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "NAICS_Code":
        //            naics = naics.OrderByDescending(i => i.Description);
        //            break;
        //        default:
        //            naics = naics.OrderBy(i => i.NAICSCode1);
        //            break;
        //    }
        //    int pageSize = 25;
        //    int pageNumber = (page ?? 1);
        //    return View(naics.ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Create( NAICSCode nAICSCodes)
        {
            if (ModelState.IsValid)
            {
                db.NAICSCodes.Add(nAICSCodes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nAICSCodes);
        }

        //GET: NAICSCodes/Edit
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NAICSCode nAICSCodes = db.NAICSCodes.Find(id);
            if (nAICSCodes == null)
            {
                return HttpNotFound();
            }
            return View(nAICSCodes);
        }

        // POST: NAICSCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit( NAICSCode nAICSCodes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nAICSCodes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nAICSCodes);
        }

        // GET: NAICSCodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NAICSCode nAICSCodes = db.NAICSCodes.Find(id);
            if (nAICSCodes == null)
            {
                return HttpNotFound();
            }
            return View(nAICSCodes);
        }

        // GET: NAICSCodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NAICSCode nAICSCodes = db.NAICSCodes.Find(id);
            if (nAICSCodes == null)
            {
                return HttpNotFound();
            }
            return View(nAICSCodes);
        }

        // POST: NAICSCodes/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public ActionResult DeleteConfirmed(string id)
        {
            NAICSCode nAICSCodes = db.NAICSCodes.Find(id);
            db.NAICSCodes.Remove(nAICSCodes);
            db.SaveChanges();
            return RedirectToAction("Index");
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


