using StudentCourses.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourses.Data
{
    public class UnitOfWork: IDisposable
    {
        private readonly StudentCourseContext _context;
        private GenericRepository<Group> _groupRepository;
        private GenericRepository<Course> _courseRepository;
        private GenericRepository<Student> _studentRepository;

        public GenericRepository<Group> GroupRepository
        {
            get
            {
                if(_groupRepository==null)
                {
                    _groupRepository = new GenericRepository<Group>(_context);
                }
                return _groupRepository;
            }
        }

        public GenericRepository<Course> CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new GenericRepository<Course>(_context);
                }
                return _courseRepository;
            }
        }

        public GenericRepository<Student> StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new GenericRepository<Student>(_context);
                }
                return _studentRepository;
            }
        }

        public UnitOfWork(StudentCourseContext context)
        {
            _context = context;

        }


        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
