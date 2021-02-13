using StudentCourses.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using StudentCourses.Domain.Entities;

namespace StudentCourses.Common.Utils
{
    public static class Mapper
    {
        public static CourseModel ToModel(this Course entity)
        {
            return entity == null ? null : new CourseModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Groups=entity.Groups.Select(ToModel).ToArray()
            };
        }
        public static StudentModel ToModel(this Student entity)
        {
            return entity == null ? null : new StudentModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Group = entity.Group.ToModel()
            };
        }
        public static GroupModel ToModel(this Group entity)
        {
            return entity == null ? null : new GroupModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Course=entity.Course.ToModel(),
                Students=entity.Students.Select(ToModel).ToArray()
            };
        }
    }
}
