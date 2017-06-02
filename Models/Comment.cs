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
    public class Comment : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }

        [ForeignKey("article")]
        public int? articleId { get; set; }
        public virtual Article article { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }

        [InverseProperty("Parent")]
        public ICollection<Comment> ChildList { get; set; }

        [DefaultValue(true)]
        public Boolean active { get; set; }
    }
}