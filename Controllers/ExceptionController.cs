using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5.Common;

namespace MVC5.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        [ActionLog]
        public ActionResult Index()
        {
            throw new Exception("Something went wrong");
        }

        [ActionLog]
        public ActionResult NotFound()
        {
            return View();
        }

       
    }
}