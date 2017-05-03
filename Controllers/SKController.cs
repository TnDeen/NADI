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
    public class SkController : BaseController
    {

        // GET: Sk
        public ActionResult Index()
        {
            var sk = db.Sk.Include(s => s.Parent);
            return View(sk.ToList());
        }

        // GET: Sk/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SK sK = db.Sk.Find(id);
            if (sK == null)
            {
                return HttpNotFound();
            }
            return View(sK);
        }

        // GET: Sk/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Sk, "Id", "Nama");
            return View();
        }

        // POST: Sk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nama,Kod,Perihal,StatusActive,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] SK sK)
        {
            if (validateSkKod(sK.Kod))
            {

                ModelState.AddModelError("", "Kod Telah Wujud! Sila Gunakan Kod Lain.");
                return View(sK);
            }
            if (ModelState.IsValid)
            {
                db.Sk.Add(sK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.Sk, "Id", "Nama", sK.ParentId);
            return View(sK);
        }

        // GET: Sk/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SK sK = db.Sk.Find(id);
            if (sK == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Sk, "Id", "Nama", sK.ParentId);
            return View(sK);
        }

        // POST: Sk/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParentId,Nama,Kod,Perihal,StatusActive,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] SK sK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Sk, "Id", "Nama", sK.ParentId);
            return View(sK);
        }

        // GET: Sk/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SK sK = db.Sk.Find(id);
            if (sK == null)
            {
                return HttpNotFound();
            }
            return View(sK);
        }

        // POST: Sk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SK sK = db.Sk.Find(id);
            db.Sk.Remove(sK);
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
