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
                Groups=entity.Groups.Select(x=>new GroupModel {Id=x.Id,Name=x.Name }).ToArray()
            };
        }
        public static StudentModel ToModel(this Student entity)
        {
            return entity == null ? null : new StudentModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Group = new GroupModel {Id=entity.Group.Id,Name=entity.Group.Name }
            };
        }
        public static GroupModel ToModel(this Group entity)
        {
            return entity == null ? null : new GroupModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Course=entity.Course.ToModel(),
                Students=entity.Students.Select(x=>new StudentModel {Id=x.Id,LastName=x.LastName,FirstName=x.FirstName }).ToArray()
            };
        }
    }
}
