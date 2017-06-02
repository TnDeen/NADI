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
    [Authorize(Roles ="Admin")]
    public class CommentsController : BaseController
    {

        // GET: Comments
        public ActionResult Index()
        {
            var comment = db.Comment.Include(c => c.article).Include(c => c.Parent).Include(c => c.User);
            return View(comment.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.articleId = new SelectList(db.Article, "Id", "Header");
            ViewBag.ParentId = new SelectList(db.Comment, "Id", "UserId");
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,articleId,UserId,ParentId,active,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] Comment comment, string content)
        {
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.articleId = new SelectList(db.Article, "Id", "Header", comment.articleId);
            ViewBag.ParentId = new SelectList(db.Comment, "Id", "UserId", comment.ParentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.articleId = new SelectList(db.Article, "Id", "Header", comment.articleId);
            ViewBag.ParentId = new SelectList(db.Comment, "Id", "UserId", comment.ParentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,articleId,UserId,ParentId,active,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.articleId = new SelectList(db.Article, "Id", "Header", comment.articleId);
            ViewBag.ParentId = new SelectList(db.Comment, "Id", "UserId", comment.ParentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "NomborAhli", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
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
