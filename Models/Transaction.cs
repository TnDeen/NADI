using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class Transaction : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CustomerID { get; set; }
        public string VendorID { get; set; }
        public string TranStatus { get; set; }
        public Boolean statusActive { get; set; }
        public Boolean claimRequestSend { get; set; }
        public Boolean claimRequestApproval { get; set; }
        public string ulasan { get; set; }
        public int level { get; set; }
        public decimal? point { get; set; }

    }
}