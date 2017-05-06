using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class Menu : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Controller { get; set; }

        public string htmlAtribute { get; set; }

        [ForeignKey("articleType")]
        public int? articleTypeId { get; set; }
        public virtual SAK articleType { get; set; }

        [ForeignKey("menuType")]
        public int? menuTypeId { get; set; }
        public virtual SAK menuType { get; set; }
    }
}