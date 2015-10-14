using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class Student : BaseEntity
    {
        [Key]   
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}