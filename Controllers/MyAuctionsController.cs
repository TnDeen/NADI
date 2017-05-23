using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5.Models;

namespace MVC5.Controllers
{
    [Authorize]
    public class MyAuctionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyAuctions
        public ActionResult Index()
        {
            return View(db.MyAuction.ToList());
        }

        // GET: MyAuctions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAuction myAuction = db.MyAuction.Find(id);
            if (myAuction == null)
            {
                return HttpNotFound();
            }
            return View(myAuction);
        }

        // GET: MyAuctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyAuctions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Unit,Address,AuctionDate,DueDate,SoldPrice,ReservePrice,SelfBid,AppointAgent,Status,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] MyAuction myAuction)
        {
            if (ModelState.IsValid)
            {
                db.MyAuction.Add(myAuction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myAuction);
        }

        // GET: MyAuctions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAuction myAuction = db.MyAuction.Find(id);
            if (myAuction == null)
            {
                return HttpNotFound();
            }
            return View(myAuction);
        }

        // POST: MyAuctions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Unit,Address,AuctionDate,DueDate,SoldPrice,ReservePrice,SelfBid,AppointAgent,Status,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] MyAuction myAuction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myAuction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myAuction);
        }

        // GET: MyAuctions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyAuction myAuction = db.MyAuction.Find(id);
            if (myAuction == null)
            {
                return HttpNotFound();
            }
            return View(myAuction);
        }

        // POST: MyAuctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyAuction myAuction = db.MyAuction.Find(id);
            db.MyAuction.Remove(myAuction);
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
