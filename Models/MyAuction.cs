using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class MyAuction : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Auction Date")]
        public DateTime? AuctionDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Sold Price")]
        public decimal? SoldPrice { get; set; }
        [Display(Name = "Reserve Price")]
        public decimal? ReservePrice { get; set; }

        [DefaultValue(true)]
        public Boolean SelfBid { get; set; }

        [DefaultValue(true)]
        public Boolean AppointAgent { get; set; }

        public string Status { get; set; }
    }
}