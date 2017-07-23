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
    public class DeviceSpecificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeviceSpecifications
        public ActionResult Index()
        {
            var deviceSpecifications = db.DeviceSpecifications.Include(d => d.Device).Include(d => d.SpecificationTitle);
            return View(deviceSpecifications.ToList());
        }

        // GET: DeviceSpecifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceSpecifications deviceSpecifications = db.DeviceSpecifications.Find(id);
            if (deviceSpecifications == null)
            {
                return HttpNotFound();
            }
            return View(deviceSpecifications);
        }
        public ActionResult FillSpecification(int type)
        {
            var specification = db.Specifications.Where(t => t.DeviceTypeFKID == type);
            return Json(specification, JsonRequestBehavior.AllowGet);
        }
        // GET: DeviceSpecifications/Create
        public ActionResult Create()
        {
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue");
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName");
            ViewBag.SpecificationFKID = new SelectList(db.Specifications, "SpecificationID", "SpecificationValue");
            return View();
        }

        // POST: DeviceSpecifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceSpecificationID,DeviceFKID,SpecificationFKID,SpecificationValue")] DeviceSpecifications deviceSpecifications)
        {
            if (ModelState.IsValid)
            {
                db.DeviceSpecifications.Add(deviceSpecifications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", deviceSpecifications.DeviceFKID);
            ViewBag.SpecificationFKID = new SelectList(db.Specifications, "SpecificationID", "SpecificationValue", deviceSpecifications.SpecificationFKID);
            return View(deviceSpecifications);
        }

        // GET: DeviceSpecifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceSpecifications deviceSpecifications = db.DeviceSpecifications.Find(id);
            if (deviceSpecifications == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", deviceSpecifications.DeviceFKID);
            ViewBag.SpecificationFKID = new SelectList(db.Specifications, "SpecificationID", "SpecificationValue", deviceSpecifications.SpecificationFKID);
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue", deviceSpecifications.DeviceTypeFKID);
            return View(deviceSpecifications);
        }

        // POST: DeviceSpecifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceSpecificationID,DeviceFKID,SpecificationFKID,SpecificationValue")] DeviceSpecifications deviceSpecifications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceSpecifications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", deviceSpecifications.DeviceFKID);
            ViewBag.SpecificationFKID = new SelectList(db.Specifications, "SpecificationID", "SpecificationValue", deviceSpecifications.SpecificationFKID);
            return View(deviceSpecifications);
        }

        // GET: DeviceSpecifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceSpecifications deviceSpecifications = db.DeviceSpecifications.Find(id);
            if (deviceSpecifications == null)
            {
                return HttpNotFound();
            }
            return View(deviceSpecifications);
        }

        // POST: DeviceSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceSpecifications deviceSpecifications = db.DeviceSpecifications.Find(id);
            db.DeviceSpecifications.Remove(deviceSpecifications);
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
