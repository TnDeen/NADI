using MVC5.Common;
using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class JsonDemoController : BaseController
    {
        // GET: JsonDemo
        public ActionResult Index()
        {
            return View();
        }

        private List<ListingSimpleVO> GetListing()
        {
            var result = (from t in db.Transactions
                          select new ListingSimpleVO { PropertyTypeId = t.PropertyTypeId, UnitNo = t.UnitNo, NegeriId = t.NegeriId }).Take(5);
            return result.ToList();
        }

        public JsonResult GetUsersData()
        {
            var users = GetListing();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public class ListingSimpleVO
        {
            public int? PropertyTypeId { get; set; }
            public string UnitNo { get; set; }
            public int? NegeriId { get; set; }
        }
    }
}