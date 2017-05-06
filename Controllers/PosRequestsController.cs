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
    public class PosRequestsController : BaseController
    {

        // GET: PosRequests
        public ActionResult Index()
        {
            var posRequest = db.PosRequest.Include(p => p.Introducer).Include(p => p.Listing).Include(p => p.User);
            return View(posRequest.ToList());
        }

        // GET: PosRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosRequest posRequest = db.PosRequest.Find(id);
            if (posRequest == null)
            {
                return HttpNotFound();
            }
            return View(posRequest);
        }

        // GET: PosRequests/Create
        public ActionResult Create()
        {
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli");
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo");
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli");
            return View();
        }

        // POST: PosRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TarikhSah,TarikhTamat,StatusActive,nama,ic,contact,address,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] PosRequest posRequest)
        {
            if (ModelState.IsValid)
            {
                posRequest.UserId = findCurrentUserId();
                db.PosRequest.Add(posRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", posRequest.IntroducerId);
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo", posRequest.ListingId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", posRequest.UserId);
            return RedirectToAction("Index", "Manage");
        }

        // GET: PosRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosRequest posRequest = db.PosRequest.Find(id);
            if (posRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", posRequest.IntroducerId);
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo", posRequest.ListingId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", posRequest.UserId);
            return View(posRequest);
        }

        // POST: PosRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,IntroducerId,ListingId,TarikhSah,TarikhTamat,StatusActive,nama,ic,contact,address,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] PosRequest posRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", posRequest.IntroducerId);
            ViewBag.ListingId = new SelectList(db.Transactions, "Id", "UnitNo", posRequest.ListingId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", posRequest.UserId);
            return View(posRequest);
        }

        // GET: PosRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosRequest posRequest = db.PosRequest.Find(id);
            if (posRequest == null)
            {
                return HttpNotFound();
            }
            return View(posRequest);
        }

        // POST: PosRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PosRequest posRequest = db.PosRequest.Find(id);
            db.PosRequest.Remove(posRequest);
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
