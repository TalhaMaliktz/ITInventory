﻿using System;
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
    public class DeviceTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeviceTypes
        public ActionResult Index()
        {
            return View(db.Types.ToList());
        }

        // GET: DeviceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType deviceType = db.Types.Find(id);
            if (deviceType == null)
            {
                return HttpNotFound();
            }
            return View(deviceType);
        }

        // GET: DeviceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeviceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceTypeID,DeviceTypeValue")] DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                db.Types.Add(deviceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deviceType);
        }

        // GET: DeviceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType deviceType = db.Types.Find(id);
            if (deviceType == null)
            {
                return HttpNotFound();
            }
            return View(deviceType);
        }

        // POST: DeviceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceTypeID,DeviceTypeValue")] DeviceType deviceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deviceType);
        }

        // GET: DeviceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceType deviceType = db.Types.Find(id);
            if (deviceType == null)
            {
                return HttpNotFound();
            }
            return View(deviceType);
        }

        // POST: DeviceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceType deviceType = db.Types.Find(id);
            db.Types.Remove(deviceType);
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
