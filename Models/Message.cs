using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Models
{
    public class Message :BaseEntity
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Display(Name ="Message")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Perihal { get; set; }
        [Required]
        [Display(Name ="Sender (Email)")]
        public string Sender { get; set; }
        [Required]
        [Display(Name = "Recipient (Email)")]
        public string Recipient { get; set; }
        [DefaultValue(false)]
        public Boolean ReadStatus { get; set; }

    }
}