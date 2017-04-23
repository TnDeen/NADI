using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class TableVO
    {
        protected ApplicationDbContext db = new ApplicationDbContext();


        public List<Listing> allTransaction { get; set; }
        public Dictionary<string, string> childMap {get; set;}
        public Dictionary<string, string> childActiveMap { get; set; }
        public Dictionary<string, string> childNonActiveMap { get; set; }
    }
}