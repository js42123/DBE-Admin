using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBESearchAdmin.Models;
using DBESearchAdmin.Helpers;
using PagedList;

namespace DBE.Views
{
    public class ItemCodesController : Controller
    {
        private DBEDirectoryEntities1 db = new DBEDirectoryEntities1();

        public ActionResult Index()
        {
            return View(db.ItemCodes.OrderBy(i => i.ItemCode1));
        }

        //
        // GET: /ItemCodes/

        //public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Item_Code" : "";


        //    if (search != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        search = currentFilter;
        //    }



        //    ViewBag.CurrentFilter = search;



        //    var items = from i in db.ItemCodes

        //                select i;

        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        items = items.Where(m => m.Description.Contains(search));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "Item_Code":
        //            items = items.OrderByDescending(i => i.ItemCode1);
        //            break;

        //        default:
        //            items = items.OrderBy(i => i.ItemCode1);
        //            break;
        //    }

        //    int pageSize = 25;
        //    int pageNumber = (page ?? 1);
        //    return View(items.ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create( ItemCode itemCodes)
        {
            if (ModelState.IsValid)
            {

                db.ItemCodes.Add(itemCodes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            else
            {
                throw new HttpException(500, "Internal Server Error");
            }
        }

        //GET: ItemCodes/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCode itemCodes = db.ItemCodes.Find(id);
            if (itemCodes == null)
            {
                return HttpNotFound();
            }
            return View(itemCodes);
        }

        // POST: ItemCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Edit(ItemCode itemCodes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemCodes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemCodes);
        }

        // GET: ItemCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCode itemCodes = db.ItemCodes.Find(id);
            if (itemCodes == null)
            {
                return HttpNotFound();
            }
            return View(itemCodes);
        }

        // GET: ItemCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCode itemCodes = db.ItemCodes.Find(id);
            if (itemCodes == null)
            {
                return HttpNotFound();
            }
            return View(itemCodes);
        }

        // POST: ItemCodes/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(int id)
        {
            ItemCode itemCodes = db.ItemCodes.Find(id);
            db.ItemCodes.Remove(itemCodes);
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


