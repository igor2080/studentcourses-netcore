using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCourse.Infrastructure.Services;
using StudentCourses.Common.Models;

namespace StudentCourses.Web.Controllers
{
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly IService<CourseModel> _service;

        public CourseController(IService<CourseModel> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.error = TempData["error"];
            return View(_service.GetAll());
        }

        // GET: Course/Details/5
        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            CourseModel course = _service.Get(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost("Create")]
        [Authorize(Roles = "Deans")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseModel course)
        {
            if (ModelState.IsValid)
            {
                _service.Create(course);
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // GET: Course/Edit/5
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            CourseModel course = _service.Get(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost("Edit/{id}")]
        [Authorize(Roles = "Deans")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CourseModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(course);
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // GET: Course/Delete/5
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            CourseModel course = _service.Get(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Deans")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            CourseModel course = _service.Get(id);
            if (course.Groups.Length > 0)
            {
                TempData["error"] = $"Cannot delete course {course.Name} because it contains active groups";
            }
            else
            {
                _service.Delete(course);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
