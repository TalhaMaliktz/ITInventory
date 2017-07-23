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
    public class IPsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IPs
        public ActionResult Index()
        {
            var iP = db.IP.Include(i => i.DeviceName);
            return View(iP.ToList());
        }

        // GET: IPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IP iP = db.IP.Find(id);
            if (iP == null)
            {
                return HttpNotFound();
            }
            return View(iP);
        }

        // GET: IPs/Create
        public ActionResult Create()
        {
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName");
            return View();
        }

        // POST: IPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IPID,DeviceFKID,FamilyIP,ChildIP,Purpose")] IP iP)
        {
            if (ModelState.IsValid)
            {
                db.IP.Add(iP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", iP.DeviceFKID);
            return View(iP);
        }

        // GET: IPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IP iP = db.IP.Find(id);
            if (iP == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", iP.DeviceFKID);
            return View(iP);
        }

        // POST: IPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IPID,DeviceFKID,FamilyIP,ChildIP,Purpose")] IP iP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceFKID = new SelectList(db.Device, "DeviceID", "DeviceName", iP.DeviceFKID);
            return View(iP);
        }

        // GET: IPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IP iP = db.IP.Find(id);
            if (iP == null)
            {
                return HttpNotFound();
            }
            return View(iP);
        }

        // POST: IPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IP iP = db.IP.Find(id);
            db.IP.Remove(iP);
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
