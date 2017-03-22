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
            ViewBag.totalregister = db.Users.Count();
            ViewBag.Title = "Laman Utama";
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