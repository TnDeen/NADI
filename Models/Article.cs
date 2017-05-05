using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Models
{
    public class Article : BaseEntity
    {
        [Key, AllowHtml]
        public int Id { get; set; }
        [Required, AllowHtml]
        public string Header { get; set; }
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
        [AllowHtml]
        public string Link { get; set; }
        [AllowHtml]
        public string imgUrl { get; set; }
        [DefaultValue(true), AllowHtml]
        public Boolean active { get; set; }

        [DefaultValue(false), AllowHtml]
        public Boolean featured { get; set; }

        [ForeignKey("articleType"), AllowHtml]
        public int? articleTypeId { get; set; }
        public virtual SAK articleType { get; set; }
    }
}