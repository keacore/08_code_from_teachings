using System.Collections.Generic;
using ConsoleApplication.Models;
using ConsoleApplication.Models.Entities;
using ConsoleApplication.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApplication.Controllers
{
    public class StudentController : Controller
    {
        // Tightly Couple
        MyDbContext db = new MyDbContext();

       // Loosly Coupled
        private IStudentRepository studentRepository;
        private ICourseRepository courseRepository;

        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            
        }

        // Read
        [HttpGet]
        public IActionResult Index()
        {
            //List<Student> students = db.Students.ToList();
            IEnumerable<Student> students = studentRepository.GetAll();
            return View(students);
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //Create
        [HttpPost]
        public IActionResult Create(Student st)
        {
            if (ModelState.IsValid)
            {
                studentRepository.Save(st);

                // Moved to the repo
                // db.Students.Add(st);
                // db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }

        // Update
        public IActionResult Update(int id)
        {
            //Student student = db.Students.Find(id);
            Student student =  studentRepository.Get(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.Update(student);
                //db.Students.Update(student);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            //Student student = db.Students.Find(id);
            Student student =  studentRepository.Get(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(Student st)
        {
            //Student student = db.Students.Find(st.StudentID);
            //db.Students.Remove(st);
            //db.SaveChanges();
            studentRepository.Delete(st);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var student = studentRepository.Get(id);
            return View(student);
        }

        public IActionResult Course(int id)
        {
            StudentCourseViewModel stcvm = new StudentCourseViewModel();
            stcvm.Student = studentRepository.Get(id);
            stcvm.Courses = courseRepository.GetAll();  

            return View(stcvm);

        }

    }
}