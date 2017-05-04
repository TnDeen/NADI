using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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

        public string alamatFull()
        {
            string almt = string.Format("{0}-{1}-{2}", Id, Address1,Negeri.Nama);  
            return almt;
        }

        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}", Id, Address1);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }


    }
}