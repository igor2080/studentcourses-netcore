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
    public class GroupRepository : IRepository<Group>
    {
        private readonly IDbContext _context;

        public GroupRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public Group Add(Group entity)
        {
            _context.Groups.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            Group entity = this.Get(id);
            _context.Groups.Remove(entity);
            _context.SaveChanges();
        }

        public Group Find(Expression<Func<Group, bool>> condition)
        {
            return _context.Groups.Include(x => x.Course).Include(x => x.Students).FirstOrDefault(condition);
        }

        public Group Get(int id)
        {
            Group entity = _context.Groups.Include(x => x.Course).Include(x => x.Students).SingleOrDefault(x=>x.Id==id);
            return entity;
        }

        public IEnumerable<Group> GetAll()
        {
            return _context.Groups.Include(x => x.Course).Include(x => x.Students).AsNoTracking();
        }

        public IQueryable<Group> GetAllQueryable()
        {
            return _context.Groups.Include(x=>x.Course).Include(x => x.Students);
        }

        public Group Update(Group entity)
        {
            _context.Groups.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
