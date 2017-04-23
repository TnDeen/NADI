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
            string vra = Request.Url.ToString();
            if (!vra.Contains("localhost"))
            {
                var client = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                Credentials = new NetworkCredential("nadikebangsaan@yahoo.com", "nadiadmin123"),
                EnableSsl = true
            };
            
                client.Send("nadikebangsaan@yahoo.com", recipient, subject, body);
            }
            
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

        public Boolean validateUserByEmail(string email)
        {
            Boolean result = false;
            if (email != null)
            {
                var user = db.Users.Where(a => a.UserName.Equals(email)).FirstOrDefault();
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
            sb.Append("NADI");
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

        
    }
}