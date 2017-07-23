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
    public class DeviceToDevicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeviceToDevices
        public ActionResult Index()
        {
            var deviceToDevices = db.DeviceToDevices.Include(d => d.DeviceConnected).Include(d => d.Devices);
            return View(deviceToDevices.ToList());
        }

        // GET: DeviceToDevices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceToDevice deviceToDevice = db.DeviceToDevices.Find(id);
            if (deviceToDevice == null)
            {
                return HttpNotFound();
            }
            return View(deviceToDevice);
        }

        // GET: DeviceToDevices/Create
        public ActionResult Create()
        {
            ViewBag.DeviceFKIDConnected = new SelectList(db.Device, "DeviceID", "DeviceName");
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName");
            return View();
        }

        // POST: DeviceToDevices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceToDeviceID,DeviceFKID,DeviceFKIDConnected")] DeviceToDevice deviceToDevice)
        {
            if (ModelState.IsValid)
            {
                db.DeviceToDevices.Add(deviceToDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceFKIDConnected = new SelectList(db.Device, "DeviceID", "DeviceName", deviceToDevice.DeviceFKIDConnected);
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", deviceToDevice.DeviceFKID);
            return View(deviceToDevice);
        }

        // GET: DeviceToDevices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceToDevice deviceToDevice = db.DeviceToDevices.Find(id);
            if (deviceToDevice == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceFKIDConnected = new SelectList(db.Device, "DeviceID", "DeviceName", deviceToDevice.DeviceFKIDConnected);
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", deviceToDevice.DeviceFKID);
            return View(deviceToDevice);
        }

        // POST: DeviceToDevices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceToDeviceID,DeviceFKID,DeviceFKIDConnected")] DeviceToDevice deviceToDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceToDevice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceFKIDConnected = new SelectList(db.Device, "DeviceID", "DeviceName", deviceToDevice.DeviceFKIDConnected);
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", deviceToDevice.DeviceFKID);
            return View(deviceToDevice);
        }

        // GET: DeviceToDevices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceToDevice deviceToDevice = db.DeviceToDevices.Find(id);
            if (deviceToDevice == null)
            {
                return HttpNotFound();
            }
            return View(deviceToDevice);
        }

        // POST: DeviceToDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceToDevice deviceToDevice = db.DeviceToDevices.Find(id);
            db.DeviceToDevices.Remove(deviceToDevice);
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
