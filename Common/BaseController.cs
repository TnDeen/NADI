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
        protected SchoolContext db = new SchoolContext();
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
    }
}