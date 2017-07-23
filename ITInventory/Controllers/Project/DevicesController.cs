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
using System.Linq.Dynamic;


namespace ITInventory.Controllers.Project
{
    public class DevicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ViewResult Index(string id)
        {
            var devicename = db.Device.Include(d => d.DeviceTypes).Include(d => d.Locations);
            if (!String.IsNullOrEmpty(id))
            {
                devicename = devicename.Where(s => s.DeviceName.Contains(id) /*|| */
                //s.DeviceStatus.Equals(id) ||
                //s.EntryDate.Equals(id) ||
                //s.AssignDate.Equals(id) ||
                //s.MACAddress.Contains(id)         
                );
            }
            return View(devicename.ToList());
        }

        public ActionResult FillSpecification(int id)
        {
            var specifications = db.Specifications.Where(t => t.DeviceTypeFKID == id);
            return Json(specifications, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateType([Bind(Include = "DeviceTypeValue")] DeviceType types)
        {
            if (ModelState.IsValid)
            {
                db.Types.Add(types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(types);
        }
        public ActionResult CreateLocation([Bind(Include = "LocationType,LocationTypeValue")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.LocationTypeValue = location.LocationTypeValue + "-" + location.LocationType;
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }
        public ActionResult CreateDeviceSpecification([Bind(Include = "DeviceFKID,SpecificationFKID,SpecificationValue,DeviceTypeFKID")] DeviceSpecifications deviceSpecifications)
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
        public ActionResult CreateSpecification([Bind(Include = "SpecificationValue,DeviceTypeFKID")] Specifications specifications)
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
        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Device.Find(id);
            
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue");
            ViewBag.LocationFKID = new SelectList(db.Locations, "LocationID", "LocationTypeValue");
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName");
            ViewBag.SpecificationFKID = new SelectList(db.Specifications, "SpecificationID", "SpecificationValue");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateDevice([Bind(Include = "DeviceName,DeviceTypeFKID,LocationFKID,EntryDate,AssignDate,DeviceStatus,MACAddress")] Device device)
        {
            
            if (ModelState.IsValid)
            {
                db.Device.Add(device);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue", device.DeviceTypeFKID);
            ViewBag.LocationFKID = new SelectList(db.Locations, "LocationID", "LocationType", device.LocationFKID);
            return View(device);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Device.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue", device.DeviceTypeFKID);
            ViewBag.LocationFKID = new SelectList(db.Locations, "LocationID", "LocationTypeValue", device.LocationFKID);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceID,DeviceName,DeviceTypeFKID,LocationFKID,EntryDate,AssignDate,DeviceStatus,MACAddress")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceTypeFKID = new SelectList(db.Types, "DeviceTypeID", "DeviceTypeValue", device.DeviceTypeFKID);
            ViewBag.LocationFKID = new SelectList(db.Locations, "LocationID", "LocationType", device.LocationFKID);
            return View(device);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Device.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Device device = db.Device.Find(id);
            db.Device.Remove(device);
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
