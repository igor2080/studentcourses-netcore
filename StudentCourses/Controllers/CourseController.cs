using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentCourses.Data;
using StudentCourses.Models;

namespace StudentCourses.Controllers
{
    public class CourseController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CourseController(StudentCourseContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.CourseRepository.GetAllAsync());
        }

        // GET: Course/Details/5
        [HttpGet("Course/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            Course course = await _unitOfWork.CourseRepository.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Course course)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CourseRepository.Insert(course);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // GET: Course/Edit/5
        [HttpGet("Course/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Course course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost("Course/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.CourseRepository.Update(course);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // GET: Course/Delete/5
        [HttpGet("Course/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Course course = await _unitOfWork.CourseRepository.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost("Course/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Course course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            _unitOfWork.CourseRepository.Delete(course);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
