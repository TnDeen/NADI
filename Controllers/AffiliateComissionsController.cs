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
    public class AffiliateComissionsController : BaseController
    {

        // GET: AffiliateComissions
        public ActionResult Index()
        {
            var affiliateComission = db.AffiliateComission.Include(a => a.Introducer).Include(a => a.User);
            return View(affiliateComission.ToList());
        }

        // GET: AffiliateComissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AffiliateComission affiliateComission = db.AffiliateComission.Find(id);
            if (affiliateComission == null)
            {
                return HttpNotFound();
            }
            return View(affiliateComission);
        }

        // GET: AffiliateComissions/Create
        public ActionResult Create()
        {
            ViewBag.IntroducerId = new SelectList(idb.Users, "Id", "NomborAhli");
            ViewBag.UserId = new SelectList(idb.Users, "Id", "NomborAhli");
            return View();
        }

        // POST: AffiliateComissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,IntroducerId,statusActive,ulasan,level,point,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] AffiliateComission affiliateComission)
        {
            if (ModelState.IsValid)
            {
                db.AffiliateComission.Add(affiliateComission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IntroducerId = new SelectList(idb.Users, "Id", "NomborAhli", affiliateComission.IntroducerId);
            ViewBag.UserId = new SelectList(idb.Users, "Id", "NomborAhli", affiliateComission.UserId);
            return View(affiliateComission);
        }

        // GET: AffiliateComissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AffiliateComission affiliateComission = db.AffiliateComission.Find(id);
            if (affiliateComission == null)
            {
                return HttpNotFound();
            }
            ViewBag.IntroducerId = new SelectList(idb.Users, "Id", "NomborAhli", affiliateComission.IntroducerId);
            ViewBag.UserId = new SelectList(idb.Users, "Id", "NomborAhli", affiliateComission.UserId);
            return View(affiliateComission);
        }

        // POST: AffiliateComissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,IntroducerId,statusActive,ulasan,level,point,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] AffiliateComission affiliateComission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(affiliateComission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IntroducerId = new SelectList(idb.Users, "Id", "NomborAhli", affiliateComission.IntroducerId);
            ViewBag.UserId = new SelectList(idb.Users, "Id", "NomborAhli", affiliateComission.UserId);
            return View(affiliateComission);
        }

        // GET: AffiliateComissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AffiliateComission affiliateComission = db.AffiliateComission.Find(id);
            if (affiliateComission == null)
            {
                return HttpNotFound();
            }
            return View(affiliateComission);
        }

        // POST: AffiliateComissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AffiliateComission affiliateComission = db.AffiliateComission.Find(id);
            db.AffiliateComission.Remove(affiliateComission);
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
