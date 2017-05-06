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
        public string nama { get; set; }
        [Required]
        public string ic { get; set; }
        [Required]
        public string contact { get; set; }
        public string address { get; set; }
    }
}