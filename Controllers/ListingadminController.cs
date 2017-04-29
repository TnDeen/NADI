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
    public class ListingadminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Listingadmin
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(l => l.AuctionBank).Include(l => l.AuctionType).Include(l => l.Bandar).Include(l => l.Negeri).Include(l => l.PropertyType);
            return View(transactions.ToList());
        }

        // GET: Listingadmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Transactions.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // GET: Listingadmin/Create
        public ActionResult Create()
        {
            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama");
            return View();
        }

        // POST: Listingadmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PropertyTypeId,UnitNo,Address1,Address2,Address3,Address4,Poskod,BandarId,NegeriId,Size,Price,AuctionDate,AuctionTime,AuctionTypeId,AuctionBankId,AuctionVenue,AuctionNeer,Lawyer,Assignor,imageUrl,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(listing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionBankId);
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionTypeId);
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama", listing.BandarId);
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama", listing.NegeriId);
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama", listing.PropertyTypeId);
            return View(listing);
        }

        // GET: Listingadmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Transactions.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionBankId);
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionTypeId);
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama", listing.BandarId);
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama", listing.NegeriId);
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama", listing.PropertyTypeId);
            return View(listing);
        }

        // POST: Listingadmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PropertyTypeId,UnitNo,Address1,Address2,Address3,Address4,Poskod,BandarId,NegeriId,Size,Price,AuctionDate,AuctionTime,AuctionTypeId,AuctionBankId,AuctionVenue,AuctionNeer,Lawyer,Assignor,imageUrl,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionBankId);
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionTypeId);
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama", listing.BandarId);
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama", listing.NegeriId);
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama", listing.PropertyTypeId);
            return View(listing);
        }

        // GET: Listingadmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Transactions.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // POST: Listingadmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Listing listing = db.Transactions.Find(id);
            db.Transactions.Remove(listing);
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
