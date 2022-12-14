using Microsoft.EntityFrameworkCore;
using StudentCourses.Domain.Context;
using StudentCourses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StudentCourses.Domain.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly IDbContext _context;

        public StudentRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Student Add(Student entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Students.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var entity = this.Get(id);
            _context.Students.Remove(entity);
            _context.SaveChanges();
        }

        public Student Find(Expression<Func<Student, bool>> condition)
        {
            return _context.Students.Include(x => x.Group).FirstOrDefault(condition);
        }

        public Student Get(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return _context.Students.Include(x => x.Group).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.Include(x => x.Group).AsNoTracking();
        }

        public IQueryable<Student> GetAllQueryable()
        {
            return _context.Students.Include(x => x.Group);
        }

        public Student Update(Student entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Students.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
