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
    [Authorize]
    public class MembershipRequestsController : BaseController
    {

        // GET: MembershipRequests
        public ActionResult Index()
        {
            var membershipRequest = db.MembershipRequest.Include(m => m.Introducer).Include(m => m.PackageType).Include(m => m.User);
            return View(membershipRequest.ToList());
        }

        // GET: MembershipRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipRequest membershipRequest = db.MembershipRequest.Find(id);
            if (membershipRequest == null)
            {
                return HttpNotFound();
            }
            return View(membershipRequest);
        }

        // GET: MembershipRequests/Create
        public ActionResult Create()
        {
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli");
            ViewBag.PackageTypeId = new SelectList(db.Sak.Where(a => a.Sk.Kod.Equals("MMBRSHP_TYPE")), "Id", "Nama");
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli");
            return View();
        }

        // POST: MembershipRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,IntroducerId,PackageTypeId,TarikhSah,TarikhTamat,nama,ic,contact,address,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] MembershipRequest membershipRequest)
        {
            if (ModelState.IsValid)
            {
                membershipRequest.UserId = findCurrentUserId();
                db.MembershipRequest.Add(membershipRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", membershipRequest.IntroducerId);
            ViewBag.PackageTypeId = new SelectList(db.Sak, "Id", "Nama", membershipRequest.PackageTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", membershipRequest.UserId);
            return RedirectToAction("Index","Manage");
        }

        // GET: MembershipRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipRequest membershipRequest = db.MembershipRequest.Find(id);
            if (membershipRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", membershipRequest.IntroducerId);
            ViewBag.PackageTypeId = new SelectList(db.Sak, "Id", "Nama", membershipRequest.PackageTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", membershipRequest.UserId);
            return View(membershipRequest);
        }

        // POST: MembershipRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,IntroducerId,PackageTypeId,TarikhSah,TarikhTamat,StatusActive,nama,ic,contact,address,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] MembershipRequest membershipRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membershipRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IntroducerId = new SelectList(db.Users, "Id", "NomborAhli", membershipRequest.IntroducerId);
            ViewBag.PackageTypeId = new SelectList(db.Sak, "Id", "Nama", membershipRequest.PackageTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", membershipRequest.UserId);
            return View(membershipRequest);
        }

        // GET: MembershipRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipRequest membershipRequest = db.MembershipRequest.Find(id);
            if (membershipRequest == null)
            {
                return HttpNotFound();
            }
            return View(membershipRequest);
        }

        // POST: MembershipRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipRequest membershipRequest = db.MembershipRequest.Find(id);
            db.MembershipRequest.Remove(membershipRequest);
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
