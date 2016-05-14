using MVC5.Common;
using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(idb.Users.ToList());
        }
    }
}