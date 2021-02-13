using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StudentCourses.Domain.Repositories
{
    public interface IRepository<T>
    {
        void Delete(int id);
        T Add(T entity);
        T Update(T entity);
        IQueryable<T> GetAllQueryable();
        IEnumerable<T> GetAll();
        T Get(int id);
        T Find(Expression<Func<T, bool>> condition);

    }
}
