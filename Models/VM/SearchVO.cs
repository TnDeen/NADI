using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class SearchVO
    {
        public string Address { get; set; }
        
        public int? PropertyTypeId { get; set; }
        public int? NegeriId { get; set; }
        public int? ListingTypeId { get; set; }

        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string MinSize { get; set; }
        public string MaxSize { get; set; }

        public List<ListingVO> listing { get; set; }
    }
}