using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class ArticleVO
    {
        public Article article { get; set; }
        public List<Comment> commentList { get; set; }
    }
}