using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Infrastructure.Services;
using StudentCourses.Common.Models;
using StudentCourses.Domain.Entities;
using StudentCourses.Models;

namespace StudentCourses.Controllers
{
    public class StudentController : Controller
    {
        private readonly IService<StudentModel> _studentService;
        private readonly IService<GroupModel> _groupService;

        public StudentController(IService<StudentModel> studentService, IService<GroupModel> groupService)
        {
            _studentService = studentService ?? throw new ArgumentNullException();
            _groupService = groupService ?? throw new ArgumentNullException();
        }

        // GET: Student
        [HttpGet]
        public IActionResult Index()
        {
            return View(_studentService.GetAll());
        }

        // GET: Student/Details/5
        [HttpGet("Student/Details/{id}")]
        public IActionResult Details(int id)
        {
            StudentModel student = _studentService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        [HttpGet("Student/Create")]
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_groupService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Student/Create        
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Group")] StudentModel student)
        {
            if (ModelState.IsValid)
            {
                student.Group = _groupService.Get(student.Group.Id);
                _studentService.Create(student);
                return RedirectToAction(nameof(Index));
            }

            ViewData["GroupId"] = new SelectList(_groupService.GetAll(), "Id", "Name", student.Group.Id);
            return View(student);
        }

        // GET: Student/Edit/5
        [HttpGet("Student/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            StudentModel student = _studentService.Get(id);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = new SelectList(_groupService.GetAll(), "Id", "Name", student.Group.Id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost("Student/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Group")] StudentModel student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _studentService.Update(student);
                return RedirectToAction(nameof(Index));
            }

            ViewData["GroupId"] = new SelectList(_groupService.GetAll(), "Id", "Name", student.Group.Id);
            return View(student);
        }

        // GET: Student/Delete/5
        [HttpGet("Student/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            StudentModel student = _studentService.Get(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost("Student/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            StudentModel student = _studentService.Get(id);
            _studentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
