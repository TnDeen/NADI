using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5.Common;
using MVC5.Models.VM;

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
                          select new ListingVO { listing = t }).Take(4);

            SearchVO svo = new SearchVO();
            svo.listing = list.ToList();
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

        public ActionResult Article(string articleKod)
        {
            var article = db.Article.Where(a => a.articleType.Kod.Equals(articleKod)).FirstOrDefault();

            return View(article);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Offline()
        {
            ViewBag.totalregister = db.Users.Count();
            ViewBag.Title = "Site Offline";
            return View("Offline", "~/Views/Shared/_LayoutOffline.cshtml");
        }
    }
}