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

        public void sendNotification(string toUser, String subjext, String message)
        {
            ApplicationUser admin = findUserbyEmail(MyConstant.user_admin_email);
            Message msg = new Message();
            msg.Subject = subjext;
            msg.Perihal = message;
            msg.Sender = admin.Id;
            msg.Recipient = toUser;
            msg.ReadStatus = false;
            db.SystemMessage.Add(msg);
            db.SaveChanges();
        }

        public string findNoAhliById(string id)
        {
            string noAhli = null;
            if (id != null)
            {
                var user = db.Users.Where(a => a.Id.Equals(id)).FirstOrDefault();
                if (user != null)
                {
                    noAhli = user.NomborAhli;
                }
            }
            return noAhli;
        }

        public string finduserIdBynoAhli(string noAhli)
        {
            string parentId = null;
            if (noAhli != null)
            {
                var user = db.Users.Where(a => a.NomborAhli.Equals(noAhli)).FirstOrDefault();
                if (user != null)
                {
                    parentId = user.Id;
                }
            }
            return parentId;
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

        public ApplicationUser findUserbyId(string id)
        {
            return db.Users.Find(id);
        }

        public ApplicationUser findUserbyEmail(string email)
        {
            return db.Users.Where(a => a.Email.Equals(email)).FirstOrDefault();
        }

        private Transaction createTransactionObj(string userId, string parentId, double point, int level)
        {
            Transaction tran = new Transaction();
            tran.CustomerID = userId;
            tran.VendorID = parentId;
            tran.point = Convert.ToDecimal(point);
            tran.level = level;
            tran.CreateBy = User.Identity.Name;
            tran.CreateDate = DateTime.Now;
            tran.LastUpdated = DateTime.Now;
            tran.LastUpdatedBy = User.Identity.Name;
            return tran;
        }

        public void AddTransaction(ApplicationUser parent, ApplicationUser user)
        {
            if (user != null && parent != null)
            {
                Transaction tran = createTransactionObj(user.Id, parent.Id, MyConstant.Point_1, 1);
                db.Transactions.Add(tran);


                if (parent.ParentId != null)
                {
                    var parent2 = idb.Users.Find(parent.ParentId);
                    Transaction tran2 = createTransactionObj(user.Id, parent2.Id, MyConstant.Point_2, 2);
                    db.Transactions.Add(tran2);

                    
                    if (parent2.ParentId != null)
                    {
                        var parent3 = idb.Users.Find(parent2.ParentId);
                        Transaction tran3 = createTransactionObj(user.Id, parent3.Id, MyConstant.Point_3, 3);
                        db.Transactions.Add(tran3);

                        if (parent3.ParentId != null)
                        {
                            var parent4 = idb.Users.Find(parent3.ParentId);
                            Transaction tran4 = createTransactionObj(user.Id, parent4.Id, MyConstant.Point_4, 4);
                            db.Transactions.Add(tran4);

                            if (parent4.ParentId != null)
                            {
                                var parent5 = idb.Users.Find(parent4.ParentId);
                                Transaction tran5 = createTransactionObj(user.Id, parent5.Id, MyConstant.Point_5, 5);
                                db.Transactions.Add(tran5);

                                if (parent5.ParentId != null)
                                {
                                    var parent6 = idb.Users.Find(parent5.ParentId);
                                    Transaction tran6 = createTransactionObj(user.Id, parent6.Id, MyConstant.Point_6, 6);
                                    db.Transactions.Add(tran6);

                                    if (parent6.ParentId != null)
                                    {
                                        var parent7 = idb.Users.Find(parent6.ParentId);
                                        Transaction tran7 = createTransactionObj(user.Id, parent7.Id, MyConstant.Point_7, 7);
                                        db.Transactions.Add(tran7);

                                        if (parent7.ParentId != null)
                                        {
                                            var parent8 = idb.Users.Find(parent7.ParentId);
                                            Transaction tran8 = createTransactionObj(user.Id, parent8.Id, MyConstant.Point_8, 8);
                                            db.Transactions.Add(tran8);

                                            if (parent8.ParentId != null)
                                            {
                                                var parent9 = idb.Users.Find(parent8.ParentId);
                                                Transaction tran9 = createTransactionObj(user.Id, parent9.Id, MyConstant.Point_9, 9);
                                                db.Transactions.Add(tran9);

                                                if (parent9.ParentId != null)
                                                {
                                                    var parent10 = idb.Users.Find(parent9.ParentId);
                                                    Transaction tran10 = createTransactionObj(user.Id, parent10.Id, MyConstant.Point_10, 10);
                                                    db.Transactions.Add(tran10);

                                                    if (parent10.ParentId != null)
                                                    {
                                                        var parent11 = idb.Users.Find(parent10.ParentId);
                                                        Transaction tran11 = createTransactionObj(user.Id, parent11.Id, MyConstant.Point_11, 11);
                                                        db.Transactions.Add(tran11);

                                                        if (parent11.ParentId != null)
                                                        {
                                                            var parent12 = idb.Users.Find(parent11.ParentId);
                                                            Transaction tran12 = createTransactionObj(user.Id, parent12.Id, MyConstant.Point_12, 12);
                                                            db.Transactions.Add(tran12);
                                                        }
                                                    }
                                                }
                                            }
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