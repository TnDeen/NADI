﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Models
{
    public class News : BaseEntity
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
        public string Link { get; set; }
        [DefaultValue(true)]
        public Boolean active { get; set; }

    }
}