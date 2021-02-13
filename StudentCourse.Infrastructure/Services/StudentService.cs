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
    public class StudentService : IService<StudentModel>
    {
        private readonly IRepository<Student> _repository;

        public StudentService(IRepository<Student> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public StudentModel Create(StudentModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Student entity = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                GroupId = model.Group.Id
            };

            entity = _repository.Add(entity);
            return entity.ToModel();
        }

        public void Delete(StudentModel model)
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

        public StudentModel Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Student entity = _repository.Get(id);
            return entity.ToModel();
        }

        public IEnumerable<StudentModel> GetAll()
        {
            return _repository.GetAll().Select(Mapper.ToModel);
        }

        public StudentModel Update(StudentModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Student entity = _repository.Get(model.Id);

            if (entity == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            entity.LastName = model.LastName;
            entity.FirstName = model.FirstName;
            entity.GroupId = model.Group.Id;
            entity = _repository.Update(entity);
            return entity.ToModel();
        }
    }
}
