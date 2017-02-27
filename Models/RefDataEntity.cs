using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class RefDataEntity : BaseEntity
    {
       
        public string Nama { get; set; }
        public string Kod { get; set; }
        public string Perihal { get; set; }
        [DefaultValue(true)]
        public Boolean StatusActive { get; set; }
    }
}