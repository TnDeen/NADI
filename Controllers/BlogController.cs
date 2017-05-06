using MVC5.Common;
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
    public class BlogController : BaseController
    {
        // GET: Blog
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
                        
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            

            

            var alltran = from t in db.Article
                          where t.articleType.Kod.Equals("ARTCL_TYPE_BLOG")
                          select new ListingVO { article = t };
            

            switch (sortOrder)
            {
                case "name_desc":
                    alltran = alltran.OrderByDescending(s => s.article.Header);
                    break;
                case "Date":
                    alltran = alltran.OrderBy(s => s.article.DateCreated);
                    break;
                case "date_desc":
                    alltran = alltran.OrderByDescending(s => s.article.DateCreated);
                    break;
                default:  // Name ascending 
                    alltran = alltran.OrderBy(s => s.article.Header);
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            List<ListingVO> nwlist = new List<ListingVO>();
            foreach (ListingVO ls in alltran)
            {
                
                var basePath = Server.MapPath("~/Content/img/article/" + ls.article.Id);
                string filename = "Default";
                if (ls.article.imgUrl != null)
                {
                    filename = ls.article.imgUrl;
                }
                var path = Path.Combine(basePath, filename);
                if (System.IO.File.Exists(path))
                {
                    ls.imgUrl = MyConstant.property_img_base_url + ls.article.Id + "/" + filename;
                }

                nwlist.Add(ls);
            }

            ViewBag.listSize = nwlist.Count();

            return View(nwlist.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Article(int id)
        {
            var article = db.Article.Where(a => a.Id == id).FirstOrDefault();

            return View(article);
        }
    }
}