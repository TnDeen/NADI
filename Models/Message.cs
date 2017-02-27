using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class Message :BaseEntity
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        public string Perihal { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Recipient { get; set; }
        [DefaultValue(false)]
        public Boolean ReadStatus { get; set; }

    }
}