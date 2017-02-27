using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class SK : RefDataEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        public virtual SK Parent { get; set; }

        [InverseProperty("Parent")]
        public ICollection<SK> ChildList { get; set; }

        public ICollection<SAK> SakList { get; set; }
    }
}