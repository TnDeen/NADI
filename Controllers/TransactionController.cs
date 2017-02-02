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
            return View(db.Transactions.ToList().Where(c => c.VendorID.Equals(user.Id)));
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
                AddTransaction(vendor.Id, userId);
                ViewBag.Message = "Success Add Transaction";
                return View("Success");
            }
            return View("Error");
        }
    }
}