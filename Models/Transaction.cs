using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class Listing : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PropertyType")]
        public int? PropertyTypeId { get; set; }
        public virtual SAK PropertyType { get; set; }

        public string UnitNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }

        public int Poskod { get; set; }

        [ForeignKey("Bandar")]
        public int? BandarId { get; set; }
        public virtual SAK Bandar { get; set; }

        [ForeignKey("Negeri")]
        public int? NegeriId { get; set; }
        public virtual SAK Negeri { get; set; }

        public decimal? Size { get; set; }
        public decimal? Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Auction Date")]
        public DateTime? AuctionDate { get; set; }

        public string AuctionTime { get; set; }

        [ForeignKey("AuctionType")]
        public int? AuctionTypeId { get; set; }
        public virtual SAK AuctionType { get; set; }

        [ForeignKey("AuctionBank")]
        public int? AuctionBankId { get; set; }
        public virtual SAK AuctionBank { get; set; }

        public string AuctionVenue { get; set; }
        public string AuctionNeer{ get; set; }
        public string Lawyer { get; set; }
        public string Assignor { get; set; }

        public string imageUrl { get; set; }


    }
}