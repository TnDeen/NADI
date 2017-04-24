using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class ListingVO
    {
        public SearchVO search { get; set; }
        public Listing listing { get; set; }
        public string imgUrl { get; set; }
    }
}