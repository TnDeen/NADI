using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5.Models;
using System.IO;

namespace MVC5.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        public ActionResult Index()
        {
            var article = db.Article.Include(a => a.articleType);
            return View(article.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.articleTypeId = new SelectList(db.Sak.Where(a => a.Sk.Kod.Equals("ARTCL_TYPE")), "Id", "Nama");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Header,Content,Link,active,featured,articleTypeId,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Article.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.articleTypeId = new SelectList(db.Sak, "Id", "Nama", article.articleTypeId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.articleTypeId = new SelectList(db.Sak, "Id", "Nama", article.articleTypeId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,Content,Link,active,featured,articleTypeId,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] Article article, FormCollection formCollection, string summernote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                string newfilename = null;
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {

                        string fileName = file.FileName;
                        string extension = Path.GetExtension(fileName);
                        newfilename = "default" + extension;
                        var basePath = Server.MapPath("~/Content/img/article/" + article.Id);
                        var path = Path.Combine(basePath, newfilename);
                        if (!Directory.Exists(basePath))
                        {
                            Directory.CreateDirectory(basePath);
                        }
                        file.SaveAs(path);
                    }
                }

                article.imgUrl = newfilename;
                article.Content = summernote;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.articleTypeId = new SelectList(db.Sak, "Id", "Nama", article.articleTypeId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Article.Find(id);
            db.Article.Remove(article);
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
