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
    [Authorize]
    public class NoteController : BaseController
    {
        // GET: Blog
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";




            var alltran = from t in db.Article
                          where t.articleType.Kod.Equals("ARTCL_TYPE_NOTE")
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

        public ActionResult Article(string articleKod, int? id)
        {
            Article article = null;
            if (articleKod == null && id != null)
            {
                article = db.Article.Where(a => a.Id == id).FirstOrDefault();
            }
            else
            {
                article = db.Article.Where(a => a.articleType.Kod.Equals(articleKod)).FirstOrDefault();
            }

            ViewBag.articleId = article.Id;
            ViewBag.UserId = findCurrentUserId();

            ArticleVO avo = new ArticleVO();
            avo.article = article;
            avo.commentList = db.Comment.Where(a => a.articleId == article.Id).ToList();

            return View(avo);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleVO articleVO, string content, int? articleId, string UserId)
        {
            Comment cmnt = new Comment();
            cmnt.articleId = articleId;
            cmnt.UserId = UserId;
            cmnt.Content = content;
            db.Comment.Add(cmnt);
            db.SaveChanges();
            return RedirectToAction("Article", new { id = articleId });

        }
    }
}