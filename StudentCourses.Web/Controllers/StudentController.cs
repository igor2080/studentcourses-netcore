using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentCourse.Infrastructure.Services;
using StudentCourses.Common.Models;
using StudentCourses.Domain.Entities;
using StudentCourses.Models;

namespace StudentCourses.Controllers
{
    [Route("[controller]")]
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
            
            ViewBag.error = TempData["error"];
            return View(_studentService.GetAll());
        }

        // GET: Student/Details/5
        [HttpGet]
        [Route("Details/{id}")]
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
        [HttpGet("Create")]
        [Authorize(Roles = "Deans")]
        public IActionResult Create()
        {
            var groups = _groupService.GetAll();

            if (groups.Count() < 1)
            {
                TempData["error"] = $"Cannot create students at this point as there are no active groups";
                return RedirectToAction(nameof(Index));
            }

            ViewData["GroupId"] = new SelectList(groups, "Id", "Name");
            return View();
        }

        // POST: Student/Create        
        [HttpPost("Create")]
        [Authorize(Roles = "Deans")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentModel student)
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
        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Deans")]
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
        [HttpPost("Edit/{id}")]
        [Authorize(Roles = "Deans")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,StudentModel student)
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
        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Deans")]
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
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [Authorize(Roles = "Deans")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            StudentModel student = _studentService.Get(id);
            _studentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
