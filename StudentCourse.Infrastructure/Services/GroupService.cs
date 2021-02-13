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
    public class GroupService : IService<GroupModel>
    {
        private readonly IRepository<Group> _repository;

        public GroupService(IRepository<Group> repository)
        {
            _repository = repository;
        }

        public GroupModel Create(GroupModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Group entity = new Group
            {
                CourseId = model.Course.Id,
                Name = model.Name
            };

            entity = _repository.Add(entity);
            return entity.ToModel();
        }

        public void Delete(GroupModel model)
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

        public GroupModel Get(int id)
        {
            if (id<= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Group entity = _repository.Get(id);
            return entity.ToModel();
        }

        public IEnumerable<GroupModel> GetAll()
        {
            return _repository.GetAll().Select(Mapper.ToModel);
        }

        public GroupModel Update(GroupModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Group entity = _repository.Get(model.Id);

            if (entity == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            entity.Name = model.Name;
            entity.CourseId = model.Course.Id;

            entity = _repository.Update(entity);
            return entity.ToModel();
        }
    }
}
