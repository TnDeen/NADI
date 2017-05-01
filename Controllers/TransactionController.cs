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
using OfficeOpenXml;
using PagedList;

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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, SearchVO search)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var alltran = from t in db.Transactions
                          select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                alltran = alltran.Where(s => s.Address1.Contains(searchString)
                                       || s.UnitNo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    alltran = alltran.OrderByDescending(s => s.Address1);
                    break;
                case "Date":
                    alltran = alltran.OrderBy(s => s.AuctionDate);
                    break;
                case "date_desc":
                    alltran = alltran.OrderByDescending(s => s.AuctionDate);
                    break;
                default:  // Name ascending 
                    alltran = alltran.OrderBy(s => s.Address1);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            ApplicationUser user = UserManager.FindByEmail(User.Identity.Name);
            string role = UserManager.GetRoles(user.Id).First();
            
            TableVO tbvo = new TableVO
            {
                allTransaction = alltran.ToList(),
                childMap = mapDataFromTransaction(user.Id, null),
                childActiveMap = mapDataFromTransaction(user.Id, true),
                childNonActiveMap = mapDataFromTransaction(user.Id, false)

            };
            return View(alltran.ToPagedList(pageNumber, pageSize));
        }

        private Dictionary<string, string> mapDataFromTransaction(String userId, Boolean? active)
        {
            var all = MyConstant.allLevel.ToList();
            var map = new Dictionary<string, string>();
            foreach (int i in all)
            {
                var c = db.Transactions.Where(a => a.Address1.Equals(userId) && a.Size == i).Count();
                if (active != null && active.Value)
                {
                     c = db.Transactions.Where(a => a.Address1.Equals(userId) && a.Size == i).Count();
                } else if (active != null && !active.Value)
                {
                    c = db.Transactions.Where(a => a.Address1.Equals(userId) && a.Size == i).Count();
                }
                
                map.Add(i.ToString(), c.ToString());
            }
            return map;
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
               
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ClaimRequestList()
        {
            FileVO vo = new FileVO();
            var llsRequest = from tran in db.Transactions
                             join uuser in db.Users on tran.Address1 equals uuser.Id
                             where tran.Size == 1
                             select new FileVO { transantion = tran, CurrentUser = uuser };
            var btlRequest = from tran in db.Transactions
                             join uuser in db.Users on tran.Address1 equals uuser.Id
                             where tran.Size == 1
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
                
                ViewBag.Message = "Success Add Transaction";
                return View("Success");
            }
            return View("Error");
        }

        public FileResult DownloadFile()
        {
            string path = HttpContext.Server.MapPath("~/App_Data/uploads/DownloadFile.xlsx");
            return File(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }


        public ActionResult Upload(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {

                    string fileName = file.FileName;
                    var basePath = Server.MapPath("~/App_Data/uploads");
                    var path = Path.Combine(basePath, fileName);
                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    file.SaveAs(path);

                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));


                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var workSheet = package.Workbook.Worksheets["Pos"];
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {

                            var pTypeRaw = workSheet.Cells[rowIterator, 1].Value;

                            if (pTypeRaw == null)
                            {
                                break;
                            }
                            String pType = pTypeRaw.ToString();
                            String unitNo = workSheet.Cells[rowIterator, 2].Value.ToString();
                            String Address1 = workSheet.Cells[rowIterator, 3].Value.ToString();
                            String poskod = workSheet.Cells[rowIterator, 4].Value.ToString();

                            String bandar = workSheet.Cells[rowIterator, 5].Value.ToString();
                            String negeri = workSheet.Cells[rowIterator, 6].Value.ToString();

                            String area = workSheet.Cells[rowIterator, 7].Value.ToString();
                            String price = workSheet.Cells[rowIterator, 8].Value.ToString();

                            String rawdtval = workSheet.Cells[rowIterator, 9].Value.ToString();
                            String[] dtarray = rawdtval.Split('.');
                            String aucDate = dtarray[0] + "/" + dtarray[1] + "/" + dtarray[2];

                            String aucType = workSheet.Cells[rowIterator, 10].Value.ToString();
                            String aucBank = workSheet.Cells[rowIterator, 11].Value.ToString();

                            String venue = workSheet.Cells[rowIterator, 12].Value.ToString();
                            String timeRaw = workSheet.Cells[rowIterator, 13].Value.ToString();
                            DateTime strdate = Convert.ToDateTime(timeRaw);
                            String time = strdate.ToString("hh:mm tt");
                            String aucneer = workSheet.Cells[rowIterator, 14].Value.ToString();
                            String lawyer = workSheet.Cells[rowIterator, 15].Value.ToString();
                            String asignor = workSheet.Cells[rowIterator, 16].Value.ToString();



                            Listing tran = new Listing();

                            tran.PropertyTypeId = db.Sak.Where(a => a.Kod.Equals(pType)).Select(a => a.Id).FirstOrDefault();
                            tran.UnitNo = unitNo;
                            tran.Address1 = Address1;
                            tran.Poskod = Convert.ToInt32(poskod);
                            tran.BandarId = db.Sak.Where(a => a.Nama.Equals(bandar)).Select(a => a.Id).FirstOrDefault();
                            tran.NegeriId = db.Sak.Where(a => a.Nama.Equals(negeri)).Select(a => a.Id).FirstOrDefault();
                            tran.Size = decimal.Parse(area);
                            tran.Price = decimal.Parse(price);
                            tran.AuctionDate = DateUtil.convertStringToDate(aucDate);
                            tran.AuctionTypeId = db.Sak.Where(a => a.Kod.Equals(aucType)).Select(a => a.Id).FirstOrDefault();
                            tran.AuctionBankId = db.Sak.Where(a => a.Kod.Equals(aucBank)).Select(a => a.Id).FirstOrDefault();
                            tran.AuctionVenue = venue;
                            tran.AuctionTime = time;
                            tran.AuctionNeer = aucneer;
                            tran.Lawyer = lawyer;
                            tran.Assignor = asignor;

                            tran.CreateBy = MyConstant.user_admin_email;
                            tran.CreateDate = DateTime.Now;
                            tran.LastUpdated = DateTime.Now;
                            tran.LastUpdatedBy = MyConstant.user_admin_email;

                            db.Transactions.Add(tran);
                            db.SaveChanges();

                            var id = tran.Id;
                            if (id > 0)
                            {
                                string imgId = id.ToString();
                                var imgBasePath = Server.MapPath("~/Content/img/property-type/" + imgId);
                                if (!Directory.Exists(imgBasePath))
                                {
                                    Directory.CreateDirectory(imgBasePath);
                                }
                            }



                        }
                    }

                }
            }
            return RedirectToAction("Index");
        }
    }
}