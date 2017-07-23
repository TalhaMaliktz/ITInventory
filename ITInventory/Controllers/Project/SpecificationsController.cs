using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITInventory.Models;
using ITInventory.Models.Entities;

namespace ITInventory.Controllers.Project
{
    public class SpecificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Specifications
        public ActionResult Index()
        {
            var specifications = db.Specifications.Include(s => s.DeviceTypes);
            return View(specifications.ToList());
        }

        // GET: Specifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specifications specifications = db.Specifications.Find(id);
            if (specifications == null)
            {
                return HttpNotFound();
            }
            return View(specifications);
        }

        // GET: Specifications/Create
        public ActionResult Create()
        {
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue");
            return View();
        }

        // POST: Specifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpecificationID,SpecificationValue,DeviceTypeFKID")] Specifications specifications)
        {
            if (ModelState.IsValid)
            {
                db.Specifications.Add(specifications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue", specifications.DeviceTypeFKID);
            return View(specifications);
        }

        // GET: Specifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specifications specifications = db.Specifications.Find(id);
            if (specifications == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue", specifications.DeviceTypeFKID);
            return View(specifications);
        }

        // POST: Specifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpecificationID,SpecificationValue,DeviceTypeFKID")] Specifications specifications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specifications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue", specifications.DeviceTypeFKID);
            return View(specifications);
        }

        // GET: Specifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specifications specifications = db.Specifications.Find(id);
            if (specifications == null)
            {
                return HttpNotFound();
            }
            return View(specifications);
        }

        // POST: Specifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specifications specifications = db.Specifications.Find(id);
            db.Specifications.Remove(specifications);
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
