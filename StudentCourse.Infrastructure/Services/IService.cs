using StudentCourses.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCourse.Infrastructure.Services
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T model);
        T Update(T model);
        void Delete(T model);
        void Delete(int id);
    }
}
