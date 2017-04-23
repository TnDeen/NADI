using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5.Common;

namespace MVC5.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Laman Utama";
            ViewBag.PropertyTypeId = new SelectList(db.Sak.ToList().Where(a => a.SkId == 8).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.NegeriID = new SelectList(db.Sak.ToList().Where(a => a.SkId == 7).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.ListingTypeId = new SelectList(db.Sak.Where(a => a.SkId == 9).OrderBy(o => o.Nama), "Id", "Nama");
            return View("Index", "~/Views/Shared/_LayoutHome.cshtml");
        }

        [Authorize]
        public ActionResult Tnc()
        {
            ViewBag.Title = "Terma Dan Syarat";
            return View("Tnc", "~/Views/Shared/_LayoutHome.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
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