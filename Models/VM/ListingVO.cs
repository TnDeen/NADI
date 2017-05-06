using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class ListingVO
    {
        public SearchVO search { get; set; }
        public Listing listing { get; set; }
        public Article article { get; set; }
        public string imgUrl { get; set; }

        public List<News> NewsList { get; set; }

        [DefaultValue(false)]
        public Boolean Subscribe { get; set; }
    }
}