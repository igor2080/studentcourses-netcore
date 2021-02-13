using StudentCourses.Common.Models;
using StudentCourses.Common.Utils;
using StudentCourses.Domain.Entities;
using StudentCourses.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentCourse.Infrastructure.Services
{
    public class CourseService : IService<CourseModel>
    {
        private readonly IRepository<Course> _repository;

        public CourseService(IRepository<Course> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public CourseModel Create(CourseModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = new Course
            {
                Description = model.Description,
                Name = model.Name
            };

            entity = _repository.Add(entity);
            return entity.ToModel();
        }

        public void Delete(CourseModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.Delete(model.Id);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            _repository.Delete(id);
        }

        public CourseModel Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Course entity = _repository.Get(id);
            return entity.ToModel();
        }

        public IEnumerable<CourseModel> GetAll()
        {
            return _repository.GetAll().Select(Mapper.ToModel);
        }

        public CourseModel Update(CourseModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Course entity = _repository.Get(model.Id);

            if (entity == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            entity.Name = model.Name;
            entity.Description = model.Description;

            entity = _repository.Update(entity);

            return entity.ToModel();
        }
    }
}
