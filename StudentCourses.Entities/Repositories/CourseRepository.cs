using Microsoft.EntityFrameworkCore;
using StudentCourses.Domain.Entities;
using StudentCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StudentCourses.Domain.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly IDbContext _context;

        public CourseRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Course Add(Course entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Courses.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Course entity = this.Get(id);
            _context.Courses.Remove(entity);
            _context.SaveChanges();
        }

        public Course Find(Expression<Func<Course, bool>> condition)
        {
            return _context.Courses.Include(x=>x.Groups).FirstOrDefault(condition);
        }

        public Course Get(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Course entity = _context.Courses.Include(x=>x.Groups).SingleOrDefault(x=>x.Id==id);
            return entity;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.Include(x=>x.Groups).AsNoTracking();
        }

        public IQueryable<Course> GetAllQueryable()
        {
            return _context.Courses.Include(x => x.Groups);
        }

        public Course Update(Course entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Courses.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
