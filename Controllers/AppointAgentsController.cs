using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5.Models;
using MVC5.Common;

namespace MVC5.Controllers
{
    public class AppointAgentsController : BaseController
    {

        // GET: AppointAgents
        public ActionResult Index()
        {
            var appointAgent = db.AppointAgent.Include(a => a.Introducer).Include(a => a.Listing).Include(a => a.User);
            return View(appointAgent.ToList());
        }

        // GET: AppointAgents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointAgent appointAgent = db.AppointAgent.Find(id);
            if (appointAgent == null)
            {
                return HttpNotFound();
            }
            return View(appointAgent);
        }

        // GET: AppointAgents/Create
        public ActionResult Create()
        {
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli");
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo");
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli");
            return View();
        }

        // POST: AppointAgents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TarikhSah,TarikhTamat,StatusActive,nama,ic,contact,address,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] AppointAgent appointAgent)
        {
            if (ModelState.IsValid)
            {
                appointAgent.UserId = findCurrentUserId();
                db.AppointAgent.Add(appointAgent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", appointAgent.IntroducerId);
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo", appointAgent.ListingId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", appointAgent.UserId);
            return RedirectToAction("Index", "Manage");
        }

        // GET: AppointAgents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointAgent appointAgent = db.AppointAgent.Find(id);
            if (appointAgent == null)
            {
                return HttpNotFound();
            }
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", appointAgent.IntroducerId);
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo", appointAgent.ListingId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", appointAgent.UserId);
            return View(appointAgent);
        }

        // POST: AppointAgents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,IntroducerId,ListingId,TarikhSah,TarikhTamat,StatusActive,nama,ic,contact,address,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] AppointAgent appointAgent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointAgent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", appointAgent.IntroducerId);
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo", appointAgent.ListingId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", appointAgent.UserId);
            return View(appointAgent);
        }

        // GET: AppointAgents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointAgent appointAgent = db.AppointAgent.Find(id);
            if (appointAgent == null)
            {
                return HttpNotFound();
            }
            return View(appointAgent);
        }

        // POST: AppointAgents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppointAgent appointAgent = db.AppointAgent.Find(id);
            db.AppointAgent.Remove(appointAgent);
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
