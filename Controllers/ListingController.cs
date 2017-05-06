using MVC5.Common;
using MVC5.Models;
using MVC5.Models.VM;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class ListingController : BaseController
    {
        // GET: Listing
        public ActionResult Index(string sortOrder, string currentFilter, int? page, SearchVO search, string address, int? propertyType, int? state)
        {

            ViewBag.PropertyTypeId = new SelectList(db.Sak.ToList().Where(a => a.SkId == 8).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.NegeriID = new SelectList(db.Sak.ToList().Where(a => a.SkId == 7).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.ListingTypeId = new SelectList(db.Sak.Where(a => a.SkId == 9).OrderBy(o => o.Nama), "Id", "Nama");

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (search == null)
            {
                search = new SearchVO();
                search.Address = address;
                search.PropertyTypeId = propertyType;
                search.NegeriId = state;
            }
            string searchString = search.Address;
            
            if (searchString != null)
            {
                page = 1;
                search.Address = searchString;
                ViewBag.enterLocation = searchString;
            }
            else
            {
                searchString = currentFilter;
                ViewBag.enterLocation = "All";
            }

            ViewBag.CurrentFilter = searchString;

            var news = (from n in db.News
                       select n).Take(5);

            var alltran = from t in db.Transactions
                          select new ListingVO { listing = t, NewsList = news.ToList() };
            if (!String.IsNullOrEmpty(searchString))
            {
                alltran = alltran.Where(s => s.listing.Address1.Contains(searchString)
                                       || s.listing.UnitNo.Contains(searchString));
            }

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

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            
            List<ListingVO> nwlist = new List<ListingVO>();
            foreach (ListingVO ls in alltran)
            {
                ls.search = new SearchVO { Address = searchString };
                var basePath = Server.MapPath("~/Content/img/property-type/" + ls.listing.Id);
                string filename = "default" + ".jpg";
                var path = Path.Combine(basePath, filename);
                if (System.IO.File.Exists(path))
                {
                    ls.imgUrl = MyConstant.property_img_base_url + ls.listing.Id + "/" + filename;
                }
                nwlist.Add(ls);
            }

            ViewBag.listSize = nwlist.Count();

            return View(nwlist.ToPagedList(pageNumber, pageSize));
        }

        public String PropertyImage(String id)
        {
            return MyConstant.property_img_default_url + id;
        }

        // GET: Listing/Details/5
        public ActionResult Details(int id, string address, int? propertyType, int? state)
        {
            ViewBag.PropertyTypeId = new SelectList(db.Sak.ToList().Where(a => a.SkId == 8).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.NegeriID = new SelectList(db.Sak.ToList().Where(a => a.SkId == 7).OrderBy(o => o.Nama), "Id", "Nama");
            ViewBag.ListingTypeId = new SelectList(db.Sak.Where(a => a.SkId == 9).OrderBy(o => o.Nama), "Id", "Nama");
            
            ListingVO vo = new ListingVO();
            var listing = db.Transactions.Where(a => a.Id == id).FirstOrDefault();
            vo.listing = listing;

            var basePath = Server.MapPath("~/Content/img/property-type/" + listing.Id);
            string filename = "default" + ".jpg";
            var path = Path.Combine(basePath, filename);
            if (System.IO.File.Exists(path))
            {
                vo.imgUrl = MyConstant.property_img_base_url + listing.Id + "/" + filename;
            }
            var news = db.News.Take(5);
            vo.NewsList = news.ToList();


            ViewBag.ListingViewCount = updateListingView(listing.Id);

            SearchVO searchvo = new SearchVO();
            searchvo.Address = address;
            searchvo.NegeriId = state;
            searchvo.PropertyTypeId = propertyType;
            vo.search = searchvo;
            return View(vo);
        }

        // GET: Listing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Listing/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Listing/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Listing/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Listing/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Listing/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
