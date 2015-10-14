using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    public class Course :BaseEntity
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int Credit { get; set; }
        public List<Enrollment> Enrollments { get; set; }

    }
}