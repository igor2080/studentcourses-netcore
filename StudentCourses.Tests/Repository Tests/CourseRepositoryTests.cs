using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentCourses.Domain;
using StudentCourses.Domain.Entities;
using StudentCourses.Domain.Repositories;
using StudentCourses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentCourses.Tests
{
    [TestClass]
    public class CourseRepositoryTests
    {
        private readonly Mock<IDbContext> _context = new Mock<IDbContext>();
        private readonly Mock<DbSet<Course>> _dbSet = new Mock<DbSet<Course>>();
        private CourseRepository _repository;
        private Group _testGroup;
        private Course _testCourse;
        private List<Course> _testCourseList;

        [TestInitialize]
        public void CourseTestInit()
        {
            _testCourse = new Course { Id = 5, Description = "description", Name = "coursename" };
            _testGroup = new Group() { Id = 5, Name = "groupname", Course = _testCourse };
            _testCourse.Groups = new List<Group> { _testGroup };
            _testCourseList = new List<Course>() { _testCourse };
            _dbSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(_testCourseList.AsQueryable().Provider);
            _dbSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(_testCourseList.AsQueryable().Expression);
            _dbSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(_testCourseList.AsQueryable().ElementType);
            _dbSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(_testCourseList.GetEnumerator());
            _repository = new CourseRepository(_context.Object);
            _context.Setup(x => x.Courses).Returns(_dbSet.Object);
        }

        [TestMethod]
        public void CourseRepository_Add()
        {
            _context.Setup(x => x.Courses.Add(It.IsAny<Course>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository.Add(new Course());

            _context.Verify();
        }

        [TestMethod]
        public void CourseRepository_Delete()
        {
            _context.Setup(x => x.Courses.Remove(It.IsAny<Course>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository.Delete(5);

            _context.Verify();
        }

        [TestMethod]
        public void CourseRepository_Find()
        {
            var result = _repository.Find(x => x.Id == 5);

            Assert.AreEqual(_testCourse, result);
        }

        [TestMethod]
        public void CourseRepository_Get()
        {
            var result = _repository.Get(5);

            Assert.AreEqual(_testCourse, result);
        }

        [TestMethod]
        public void CourseRepository_GetAll()
        {
            var result = _repository.GetAll().ToList();

            Assert.AreEqual(_testCourseList.First(), result.First());
        }

        [TestMethod]
        public void CourseRepository_GetAllQueryable()
        {
            var result = _repository.GetAllQueryable().ToList();

            Assert.AreEqual(_testCourseList.First(), result.First());
        }

        [TestMethod]
        public void CourseRepository_Update()
        {
            _testCourse.Id = 7;

            _repository.Update(_testCourse);

            Assert.IsTrue(_dbSet.Object.First().Id == 7);
        }
    }
}
