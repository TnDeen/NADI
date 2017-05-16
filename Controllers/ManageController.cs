using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVC5.Models;
using System.Net;
using MVC5.Common;
using System.Collections.Generic;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace MVC5.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            ApplicationUser curuser = UserManager.FindById(userId);
            var userlink = Url.Action("Register", "Account",
               new { userId = curuser.NomborAhli }, protocol: Request.Url.Scheme);
            decimal? acp = 0;
            decimal? pap = 0;
            decimal? ap = 0;

            var aa = db.Transactions.Where(a => a.Address1.Equals(userId)).Select(a => a.Size).Sum();
            if (aa != null)
            {
                pap = aa;
            }
            var bb = db.Transactions.Where(a => a.Address1.Equals(userId)).Select(a => a.Size).Sum();
            if (bb != null)
            {
                ap = bb;
            }
            acp = pap + ap;
            string status = "Not Active";
            if (curuser.EmailConfirmed)
            {
                status = "Active";
            }

            var model = new IndexViewModel
            {

                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                PosRequest = db.PosRequest.Where(a => a.UserId.Equals(userId)).ToList(),
                AppointAgent = db.AppointAgent.Where(a => a.UserId.Equals(userId)).ToList(),
                MembershipRequest = db.MembershipRequest.Where(a => a.UserId.Equals(userId)).ToList(),
                userLink = userlink,
                accumulativePoint = acp,
                potentialPoint = pap,
                wallet = ap,
                accstatus = status,
                totalMessage = db.SystemMessage.Where(t => t.Recipient.Equals(curuser.Id) && !t.ReadStatus).Count(),
                tarikhPenginapan = String.Format("{0:M/d/yyyy}", curuser.tarikhPenginapan),
                TarikhTamatKeahlian = String.Format("{0:M/d/yyyy}", curuser.TarikhTamatAhli),
                totalChild = db.Transactions.Where(a => a.Address1.Equals(userId)).Count(),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                accType = db.MembershipRequest.Where(a => a.StatusActive && a.UserId.Equals(userId)).FirstOrDefault(),
                CurrentUser = curuser,
                Pencadang = curuser.Parent,
                ChildList = idb.Users.Where(a => a.ParentId.Equals(userId)).ToList()

            };
            return View(model);
        }

        public async Task<ActionResult> BarcodeImage()
        {

            ApplicationUser curstudent = UserManager.FindByEmail(User.Identity.Name);
            string code = await UserManager.GenerateUserTokenAsync(MyConstant.ConfirmCusCode, curstudent.Id);
            //var callbackUrl = Url.Action("CreateTransac", "Transaction",
            //   new { userId = curstudent.Id, code = code }, protocol: Request.Url.Scheme);
            var callbackUrl = Url.Action("Register", "Account",
               new { userId = curstudent.NomborAhli }, protocol: Request.Url.Scheme);

            // generating a barcode here. Code is taken from QrCode.Net library
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(callbackUrl, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);

            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;

            var resultStream = new FileStreamResult(memoryStream, "image/png");
            resultStream.FileDownloadName = String.Format("{0}.png", callbackUrl);

            return resultStream;
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser student = UserManager.FindById(id);

            if (student == null)
                return new HttpNotFoundResult();
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplicationUser student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser curstudent = UserManager.FindById(student.Id);
                    curstudent.HomeTown = student.HomeTown;
                    curstudent.BirthDate = student.BirthDate;
                    curstudent.PhoneNumber = student.PhoneNumber;
                    curstudent.ParentId = student.ParentId;
                    curstudent.Nama = student.Nama;
                    curstudent.NamaWaris = student.NamaWaris;
                    curstudent.NomborTelefonWaris = student.NomborTelefonWaris;
                    UserManager.Update(curstudent);
                    return RedirectToAction("Index", "Manage");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // GET: Department/Create
        public ActionResult CreateCus()
        {
            IndexViewModel ivm = new IndexViewModel(); 
            ViewBag.ParentID = new SelectList(idb.Users, "Id", "Email");
            return View(ivm);
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> CreateCus(string ParentID)
        {
            if (ParentID != null)
            {
                string callbackUrl = await SendCustomerConfirmationTokenAsync(ParentID, "Add You As Custmoer");
                ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                                    + "before you can log in.";
                return View("Success");
            }
            return RedirectToAction("Error");
        }

        private async Task<string> SendCustomerConfirmationTokenAsync(string userID, string subject)
        {
            var user =  UserManager.FindById(userID);
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmCustomer", "Manage",
               new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            //await UserManager.SendEmailAsync(userID, subject,
            //   "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            sendMail(subject, "Please confirm to be a customer by clik here <a href=\"" + callbackUrl + "\">here</a>", user.Email);
            return callbackUrl;
        }

        //
        // GET: /Account/ConfirmCustomer
        public async Task<ActionResult> ConfirmCustomer(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            // confirm customer here
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                ApplicationUser curstudent = UserManager.FindById(userId);
                ApplicationUser vendor = UserManager.FindByEmail(User.Identity.Name);
                curstudent.ParentId = vendor.Id;
                UserManager.Update(curstudent);
                ViewBag.Message = "Success Add Customer";
                return View("Success");
            }
            return View("Error");
            
        }


        public ActionResult ApplyMembership()
        {
           
            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            ViewBag.InstructorID = new SelectList(db.Sak.OrderBy(o => o.Nama), "Id", "Nama", user.NegeriId);
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> ApplyMembership(ApplicationUser curuser, HttpPostedFileBase ic, HttpPostedFileBase resit, int? InstructorID)
        {
            DateTime threemonth = DateTime.Today.AddMonths(3);

            if (curuser.tarikhPenginapan < threemonth)
            {
                ModelState.AddModelError("", "Tarikh penginapan mesti lebih 3 bulan.");
                ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
                ViewBag.InstructorID = new SelectList(db.Sak.OrderBy(o => o.Nama), "Id", "Nama", user.NegeriId);
                return View(user);
            }

            if (!curuser.Perakuan)
            {
                ModelState.AddModelError("", "Ruang Perakuan Tidak Diisi!");
                ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
                ViewBag.InstructorID = new SelectList(db.Sak.OrderBy(o => o.Nama), "Id", "Nama", user.NegeriId);
                return View(user);
            }

            try
            {
                

                if (ModelState.IsValid)
                {
                    ApplicationUser user = UserManager.FindById(curuser.Id);
                    List<Models.File> filelist = new List<Models.File>();
                    if (ic != null && ic.ContentLength > 0 && user != null)
                    {
                        List<Models.File> curFiles = db.Files.Where(a => a.userId.Equals(curuser.Id) && a.FileType == FileType.ic).ToList();
                        db.Files.RemoveRange(curFiles);
                        db.SaveChanges();
                        var avatar = new Models.File
                        {
                            FileName = System.IO.Path.GetFileName(ic.FileName),
                            FileType = FileType.ic,
                            ContentType = ic.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(ic.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(ic.ContentLength);
                        }
                        filelist.Add(avatar);
                    }

                    if (resit != null && resit.ContentLength > 0 && user != null)
                    {
                        List<Models.File> curFiles = db.Files.Where(a => a.userId.Equals(curuser.Id) && a.FileType == FileType.resit).ToList();
                        db.Files.RemoveRange(curFiles);
                        db.SaveChanges();
                        var resitFile = new Models.File
                        {
                            FileName = System.IO.Path.GetFileName(resit.FileName),
                            FileType = FileType.resit,
                            ContentType = resit.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(resit.InputStream))
                        {
                            resitFile.Content = reader.ReadBytes(resit.ContentLength);
                        }
                        filelist.Add(resitFile);
                    }

                    if (filelist.Any())
                    {
                        user.Files = filelist;
                    }
                    
                    // copy object value
                    user.Nama = curuser.Nama;
                    user.Alamat = curuser.Alamat;
                    user.NegeriId = InstructorID;
                    user.NoTelRum = curuser.NoTelRum;
                    user.NoTelBim = curuser.NoTelBim;
                    user.BirthDate = curuser.BirthDate;
                    user.TempatLahir = curuser.TempatLahir;

                    user.NoPengenalan = curuser.NoPengenalan;
                    user.Bangsa = curuser.Bangsa;
                    user.Jantina = curuser.Jantina;
                    user.Pekerjaan = curuser.Pekerjaan;
                    user.Jawatan = curuser.Jawatan;
                    user.maritalStatus = curuser.maritalStatus;

                    user.NamaWaris = curuser.NamaWaris;
                    user.NomborTelefonWaris = curuser.NomborTelefonWaris;
                    user.NomborTelefonWarisHP = curuser.NomborTelefonWarisHP;

                    user.tarikhPenginapan = curuser.tarikhPenginapan;

                    user.NamaBank = curuser.NamaBank;
                    user.NomborAkaunBank = curuser.NomborAkaunBank;


                    UserManager.Update(user);
                    // end copy
                    string mesage = "Thank You for your membership application. We will inform once your application has been Approve.";
                    if (!await UserManager.IsEmailConfirmedAsync(User.Identity.GetUserId()))
                    {
                        string callbackUrl = await SendEmailApplyMembership(User.Identity.GetUserId(), "Membership Application Request");
                        ViewBag.Message = mesage;
                        return View("Success");
                    }
                    
                    sendNotification(user.Id, "membership application", mesage);
                    string adminmsg = "User with " + user.NomborAhli + " Has request membership!";
                    ApplicationUser admin = findUserbyEmail(MyConstant.user_admin_email);
                    sendNotification(admin.Id, "membership application", adminmsg);
                    return RedirectToAction("Index", "Manage");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return View();
        }

        private async Task<string> SendEmailApplyMembership(string userID, string subject)
        {
            var user = UserManager.FindById(userID);
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
               new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            //await UserManager.SendEmailAsync(userID, subject,
            //   "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            sendMail(subject, "Please confirm to approve this account by clicking <a href=\'" + callbackUrl + "\'>here</a>", MyConstant.user_admin_email);
            return callbackUrl;
        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}