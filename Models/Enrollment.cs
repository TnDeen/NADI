using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class Enrollment:BaseEntity
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public decimal? Grade { get; set; }
        public Student Student { get; set; }

    }
}