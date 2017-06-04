using Microsoft.Owin.Security;
using MVC5.Models;
using MVC5.Models.VM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
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

        public List<ListingVO> searchAuction(string sortOrder, ListingVO listingVo, SearchVO search, string address, int? propertyType, int? state, int? type, string minPrice, string maxPrice, string minArea, string maxArea, DateTime? aucDt)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (search == null)
            {
                search = new SearchVO();
                search.Address = address;
                search.PropertyTypeId = propertyType;
                search.NegeriId = state;
                search.ListingTypeId = type;
                search.MinPrice = minPrice;
                search.MaxPrice = maxPrice;
                search.MinSize = minArea;
                search.MaxSize = maxArea;
                search.AuctionDate = aucDt;
            }

            if (listingVo == null)
            {
                listingVo = new ListingVO();
                listingVo.search = search;
                listingVo.PropertyTypeId = propertyType;
                listingVo.NegeriId = state;
                listingVo.ListingTypeId = type;
            }

            ViewBag.PropertyTypeId = new SelectList(db.Sak.ToList().Where(a => a.SkId == 8).OrderBy(o => o.Nama), "Id", "Nama", search.PropertyTypeId);
            ViewBag.NegeriID = new SelectList(db.Sak.ToList().Where(a => a.SkId == 7).OrderBy(o => o.Nama), "Id", "Nama", search.NegeriId);
            ViewBag.ListingTypeId = new SelectList(db.Sak.Where(a => a.SkId == 9).OrderBy(o => o.Nama), "Id", "Nama", search.ListingTypeId);

            var news = (from n in db.News
                        select n).Take(5);

            var alltran = from t in db.Transactions
                          select new ListingVO { listing = t, NewsList = news.ToList() };
            // start filter
            // location
            ViewBag.enterLocation = "";
            if (!String.IsNullOrEmpty(search.Address))
            {
                ViewBag.enterLocation = search.Address;
                ViewBag.address = search.Address;
                alltran = alltran.Where(s => s.listing.Address1.Contains(search.Address));
            }

            //property type
            if (listingVo.PropertyTypeId != null && listingVo.ValidateAllType(listingVo.PropertyTypeId))
            {
                ViewBag.enterLocation = ViewBag.enterLocation + " type " + listingVo.getNama(listingVo.PropertyTypeId);
                ViewBag.propertyType = listingVo.PropertyTypeId;
                alltran = alltran.Where(s => s.listing.PropertyTypeId == listingVo.PropertyTypeId);
            }
            //negeri
            if (listingVo.NegeriId != null && listingVo.ValidateAllType(listingVo.NegeriId))
            {
                ViewBag.enterLocation = ViewBag.enterLocation + " at " + listingVo.getNama(listingVo.NegeriId);
                ViewBag.state = listingVo.NegeriId;
                alltran = alltran.Where(s => s.listing.NegeriId == listingVo.NegeriId);
            }
            
            //min price
            if (search.MinPrice != null)
            {
                ViewBag.minPrice = search.MinPrice;
                decimal mp = decimal.Parse(search.MinPrice);
                alltran = alltran.Where(s => s.listing.Price >= mp);
            }

            //max price
            if (search.MaxPrice != null)
            {
                ViewBag.maxPrice = search.MaxPrice;
                decimal mxp = decimal.Parse(search.MaxPrice);
                alltran = alltran.Where(s => s.listing.Price <= mxp);
            }
            //min area
            if (search.MinSize != null)
            {
                ViewBag.minArea = search.MinSize;
                decimal ms = decimal.Parse(search.MinSize);
                alltran = alltran.Where(s => s.listing.Size >= ms);
            }

            //max area
            if (search.MaxSize != null)
            {
                ViewBag.maxArea = search.MaxSize;
                decimal mxs = decimal.Parse(search.MaxSize);
                alltran = alltran.Where(s => s.listing.Size <= mxs);
            }

            // date filter remove to back due to linq datetime issue
            // end filter

            switch (sortOrder)
            {
                case "name_desc":
                    alltran = alltran.OrderByDescending(s => s.listing.Address1);
                    break;
                case "Date":
                    alltran = alltran.OrderBy(s => s.listing.AuctionDate);
                    break;
                case "date_desc":
                    alltran = alltran.OrderByDescending(s => s.listing.AuctionDate);
                    break;
                default:  // Name ascending 
                    alltran = alltran.OrderBy(s => s.listing.Address1);
                    break;
            }

            List<ListingVO> nwlist = new List<ListingVO>();
            List<ListingVO> curlist = alltran.ToList();
            //listing type
            if (listingVo.ListingTypeId != null)
            {
                ViewBag.enterLocation = ViewBag.enterLocation + " " + listingVo.getNama(search.ListingTypeId);
                ViewBag.type = listingVo.ListingTypeId;
                string kod = listingVo.getKod(listingVo.ListingTypeId);
                DateTime today = DateTime.Now;
                switch (kod)
                {
                    case MyConstant.LSTNG_TYPE_ACTIVE:
                        curlist.RemoveAll(a => a.listing.AuctionDate == null || a.listing.AuctionDate.Value.Date < DateTime.Now.Date);
                        break;
                    case MyConstant.LSTNG_TYPE_EXPRD:
                        curlist.RemoveAll(a => a.listing.AuctionDate == null || a.listing.AuctionDate.Value.Date >= DateTime.Now.Date);
                        break;
                    case MyConstant.LSTNG_TYPE_PENDING:
                        curlist.RemoveAll(a => a.listing.AuctionDate != null);
                        break;
                    default:
                        break;
                }

            }

            //auction date
            if (search.AuctionDate != null)
            {
                ViewBag.aucDt = search.AuctionDate.Value.Date;
                curlist.RemoveAll(a => a.listing.AuctionDate == null || a.listing.AuctionDate.Value.Date != DateTime.Now.Date);
            }


            if (curlist.Any())
            {
                
                foreach (ListingVO ls in curlist)
                {
                    ls.search = search;
                    var basePath = Server.MapPath("~/Content/img/property-type/" + ls.listing.Id);
                    string filename = "default" + ".jpg";
                    var path = Path.Combine(basePath, filename);
                    if (System.IO.File.Exists(path))
                    {
                        ls.imgUrl = MyConstant.property_img_base_url + ls.listing.Id + "/" + filename;
                    }
                    nwlist.Add(ls);
                }
            }

            ViewBag.listSize = nwlist.Count();

            return nwlist;
        }

        public ActionResult Success(string title, string message)
        {
            ViewBag.Title = title;
            ViewBag.Message = message;
            return View();
        }

        public string readIntroducerCookies(string userId)
        {
            string introducerId = null;
            if (userId != null)
            {
                introducerId = userId;
                HttpCookie cookie = new HttpCookie("Introducer");
                cookie.Values["IntroducerId"] = userId;
                Response.SetCookie(cookie); //SetCookie() is used for update the cookie.
            }
            else
            {
                HttpCookie cookie = Request.Cookies["Introducer"];
                if (cookie != null)
                {
                    introducerId = Server.HtmlEncode(cookie.Values["IntroducerId"]);
                }

            }
            return introducerId;
        }

        public void sendNewsletter(bool subscription, bool appuser)
        {

            List<string> emailList = new List<string>();

            if (subscription)
            {
                var email = from t in db.NewsLetterSubscription
                            where t.Subcribe
                            select t.Email;
                emailList.AddRange(email.ToList());
            }
               
            

            if (appuser)
            {
                var emailUser = from t in db.Users
                            select t.Email;
                emailList.AddRange(emailUser.ToList());
            }

            populateEmailData(MyConstant .Web_Name + " Hot Deals!", "", MyConstant.user_admin_email, "ARTCL_TYPE_EML_NWSLTR_TMPLT", emailList);
        }

        public void sendMail(string subject, string body, string recipient)
        {
            populateEmailData(subject, body, recipient, null, null);
        }

        public void sendMail(string subject, string body, string recipient, string templateKod)
        {
            populateEmailData(subject, body, recipient, templateKod, null);
        }

        public void populateEmailData(string subject, string body, string recipient, string templateKod, List<string> emailList)
        {
            string vra = Request.Url.ToString();
            Boolean enableEmail = true;
            if (enableEmail)
            {
                
                string from = ConfigurationManager.AppSettings["mailAccount"];
                string fromName = "Jomrumahlelong";
                string template = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                string content = string.Format(template, from, fromName, body);

                if (templateKod != null)
                {
                    content = populateDataTemplate(templateKod, body, recipient);
                }

                List<string> finalList = new List<string>();
                finalList.Add(recipient);
                finalList.AddRange(emailList);

                if (finalList.Any())
                {
                    foreach(string email in finalList)
                    {
                        //create the mail message 
                        MailMessage mail = new MailMessage();

                        //set the addresses 
                        mail.From = new MailAddress(from, fromName);
                        mail.To.Add(email);

                        //set the content 
                        mail.Subject = subject;
                        mail.Body = content;
                        mail.IsBodyHtml = true;
                        //send the message 
                        SmtpClient smtp = new SmtpClient(MyConstant.email_smtp, 587);

                        NetworkCredential Credentials = new NetworkCredential(from, ConfigurationManager.AppSettings["mailPassword"]);
                        smtp.Credentials = Credentials;
                        smtp.Send(mail);

                        // for testing purpose
                        //sendNotification(email, subject, content);
                    }
                }

                
            }

        }

        private string populateDataTemplate(string templateKod, string body, string recipient)
        {
            string result = null;
            if ("ARTCL_TYPE_EML_NWSLTR_TMPLT".Equals(templateKod))
            {
                var list = (from t in db.Transactions
                            orderby t.CreateDate descending
                            select new ListingVO { listing = t }).Take(4);
                List<ListingVO> listing = list.ToList();
                StringBuilder sb = new StringBuilder();
                string header = db.Article.Where(a => a.articleType.Kod.Equals("ARTCL_TYPE_EML_NWSLTR_TMPLT_HEADER")).FirstOrDefault().Content;
                string footer = db.Article.Where(a => a.articleType.Kod.Equals("ARTCL_TYPE_EML_NWSLTR_TMPLT_FOOTER")).FirstOrDefault().Content;
                string content = db.Article.Where(a => a.articleType.Kod.Equals(templateKod)).FirstOrDefault().Content;
                if (listing.Any())
                {
                    sb.Append(header);
                    sb.Append("<hr/>");
                    foreach (ListingVO item in listing)
                    {
                        if (item.imgUrl == null)
                        {
                            item.imgUrl = GetImageUrl(item.listing.PropertyTypeId.ToString());
                        }
                        string readmore = GetBaseUrl() + "property-for-auction/Details/" + item.listing.GenerateSlug();
                        sb.Append(string.Format(content, item.imgUrl, item.listing.Address1, item.listing.Price, item.listing.AuctionDate.Value.ToShortDateString(), readmore, item.listing.PropertyType.Nama));
                        sb.Append("<hr/>");
                    }
                    sb.Append(footer);

                }
                result = sb.ToString();
            } else
            {
                string content = db.Article.Where(a => a.articleType.Kod.Equals(templateKod)).FirstOrDefault().Content;
                result = string.Format(content, body, recipient);
            }


            return result;
        }

        public void sendNotification(string toUser, String subjext, String message)
        {
            sendNotification(toUser, subjext, message, MyConstant.user_admin_email);
        }

        public void sendNotification(string toUser, String subjext, String message, string fromUser)
        {
            Message msg = new Message();
            msg.Subject = subjext;
            msg.Perihal = message;
            msg.Sender = fromUser;
            msg.Recipient = toUser;
            msg.ReadStatus = false;
            db.SystemMessage.Add(msg);
            db.SaveChanges();
        }

        public string findCurrentUserId()
        {
            return idb.Users.Where(a => a.UserName.Equals(User.Identity.Name)).FirstOrDefault().Id;
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

        public ApplicationUser finduserBynoAhli(string noAhli)
        {
            ApplicationUser parent = null;
            if (noAhli != null)
            {
                var user = db.Users.Where(a => a.NomborAhli.Equals(noAhli)).FirstOrDefault();
                if (user != null)
                {
                    parent = user;
                }
            }
            return parent;
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

        public Boolean validateSkKod(string kod)
        {
            Boolean result = false;
            if (kod != null)
            {
                var sk = db.Sk.Where(a => a.Kod.Equals(kod)).FirstOrDefault();
                if (sk != null)
                {
                    result = true;
                }
            }
            return result;
        }

        public Boolean validateSakKod(string kod)
        {
            Boolean result = false;
            if (kod != null)
            {
                var sk = db.Sak.Where(a => a.Kod.Equals(kod)).FirstOrDefault();
                if (sk != null)
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
            sb.Append(MyConstant.member_code);
            sb.Append(year);
            string kod = sb.ToString();
            counter = db.SistemCounter.Where(a => a.Kod.Equals(kod)).FirstOrDefault();

            if (counter != null)
            {
                int num = counter.runningNumber + 1;
                counter.runningNumber = num;
                curnumber = counter.runningNumber.ToString("D4");
            }
            else
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

        public int updateListingView(int id)
        {
            SistemId counter = null;
            int num = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append(MyConstant.listing_count_code);
            sb.Append('-');
            sb.Append(id.ToString());
            string kod = sb.ToString();
            counter = db.SistemCounter.Where(a => a.Kod.Equals(kod)).FirstOrDefault();

            if (counter != null)
            {
                num = counter.runningNumber + 1;
                counter.runningNumber = num;
            }
            else
            {
                num = 1;
                counter = new SistemId();
                counter.Kod = kod;
                counter.runningNumber = num;
                db.SistemCounter.Add(counter);

            }
            db.SaveChanges();
            return num;
        }

        public ApplicationUser findUserbyId(string id)
        {
            return db.Users.Find(id);
        }

        public ApplicationUser findUserbyEmail(string email)
        {
            return db.Users.Where(a => a.Email.Equals(email)).FirstOrDefault();
        }

        public string GetArticleContent(string articleKod)
        {
            var article = db.Article.Where(a => a.articleType.Kod.Equals(articleKod)).FirstOrDefault();

            return article.Content;
        }

        public string GetBaseUrl()
        {
            string vra = Request.Url.ToString();
            string baseurl = "";
            if (vra.Contains("localhost"))
            {
                baseurl = MyConstant.Base_Url_local;
            } else
            {
                baseurl = MyConstant.Base_Url;
            }
            return baseurl;
        }

        public string GetImageUrl(string id)
        {
            return GetBaseUrl() + MyConstant.property_img_default_url + id + MyConstant.file_jpg;
        }

        public string GenerateCallbackUrl()
        {
            return Url.Action("ResetPassword", "Account", new { userId = "1", code = "1" }, protocol: Request.Url.Scheme);
        }


    }
}