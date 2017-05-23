using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class NewsLetterSubscription : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [DefaultValue(true)]
        public Boolean Subcribe { get; set; }
    }
}