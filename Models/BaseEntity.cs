using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
       
    }
}