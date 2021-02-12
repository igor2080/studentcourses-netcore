using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StudentCourses.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourses.Data
{
    public class GenericRepository<T> where T : class, IEntityWithId
    {
        private readonly StudentCourseContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(StudentCourseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await AddIncludes().ToListAsync();
        }

        private IIncludableQueryable<T,object> AddIncludes()
        {            
            switch (typeof(T))
            {
                case var x when x == typeof(Student):
                    return _dbSet.Include(s => (s as Student).Group);
                case var x when x == typeof(Group):
                    return _dbSet.Include(s => (s as Group).Course).AsNoTracking().Include(s => (s as Group).Students);
                case var x when x == typeof(Course):
                    return _dbSet.Include(s => (s as Course).Groups);
            }

            return null;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await AddIncludes().SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);           
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
