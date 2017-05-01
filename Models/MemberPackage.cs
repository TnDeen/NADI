using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class MemberPackage : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string ContentFree { get; set; }

        [DefaultValue(true)]
        public Boolean FreeMember { get; set; }

        public string ContentVip { get; set; }

        [DefaultValue(true)]
        public Boolean VipMember { get; set; }
    }
}