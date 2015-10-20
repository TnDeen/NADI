using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Areas.Employee.Controllers
{
    public class HomeController : Controller
    {
        // GET: Employee/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}