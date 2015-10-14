using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class Course_Student
    {

        public Course course { get; set; }
        public List<Student> StudentList { get; set; }

    }
}