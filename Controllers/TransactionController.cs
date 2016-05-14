using MVC5.Common;
using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace MVC5.Controllers
{
    public class TransactionController : BaseController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Transaction
        public ActionResult Index()
        {
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            string role = UserManager.GetRoles(user.Id).First();
            Boolean vendor = role.Equals(MyConstant.Role_Vendor);
            return View(vendor ? db.Transactions.ToList().Where(c => c.VendorID.Equals(user.Id))
                : db.Transactions.ToList().Where(c => c.CustomerID.Equals(user.Id)));
        }

        // GET: Student/Create
        [Authorize(Roles="User")]
        public ActionResult Create()
        {
            ApplicationUser curstudent = UserManager.FindByEmail(User.Identity.Name);
            string code = MyConstant.ConfirmCusCode;
            var callbackUrl = Url.Action("CreateTransac", "Transaction",
               new { userId = curstudent.Id, code = code }, protocol: Request.Url.Scheme);
            ViewBag.Message = "Please Copy This Link and scan with vendor To Get Point"
                                + " Link <a href=\"" + callbackUrl + "\">here</a>";
            return View("Success");
        }

        // POST: Student/Create
        [Authorize(Roles = "Vendor")]
        public ActionResult CreateTransac(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            if (code.Equals(MyConstant.ConfirmCusCode))
            {
                Transaction tran = new Transaction();
                ApplicationUser vendor = UserManager.FindByEmail(User.Identity.Name);
                ApplicationUser customer = UserManager.FindById(userId);
                tran.CustomerID = userId;
                tran.VendorID = vendor.Id;
                tran.point = 200;
                tran.CreateBy = User.Identity.Name;
                tran.CreateDate = DateTime.Now;
                tran.LastUpdated = DateTime.Now;
                tran.LastUpdatedBy = User.Identity.Name;
                db.Transactions.Add(tran);
                db.SaveChanges();
                ViewBag.Message = "Success Add Transaction";
                return View("Success");
            }
            return View("Error");
        }
    }
}