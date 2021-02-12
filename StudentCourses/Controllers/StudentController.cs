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
    public class StudentController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IEnumerable<Group> _groups;
        private readonly IEnumerable<Course> _courses;
        private readonly IEnumerable<Student> _students;

        public StudentController(StudentCourseContext context)
        {
            _unitOfWork = new UnitOfWork(context);
            _groups = _unitOfWork.GroupRepository.GetAll();
            _courses = _unitOfWork.CourseRepository.GetAll();
            _students = _unitOfWork.StudentRepository.GetAll();
        }

        // GET: Student
        [HttpGet]
        public IActionResult Index()
        {
            return View(_students);
        }

        // GET: Student/Details/5
        [HttpGet("Student/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            Student student = await _unitOfWork.StudentRepository.GetByIdAsync(id);

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
            ViewData["GroupId"] = new SelectList(_groups, "Id", "Name");
            return View();
        }

        // POST: Student/Create        
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,GroupId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StudentRepository.Insert(student);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GroupId"] = new SelectList(_groups, "Id", "Name", student.GroupId);
            return View(student);
        }

        // GET: Student/Edit/5
        [HttpGet("Student/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Student student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = new SelectList(_groups, "Id", "Name", student.GroupId);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost("Student/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,GroupId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.StudentRepository.Update(student);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GroupId"] = new SelectList(_groups, "Id", "Name", student.GroupId);
            return View(student);
        }

        // GET: Student/Delete/5
        [HttpGet("Student/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Student student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost("Student/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Student student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            _unitOfWork.StudentRepository.Delete(student);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
