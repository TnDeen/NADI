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
    public class SakController : BaseController
    {

        // GET: Sak
        public ActionResult Index(int? skId)
        {
            var sak = db.Sak.Include(s => s.Parent).Include(s => s.Sk);
            if (skId != null)
            {
                sak = sak.Where(a => a.SkId == skId);
            }
            return View(sak.ToList());
        }

        // GET: Sak/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAK sAK = db.Sak.Find(id);
            if (sAK == null)
            {
                return HttpNotFound();
            }
            return View(sAK);
        }

        // GET: Sak/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.SkId = new SelectList(db.Sk, "Id", "Nama");
            return View();
        }

        // POST: Sak/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SkId,Nama,Kod,Perihal,StatusActive,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] SAK sAK)
        {
            if (validateSakKod(sAK.Kod))
            {

                ModelState.AddModelError("", "Kod Telah Wujud! Sila Gunakan Kod Lain.");
                return View(sAK);
            }
            if (ModelState.IsValid)
            {
                db.Sak.Add(sAK);
                db.SaveChanges();
                return RedirectToAction("Index","Sk");
            }

            ViewBag.ParentId = new SelectList(db.Sak, "Id", "Nama", sAK.ParentId);
            ViewBag.SkId = new SelectList(db.Sk, "Id", "Nama", sAK.SkId);
            return View(sAK);
        }

        // GET: Sak/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAK sAK = db.Sak.Find(id);
            if (sAK == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Sak, "Id", "Nama", sAK.ParentId);
            ViewBag.SkId = new SelectList(db.Sk, "Id", "Nama", sAK.SkId);
            return View(sAK);
        }

        // POST: Sak/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParentId,SkId,Nama,Kod,Perihal,StatusActive,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] SAK sAK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sAK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Sak, "Id", "Nama", sAK.ParentId);
            ViewBag.SkId = new SelectList(db.Sk, "Id", "Nama", sAK.SkId);
            return View(sAK);
        }

        // GET: Sak/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAK sAK = db.Sak.Find(id);
            if (sAK == null)
            {
                return HttpNotFound();
            }
            return View(sAK);
        }

        // POST: Sak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SAK sAK = db.Sak.Find(id);
            db.Sak.Remove(sAK);
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
