using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class BaseEntity
    {
        public DateTime DateCreated
        {
            get
            {
                return this.CreateDate.HasValue
                   ? this.CreateDate.Value
                   : DateTime.Now;
            }

            set { this.CreateDate = value; }
        }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }

        public DateTime DateUpdated
        {
            get
            {
                return this.LastUpdated.HasValue
                   ? this.LastUpdated.Value
                   : DateTime.Now;
            }

            set { this.CreateDate = value; }
        }
        public DateTime? LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
       
    }
}