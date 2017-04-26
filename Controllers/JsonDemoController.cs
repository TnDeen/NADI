using MVC5.Common;
using MVC5.Models;
using MVC5.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        [HttpPost]
        public JsonResult UpdateUsersDetail(string usersJson)
        {
            var js = new JavaScriptSerializer();
            ListingSimpleVO[] user = js.Deserialize<ListingSimpleVO[]>(usersJson);
            Console.Write("Success!");
            //TODO: user now contains the details, you can do required operations  
            return Json("User Details are updated");
        }
    }
}