using Microsoft.Owin.Security;
using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        public void UpdateTransaction(ApplicationUser user, Boolean status)
        {
            db.Transactions.Where(t => t.CustomerID.Equals(user.Id)).ToList().ForEach(x => x.statusActive = true);
            db.SaveChanges();
        }

        public Boolean validateNoAhli(string noAhli)
        {
            Boolean result = false;
            if (noAhli != null)
            {
                var user = db.Users.Where(a => a.NomborAhli.Equals(noAhli)).FirstOrDefault();
                if (user != null)
                {
                    result = true;
                }
            }
            return result;
        }

        public string generateNoAhli()
        {
            SistemId counter = null;
            string curnumber = null;

            string year = DateTime.Now.Year.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("ND");
            sb.Append(year);
            string kod = sb.ToString();
            counter = db.SistemCounter.Where(a => a.Kod.Equals(kod)).FirstOrDefault();

            if (counter != null)
            {
                int num = counter.runningNumber + 1;
                counter.runningNumber = num;
                curnumber = counter.runningNumber.ToString("D4");
            } else
            {
                curnumber = 1.ToString("D4");
                counter = new SistemId();
                counter.Kod = kod;
                counter.runningNumber = 1;
                db.SistemCounter.Add(counter);

            }
            db.SaveChanges();
            
            
            
            return kod + curnumber;
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

                            if (parent4.ParentId != null)
                            {
                                var parent5 = idb.Users.Find(parent4.ParentId);
                                Transaction tran5 = new Transaction();
                                tran5.CustomerID = user.Id;
                                tran5.VendorID = parent5.Id;
                                tran5.point = MyConstant.Point_5;
                                tran5.level = 5;
                                tran5.CreateBy = User.Identity.Name;
                                tran5.CreateDate = DateTime.Now;
                                tran5.LastUpdated = DateTime.Now;
                                tran5.LastUpdatedBy = User.Identity.Name;
                                db.Transactions.Add(tran5);

                                if (parent5.ParentId != null)
                                {
                                    var parent6 = idb.Users.Find(parent5.ParentId);
                                    Transaction tran6 = new Transaction();
                                    tran6.CustomerID = user.Id;
                                    tran6.VendorID = parent6.Id;
                                    tran6.point = MyConstant.Point_6;
                                    tran6.level = 6;
                                    tran6.CreateBy = User.Identity.Name;
                                    tran6.CreateDate = DateTime.Now;
                                    tran6.LastUpdated = DateTime.Now;
                                    tran6.LastUpdatedBy = User.Identity.Name;
                                    db.Transactions.Add(tran6);

                                    if (parent6.ParentId != null)
                                    {
                                        var parent7 = idb.Users.Find(parent6.ParentId);
                                        Transaction tran7 = new Transaction();
                                        tran7.CustomerID = user.Id;
                                        tran7.VendorID = parent7.Id;
                                        tran7.point = MyConstant.Point_7;
                                        tran7.level = 7;
                                        tran7.CreateBy = User.Identity.Name;
                                        tran7.CreateDate = DateTime.Now;
                                        tran7.LastUpdated = DateTime.Now;
                                        tran7.LastUpdatedBy = User.Identity.Name;
                                        db.Transactions.Add(tran7);

                                        if (parent7.ParentId != null)
                                        {
                                            var parent8 = idb.Users.Find(parent7.ParentId);
                                            Transaction tran8 = new Transaction();
                                            tran8.CustomerID = user.Id;
                                            tran8.VendorID = parent8.Id;
                                            tran8.point = MyConstant.Point_8;
                                            tran8.level = 8;
                                            tran8.CreateBy = User.Identity.Name;
                                            tran8.CreateDate = DateTime.Now;
                                            tran8.LastUpdated = DateTime.Now;
                                            tran8.LastUpdatedBy = User.Identity.Name;
                                            db.Transactions.Add(tran8);

                                        }

                                    }

                                }

                            }

                        }
                    } 
                }

                
                db.SaveChanges();

            }
            
        }
    }
}