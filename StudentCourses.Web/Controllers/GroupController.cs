using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentCourse.Infrastructure.Services;
using StudentCourses.Common.Models;
using StudentCourses.Domain.Entities;

namespace StudentCourses.Controllers
{
    public class GroupController : Controller
    {
        private readonly IService<GroupModel> _groupService;
        private readonly IService<CourseModel> _courseService;
        private readonly IService<StudentModel> _studentService;

        public GroupController(IService<GroupModel> groupService, IService<CourseModel> courseService, IService<StudentModel> studentService)
        {
            _groupService = groupService ?? throw new ArgumentNullException();
            _courseService = courseService ?? throw new ArgumentNullException();
            _studentService = studentService ?? throw new ArgumentNullException();
        }

        // GET: Group
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.error = TempData["error"];
            return View(_groupService.GetAll());
        }

        // GET: Group/Details/5
        [HttpGet("Group/Details/{id}")]
        public IActionResult Details(int id)
        {
            GroupModel group = _groupService.Get(id);

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
            var courses = _courseService.GetAll();
            if (courses.Count() < 1)
            {
                TempData["error"] = $"Cannot create groups as there are no courses available";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(courses, "Id", "Name");
            return View();
        }

        // POST: Group/Create
        [HttpPost("Group/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GroupModel group)
        {
            if (ModelState.IsValid)
            {
                group.Course = _courseService.Get(group.Course.Id);
                _groupService.Create(group);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_courseService.GetAll(), "Id", "Name", group.Course.Id);
            return View(group);
        }

        // GET: Group/Edit/5
        [HttpGet("Group/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var group = _groupService.Get(id);

            if (group == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(_courseService.GetAll(), "Id", "Name", group.Course.Id);
            return View(group);
        }

        // POST: Group/Edit/5
        [HttpPost("Group/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GroupModel group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                group.Course = _courseService.Get(group.Course.Id);
                _groupService.Update(group);

                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(_courseService.GetAll(), "Id", "Name", group.Course.Id);
            return View(group);
        }

        // GET: Group/Delete/5
        [HttpGet("Group/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var group = _groupService.Get(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpPost("Group/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var group = _groupService.Get(id);

            var studentCount = _studentService.GetAll().Where(x => x.Group.Id == id).Count();

            if (studentCount > 0)
            {
                TempData["error"] = $"Cannot delete group {group.Name} because it contains students";
            }
            else
            {
                _groupService.Delete(group);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
