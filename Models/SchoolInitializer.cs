using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVC5.Models
{
    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SchoolContext>
    {

        protected override void Seed(SchoolContext context)
        {
            var student = new List<Student>{

            new Student {Name = "Deen",CreateDate=DateTime.Now,LastUpdated=DateTime.Now},
            new Student {Name = "Izyan",CreateDate=DateTime.Now,LastUpdated=DateTime.Now},
            new Student {Name = "Tuan",CreateDate=DateTime.Now,LastUpdated=DateTime.Now},
            new Student {Name = "TuanDeen",CreateDate=DateTime.Now,LastUpdated=DateTime.Now}
            };

            foreach (var item in student)
            {
                context.Students.Add(item);

            }

            var course = new List<Course>{

            new Course {CourseName = "Java",CreateDate=DateTime.Now,Credit=4,LastUpdated=DateTime.Now},
            new Course {CourseName = ".NET",CreateDate=DateTime.Now,Credit=5,LastUpdated=DateTime.Now}
           
            };

            foreach (var item in course)
            {
                context.Courses.Add(item);

            }

            context.SaveChanges();

            var enrollment = new List<Enrollment>{

            new Enrollment {StudentId = 1,CourseId=1,Grade=4,CreateDate=DateTime.Now,LastUpdated=DateTime.Now},
            new Enrollment {StudentId = 1,CourseId=2,Grade=6,CreateDate=DateTime.Now,LastUpdated=DateTime.Now}
           
            };

            foreach (var item in enrollment)
            {
                context.Enrollments.Add(item);

            }

            context.SaveChanges();
        }
    }
}