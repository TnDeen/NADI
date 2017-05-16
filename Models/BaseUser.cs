using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class BaseUser : BaseEntity
    {
        [Required]
        [Display(Name = "Name")]
        public string nama { get; set; }
        [Required]
        [Display(Name = "IC. Number")]
        public string ic { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string contact { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
    }
}