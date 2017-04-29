using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class AffiliateComission :BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Introducer")]
        public string IntroducerId { get; set; }
        public virtual ApplicationUser Introducer { get; set; }

        [DefaultValue(false)]
        public Boolean statusActive { get; set; }
        public string ulasan { get; set; }
        public int level { get; set; }
        public decimal? point { get; set; }
    }
}