using Microsoft.Owin.Security;
using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Common
{
    public class BaseController : Controller
    {
        protected IdentityContext idb = new IdentityContext();
        protected ApplicationDbContext db = new ApplicationDbContext();
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public void sendMail(string subject, string body, string recipient)
        {
            var client = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                Credentials = new NetworkCredential("nadikebangsaan@yahoo.com", "nadiadmin123"),
                EnableSsl = true
            };
            client.Send("nadikebangsaan@yahoo.com", recipient, subject, body);
        }

        public void AddTransaction(ApplicationUser parent, ApplicationUser user)
        {
            if (user != null && parent != null)
            {
                Transaction tran = new Transaction();
                tran.CustomerID = user.Id;
                tran.VendorID = parent.Id;
                tran.point = MyConstant.Point_1;
                tran.level = 1;
                tran.CreateBy = User.Identity.Name;
                tran.CreateDate = DateTime.Now;
                tran.LastUpdated = DateTime.Now;
                tran.LastUpdatedBy = User.Identity.Name;
                db.Transactions.Add(tran);


                if (parent.ParentId != null)
                {
                    var parent2 = idb.Users.Find(parent.ParentId);
                    Transaction tran2 = new Transaction();
                    tran2.CustomerID = user.Id;
                    tran2.VendorID = parent2.Id;
                    tran2.point = MyConstant.Point_2;
                    tran2.level = 2;
                    tran2.CreateBy = User.Identity.Name;
                    tran2.CreateDate = DateTime.Now;
                    tran2.LastUpdated = DateTime.Now;
                    tran2.LastUpdatedBy = User.Identity.Name;
                    db.Transactions.Add(tran2);

                    
                    if (parent2.ParentId != null)
                    {
                        var parent3 = idb.Users.Find(parent2.ParentId);
                        Transaction tran3 = new Transaction();
                        tran3.CustomerID = user.Id;
                        tran3.VendorID = parent3.Id;
                        tran3.point = MyConstant.Point_3;
                        tran3.level = 3;
                        tran3.CreateBy = User.Identity.Name;
                        tran3.CreateDate = DateTime.Now;
                        tran3.LastUpdated = DateTime.Now;
                        tran3.LastUpdatedBy = User.Identity.Name;
                        db.Transactions.Add(tran3);

                        if (parent3.ParentId != null)
                        {
                            var parent4 = idb.Users.Find(parent3.ParentId);
                            Transaction tran4 = new Transaction();
                            tran4.CustomerID = user.Id;
                            tran4.VendorID = parent4.Id;
                            tran4.point = MyConstant.Point_4;
                            tran4.level = 4;
                            tran4.CreateBy = User.Identity.Name;
                            tran4.CreateDate = DateTime.Now;
                            tran4.LastUpdated = DateTime.Now;
                            tran4.LastUpdatedBy = User.Identity.Name;
                            db.Transactions.Add(tran4);

                        }
                    } 
                }

                
                db.SaveChanges();

            }
            
        }
    }
}