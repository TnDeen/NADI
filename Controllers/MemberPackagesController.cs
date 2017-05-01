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
    public class MemberPackagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MemberPackages
        public ActionResult Index()
        {
            return View(db.MemberPackage.ToList());
        }

        // GET: MemberPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPackage memberPackage = db.MemberPackage.Find(id);
            if (memberPackage == null)
            {
                return HttpNotFound();
            }
            return View(memberPackage);
        }

        // GET: MemberPackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ContentFree,FreeMember,ContentVip,VipMember,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] MemberPackage memberPackage)
        {
            if (ModelState.IsValid)
            {
                memberPackage.DateCreated = DateTime.Now;
                memberPackage.DateUpdated = DateTime.Now;
                db.MemberPackage.Add(memberPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberPackage);
        }

        // GET: MemberPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPackage memberPackage = db.MemberPackage.Find(id);
            if (memberPackage == null)
            {
                return HttpNotFound();
            }
            return View(memberPackage);
        }

        // POST: MemberPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ContentFree,FreeMember,ContentVip,VipMember,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] MemberPackage memberPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberPackage);
        }

        // GET: MemberPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberPackage memberPackage = db.MemberPackage.Find(id);
            if (memberPackage == null)
            {
                return HttpNotFound();
            }
            return View(memberPackage);
        }

        // POST: MemberPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberPackage memberPackage = db.MemberPackage.Find(id);
            db.MemberPackage.Remove(memberPackage);
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

        public ActionResult BuyPackage()
        {
            return View(db.MemberPackage.ToList());
        }
    }
}
