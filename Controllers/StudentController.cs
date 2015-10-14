using MVC5.Models;
using MVC5.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            return View(db.Students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = db.Students.Find(id);

            if (student == null)
                return new HttpNotFoundResult();

            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    student.CreateDate = DateTime.Now;
                    student.LastUpdated = DateTime.Now;
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(student);

                
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = db.Students.Find(id);

            if (student == null)
                return new HttpNotFoundResult();
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = db.Students.Find(id);

            if (student == null)
                return new HttpNotFoundResult();
            return View(student);
            
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Student stdn = db.Students.Find(student.Id);
                    db.Students.Remove(stdn);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(student);
            }
            catch
            {
                return View();
            }
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
