using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5.Models;
using MVC5.Common;
using System.IO;
using OfficeOpenXml;

namespace MVC5.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    public class ListingadminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Listingadmin
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(l => l.AuctionBank).Include(l => l.AuctionType).Include(l => l.Bandar).Include(l => l.Negeri).Include(l => l.PropertyType);
            return View(transactions.ToList());
        }

        // GET: Listingadmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Transactions.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // GET: Listingadmin/Create
        public ActionResult Create()
        {
            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama");
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama");
            return View();
        }

        // POST: Listingadmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PropertyTypeId,UnitNo,Address1,Address2,Address3,Address4,Poskod,BandarId,NegeriId,Size,Price,AuctionDate,AuctionTime,AuctionTypeId,AuctionBankId,AuctionVenue,AuctionNeer,Lawyer,Assignor,imageUrl,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(listing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionBankId);
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionTypeId);
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama", listing.BandarId);
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama", listing.NegeriId);
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama", listing.PropertyTypeId);
            return View(listing);
        }

        // GET: Listingadmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Transactions.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionBankId);
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionTypeId);
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama", listing.BandarId);
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama", listing.NegeriId);
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama", listing.PropertyTypeId);
            return View(listing);
        }

        // POST: Listingadmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PropertyTypeId,UnitNo,Address1,Address2,Address3,Address4,Poskod,BandarId,NegeriId,Size,Price,AuctionDate,AuctionTime,AuctionTypeId,AuctionBankId,AuctionVenue,AuctionNeer,Lawyer,Assignor,imageUrl,DateCreated,CreateDate,CreateBy,DateUpdated,LastUpdated,LastUpdatedBy")] Listing listing, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {

                        string fileName = file.FileName;
                        string extension = Path.GetExtension(fileName);
                        string newfilename = "default" + extension;
                        var basePath = Server.MapPath("~/Content/img/property-type/" + listing.Id);
                        var path = Path.Combine(basePath, newfilename);
                        if (!Directory.Exists(basePath))
                        {
                            Directory.CreateDirectory(basePath);
                        }
                        file.SaveAs(path);
                    }
                }

                db.Entry(listing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuctionBankId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionBankId);
            ViewBag.AuctionTypeId = new SelectList(db.Sak, "Id", "Nama", listing.AuctionTypeId);
            ViewBag.BandarId = new SelectList(db.Sak, "Id", "Nama", listing.BandarId);
            ViewBag.NegeriId = new SelectList(db.Sak, "Id", "Nama", listing.NegeriId);
            ViewBag.PropertyTypeId = new SelectList(db.Sak, "Id", "Nama", listing.PropertyTypeId);
            return View(listing);
        }

        // GET: Listingadmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Transactions.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // POST: Listingadmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Listing listing = db.Transactions.Find(id);
            db.Transactions.Remove(listing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

                            tran.PropertyTypeId = db.Sak.Where(a => a.Nama.Equals(pType)).Select(a => a.Id).FirstOrDefault();
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
                        }
                    }

                }
            }
            return RedirectToAction("Index");
        }
    }
}
