using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class ListingVO
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public SearchVO search { get; set; }
        public Listing listing { get; set; }
        public Article article { get; set; }
        public string imgUrl { get; set; }

        public int? PropertyTypeId { get; set; }
        public int? NegeriId { get; set; }
        public int? ListingTypeId { get; set; }

        public List<News> NewsList { get; set; }

        [DefaultValue(false)]
        public Boolean Subscribe { get; set; }

        public bool ValidateAllType(int? id)
        {
            string nama = db.Sak.Where(a => a.Id == id).FirstOrDefault().Nama;
            return !nama.Contains("All");
        }

        public string getKod(int? id)
        {
            return db.Sak.Where(a => a.Id == id).FirstOrDefault().Kod;
        }

        public string getNama(int? id)
        {
            string nama = "";
            if (id != null)
            {
                nama = db.Sak.Where(a => a.Id == id).FirstOrDefault().Nama;
            }

            return nama;
        }
    }
}