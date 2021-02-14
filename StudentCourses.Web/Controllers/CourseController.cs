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
        [HttpGet("Course/Details/{id}")]
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
        [HttpGet("Course/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description")] CourseModel course)
        {
            if (ModelState.IsValid)
            {
                _service.Create(course);
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // GET: Course/Edit/5
        [HttpGet("Course/Edit/{id}")]
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
        [HttpPost("Course/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description")] CourseModel course)
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
        [HttpGet("Course/Delete/{id}")]
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
        [HttpPost("Course/Delete/{id}"), ActionName("Delete")]
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
