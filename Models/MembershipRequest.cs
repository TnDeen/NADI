using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class MembershipRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Introducer")]
        public string IntroducerId { get; set; }
        public virtual ApplicationUser Introducer { get; set; }

        [ForeignKey("PackageType")]
        public int? PackageTypeId { get; set; }
        public virtual SAK PackageType { get; set; }

        public DateTime? TarikhSah { get; set; }
        public DateTime? TarikhTamat { get; set; }

        [DefaultValue(true)]
        public Boolean StatusActive { get; set; }
    }
}