using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class SAK : RefDataEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        public virtual SAK Parent { get; set; }

        [InverseProperty("Parent")]
        public ICollection<SAK> ChildList { get; set; }

        [ForeignKey("Sk")]
        public int SkId { get; set; }
        public virtual SK Sk { get; set; }
    }
}