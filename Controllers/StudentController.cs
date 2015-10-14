using MVC5.Models;
using MVC5.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class StudentController : Controller
    {

        private SchoolContext db = new SchoolContext();
        // GET: Student
        public ActionResult Index()
        {

            
            return View();
        }

        public ActionResult Student()
        {
            ViewBag.header = "Student Page";

            Course math = new Course();
            math.CourseName = "Math";
            math.Credit = 4;


            //Student deen = new Student();
            //deen.Name = "Deen";
            //Student aleh = new Student();
            //aleh.Name = "aleh";
            //Student tuan = new Student();
            //tuan.Name = "tuan";

            //List<Student> studentList = new List<Student>();
            //studentList.Add(deen);
            //studentList.Add(aleh);
            //studentList.Add(tuan);

         var students = db.Students.ToList();

            Course_Student cs = new Course_Student();
            cs.course = math;
            cs.StudentList = students;

            

            return View(cs);
        }
    }
}