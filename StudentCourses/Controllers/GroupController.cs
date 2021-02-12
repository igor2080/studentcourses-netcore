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
    public class GroupController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEnumerable<Course> _courses; 
        private readonly IEnumerable<Group> _groups;

        public GroupController(StudentCourseContext context)
        {
            _unitOfWork = new UnitOfWork(context);
            _courses = _unitOfWork.CourseRepository.GetAll();
            _groups = _unitOfWork.GroupRepository.GetAll();
        }

        // GET: Group
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.error = TempData["error"];
            return View(_groups);
        }

        // GET: Group/Details/5
        [HttpGet("Group/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            Group group = await _unitOfWork.GroupRepository.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Group/Create
        [HttpGet("Group/Create")]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_courses, "Id", "Name");
            return View();
        }

        // POST: Group/Create
        [HttpPost("Group/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CourseId")] Group group)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GroupRepository.Insert(group);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_courses, "Id", "Name", group.CourseId);
            return View(group);
        }

        // GET: Group/Edit/5
        [HttpGet("Group/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_courses, "Id", "Name", group.CourseId);
            return View(group);
        }

        // POST: Group/Edit/5
        [HttpPost("Group/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CourseId")] Group group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.GroupRepository.Update(group);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_courses, "Id", "Name", group.CourseId);
            return View(group);
        }

        // GET: Group/Delete/5
        [HttpGet("Group/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpPost("Group/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _unitOfWork.GroupRepository.GetByIdAsync(id);

            if (group.Students.Count > 0)
            {
                TempData["error"] = $"Cannot delete group {group.Name} because it contains students";
            }
            else
            {
                _unitOfWork.GroupRepository.Delete(group);
                await _unitOfWork.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
