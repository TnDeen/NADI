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
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using MVC5.Models.VM;

namespace MVC5.Controllers
{
    [Authorize]
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
            return View(db.Transactions.ToList().Where(c => c.VendorID.Equals(user.Id)).OrderBy(c => c.level));
        }

        public ActionResult MessageList()
        {
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            return View(db.SystemMessage.ToList().Where(c => c.Recipient.Equals(user.Id)).OrderBy(c => c.DateCreated));
        }

        public ActionResult ReadMessage(int id, bool activate)
        {
            db.SystemMessage.Where(t => t.Id.Equals(id)).ToList().ForEach(x => x.ReadStatus = true);
            db.SaveChanges();
            return RedirectToAction("MessageList");
        }

        // GET: Transaction
        [Authorize(Roles ="Admin")]
        public ActionResult MembershipRequest()
        {
            FileVO vo = new FileVO();
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            string role = UserManager.GetRoles(user.Id).First();

            var btlRequest = from file in db.Files
                                 join uuser in db.Users on file.userId equals uuser.Id
                             where !uuser.EmailConfirmed
                                 select new FileVO { curFile = file, CurrentUser = uuser };

            var llsRequest = from file in db.Files
                             join uuser in db.Users on file.userId equals uuser.Id
                             where uuser.EmailConfirmed
                             select new FileVO { curFile = file, CurrentUser = uuser };

            vo.lulusList = llsRequest.ToList<FileVO>();
            vo.batalList = btlRequest.ToList<FileVO>();
            return View(vo);
        }

        [Authorize]
        public ActionResult claimRequest(int id, bool activate)
        {
            if (id > 0)
            {
                Transaction tran = db.Transactions.Find(id);
                if (tran != null)
                {
                    tran.claimRequestSend = activate;
                    db.Entry(tran).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ClaimRequestList()
        {
            FileVO vo = new FileVO();
            var llsRequest = from tran in db.Transactions
                             join uuser in db.Users on tran.VendorID equals uuser.Id
                             where tran.claimRequestSend where tran.claimRequestApproval
                             select new FileVO { transantion = tran, CurrentUser = uuser };
            var btlRequest = from tran in db.Transactions
                             join uuser in db.Users on tran.VendorID equals uuser.Id
                             where tran.claimRequestSend
                             where !tran.claimRequestApproval
                             select new FileVO { transantion = tran, CurrentUser = uuser };
            vo.lulusList = llsRequest.ToList<FileVO>();
            vo.batalList = btlRequest.ToList<FileVO>();
            return View(vo);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ApproveClaimRequest(int id, bool activate)
        {
            if (id > 0)
            {
                Transaction tran = db.Transactions.Find(id);
                if (tran != null)
                {
                    tran.claimRequestApproval = activate;
                    db.Entry(tran).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    string mesage = "Your claim with amount of " + tran.point + "have been approved!";
                    sendNotification(tran.VendorID, "Claim Approve", mesage);
                }
            }
            return RedirectToAction("ClaimRequestList");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ApproveMembershipRequest(string id, bool activate)
        {
            if  (id != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ApplicationUser user = findUserbyId(id);
                        
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        user.EmailConfirmed = activate;
                        user.TarikhSahAhli = DateTime.Now;
                        if (activate)
                        {
                            user.TarikhTamatAhli = DateTime.Now.AddYears(1);

                        } else
                        {
                            user.TarikhTamatAhli = DateTime.Now;
                        }
                        
                        db.SaveChanges();
                        UpdateTransaction(user, true);
                        string mesage = "Your membership application have been approved! membership is valid from " + user.TarikhSahAhli + " to " + user.TarikhTamatAhli;
                        sendNotification(user.Id, "Membership application approved", mesage);

                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                

            }
            return RedirectToAction("MembershipRequest");
        }

        // GET: Student/Create
        [Authorize(Roles="User")]
        public async Task<ActionResult> Create()
        {
            ApplicationUser curstudent = UserManager.FindByEmail(User.Identity.Name);
            string code = await UserManager.GenerateUserTokenAsync(MyConstant.ConfirmCusCode, curstudent.Id);
            //var callbackUrl = Url.Action("CreateTransac", "Transaction",
            //   new { userId = curstudent.Id, code = code }, protocol: Request.Url.Scheme);
            var callbackUrl = Url.Action("Register", "Account",
               new { userId = curstudent.Id}, protocol: Request.Url.Scheme);
            ViewBag.Message = "Please Copy This Link and scan with vendor To Get Point"
                                + " Link <a href=\"" + callbackUrl + "\">here</a>";
            ViewBag.callbackUrl = callbackUrl;
            return View("Success");
        }

        public ActionResult BarcodeImage(String barcodeText)
        {
            // generating a barcode here. Code is taken from QrCode.Net library
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(barcodeText, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);

            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;

            var resultStream = new FileStreamResult(memoryStream, "image/png");
            resultStream.FileDownloadName = String.Format("{0}.png", barcodeText);

            return resultStream;
        }

        // POST: Student/Create
        [Authorize(Roles = "Vendor")]
        public async Task<ActionResult> CreateTransac(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
             // confirm customer here
            var result = await UserManager.VerifyUserTokenAsync(userId, MyConstant.ConfirmCusCode, code);
            if (result)
            {
                ApplicationUser vendor = UserManager.FindByEmail(User.Identity.Name);
                ApplicationUser user = UserManager.FindById(userId);
                AddTransaction(vendor, user);
                ViewBag.Message = "Success Add Transaction";
                return View("Success");
            }
            return View("Error");
        }
    }
}