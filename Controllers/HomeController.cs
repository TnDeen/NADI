﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5.Common;
using MVC5.Models.VM;
using MVC5.Models;

namespace MVC5.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            
            ViewBag.PropertyTypeId = new SelectList(db.Sak.ToList().Where(a => a.SkId == 8).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.NegeriID = new SelectList(db.Sak.ToList().Where(a => a.SkId == 7).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.ListingTypeId = new SelectList(db.Sak.Where(a => a.SkId == 9).OrderBy(o => o.Nama), "Id", "Nama");

            
            var list = (from t in db.Transactions
                        orderby t.CreateDate descending
                          select new ListingVO { listing = t }).Take(4);

            var listFeatured = (from t in db.Transactions
                                where t.featured
                        orderby t.CreateDate descending
                        select new ListingVO { listing = t }).Take(4);

            var hotId = from t in db.SistemCounter
                         where t.Kod.Contains("LSTGCOUNT")
                         orderby t.runningNumber descending
                         select t.Kod;

            List<string> kod = hotId.ToList();
            List<int> ids = new List<int>();
            foreach (var n in kod)
            {
                string[] arr = n.Split('-');
                if (arr[1] != null)
                {
                    ids.Add(int.Parse(arr[1]));
                }
                
            }

            var listHot = (from t in db.Transactions
                           where ids.Contains(t.Id)
                        select new ListingVO { listing = t }).Take(4);

            SearchVO svo = new SearchVO();
            svo.listing = list.ToList();
            svo.listingFeatured = listFeatured.ToList();
            svo.listingHot = listHot.ToList();
            return View(svo);
        }

        [Authorize]
        public ActionResult Tnc()
        {
            ViewBag.Title = "Terma Dan Syarat";
            return View("Tnc", "~/Views/Shared/_LayoutHome.cshtml");
        }

        public ActionResult About()
        {
            var article = db.Article.Where(a => a.articleType.Kod.Equals("ARTCL_TYPE_ABOUT")).FirstOrDefault();

            return View(article);
        }

        public ActionResult Article(string articleKod, int? id)
        {
            Article article = null;
            if (articleKod == null && id != null)
            {
                article = db.Article.Where(a => a.Id == id).FirstOrDefault();
            } else
            {
                article = db.Article.Where(a => a.articleType.Kod.Equals(articleKod)).FirstOrDefault();
            }
            

            return View(article);
        }

        public ActionResult FAQ()
        {
            return View(db.Article.Where(a => a.articleType.Kod.Equals("ARTCL_TYPE_FAQ")).ToList());
        }

        public ActionResult News()
        {
            return View(db.News.ToList());
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Blog";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ComingSoon()
        {
            ViewBag.Title = "Coming Soon!";
            ViewBag.Message = "Mobile apps for android going to be release in short time.";
            return View("Success");
        }

        public ActionResult Offline()
        {
            ViewBag.totalregister = db.Users.Count();
            ViewBag.Title = "Site Offline";
            return View("Offline", "~/Views/Shared/_LayoutOffline.cshtml");
        }

        public ActionResult Search()
        {
            ViewBag.PropertyTypeId = new SelectList(db.Sak.ToList().Where(a => a.SkId == 8).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.NegeriID = new SelectList(db.Sak.ToList().Where(a => a.SkId == 7).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.ListingTypeId = new SelectList(db.Sak.Where(a => a.SkId == 9).OrderBy(o => o.Nama), "Id", "Nama");
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Message message)
        {
            if (message != null)
            {
                sendNotification(MyConstant.user_admin_email, message.Subject, message.Perihal, message.Sender);
                sendMail(message.Subject, message.Perihal, MyConstant.user_admin_email);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}