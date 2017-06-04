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
    public class NewsLetterSubscriptionsController : BaseController
    {

        public ActionResult SendNewsletterToSubcription()
        {
            sendNewsletter(true, false);
            return RedirectToAction("Success", "Home", new { message = "NewsLetter Sent!" });
        }

        public ActionResult SendNewsletterToUser()
        {
            sendNewsletter(false, true);
            return RedirectToAction("Success", "Home", new { message = "NewsLetter Sent!" });
        }

        // GET: NewsLetterSubscriptions
        public ActionResult Index()
        {
            return View(db.NewsLetterSubscription.ToList());
        }

        // GET: NewsLetterSubscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetterSubscription newsLetterSubscription = db.NewsLetterSubscription.Find(id);
            if (newsLetterSubscription == null)
            {
                return HttpNotFound();
            }
            return View(newsLetterSubscription);
        }

        // GET: NewsLetterSubscriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsLetterSubscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Subcribe,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] NewsLetterSubscription newsLetterSubscription, string email)
        {
            if (ModelState.IsValid)
            {
                newsLetterSubscription.Subcribe = true;
                db.NewsLetterSubscription.Add(newsLetterSubscription);
                db.SaveChanges();
                return RedirectToAction("Success", "Home", new { message = "Newsletter Subcription Success!" });
            }

            return View(newsLetterSubscription);
        }

        // GET: NewsLetterSubscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetterSubscription newsLetterSubscription = db.NewsLetterSubscription.Find(id);
            if (newsLetterSubscription == null)
            {
                return HttpNotFound();
            }
            return View(newsLetterSubscription);
        }

        // POST: NewsLetterSubscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Subcribe,CreateDate,CreateBy,LastUpdated,LastUpdatedBy")] NewsLetterSubscription newsLetterSubscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsLetterSubscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsLetterSubscription);
        }

        // GET: NewsLetterSubscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsLetterSubscription newsLetterSubscription = db.NewsLetterSubscription.Find(id);
            if (newsLetterSubscription == null)
            {
                return HttpNotFound();
            }
            return View(newsLetterSubscription);
        }

        // POST: NewsLetterSubscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsLetterSubscription newsLetterSubscription = db.NewsLetterSubscription.Find(id);
            db.NewsLetterSubscription.Remove(newsLetterSubscription);
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
