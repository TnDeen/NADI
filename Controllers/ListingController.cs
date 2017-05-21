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
        public ActionResult Index(string sortOrder, int? page, ListingVO listingVo, SearchVO search, string address, int? propertyType, int? state, int? type, string minPrice, string maxPrice, string minArea, string maxArea, DateTime? aucDt)
        {

            List<ListingVO> nwlist = searchAuction(sortOrder, listingVo, search, address, propertyType, state, type, minPrice, maxPrice, minArea, maxArea, aucDt);
            if (nwlist.Count() == 0)
            {
                return RedirectToAction("Search", "Home");
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(nwlist.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult IndexLink(string sortOrder, int? page, string address, int? propertyType, int? state, int? type, string minPrice, string maxPrice, string minArea, string maxArea, DateTime? aucDt)
        {

            List<ListingVO> nwlist = searchAuction(sortOrder, null, null, address, propertyType, state, type, minPrice, maxPrice, minArea, maxArea, aucDt);
            if (nwlist.Count() == 0)
            {
                return RedirectToAction("Search", "Home");
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View("Index",nwlist.ToPagedList(pageNumber, pageSize));
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
            if (User.Identity.IsAuthenticated)
            {
                string userid = findCurrentUserId();
                List<MembershipRequest> mtype = db.MembershipRequest.Where(a => a.StatusActive && a.UserId.Equals(userid)).ToList();
                mtype.RemoveAll(a => a.TarikhTamat.Value.Date <= DateTime.Now.Date);
                vo.Subscribe = mtype.Any();

            }
            


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
