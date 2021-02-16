using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentCourses.Domain;
using StudentCourses.Domain.Entities;
using StudentCourses.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentCourses.Tests.Repository_Tests
{
    [TestClass]
    public class StudentRepositoryTest
    {
        private readonly Mock<IDbContext> _context = new Mock<IDbContext>();
        private readonly Mock<DbSet<Student>> _dbSet = new Mock<DbSet<Student>>();
        private StudentRepository _repository;
        private Student _testStudent;
        private Course _testCourse;
        private Group _testGroup;
        private List<Student> _testStudentList;

        [TestInitialize]
        public void StudentTestInit()
        {
            _testCourse = new Course { Id = 5, Description = "description", Name = "coursename" };
            _testGroup = new Group() { Id = 5, Name = "groupname", Course = _testCourse };
            _testCourse.Groups = new List<Group> { _testGroup };
            _testStudent = new Student { FirstName = "firstname", LastName = "lastname", Id = 5, Group = _testGroup };
            _testStudentList = new List<Student> { _testStudent };
            _dbSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(_testStudentList.AsQueryable().Provider);
            _dbSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(_testStudentList.AsQueryable().Expression);
            _dbSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(_testStudentList.AsQueryable().ElementType);
            _dbSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(_testStudentList.GetEnumerator());
            _repository = new StudentRepository(_context.Object);
            _context.Setup(x => x.Students).Returns(_dbSet.Object);
        }

        [TestMethod]
        public void StudentRepository_Add()
        {
            _context.Setup(x => x.Students.Add(It.IsAny<Student>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository.Add(new Student());

            _context.Verify();
        }

        [TestMethod]
        public void StudentRepository_Delete()
        {
            _context.Setup(x => x.Students.Remove(It.IsAny<Student>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository.Delete(5);

            _context.Verify();
        }

        [TestMethod]
        public void StudentRepository_Find()
        {
            var result = _repository.Find(x => x.Id == 5);

            Assert.AreEqual(_testStudent, result);
        }

        [TestMethod]
        public void StudentRepository_Get()
        {
            var result = _repository.Get(5);

            Assert.AreEqual(_testStudent, result);
        }

        [TestMethod]
        public void StudentRepository_GetAll()
        {
            var result = _repository.GetAll().ToList();

            Assert.AreEqual(_testStudentList.First(), result.First());
        }

        [TestMethod]
        public void StudentRepository_GetAllQueryable()
        {
            var result = _repository.GetAllQueryable().ToList();

            Assert.AreEqual(_testStudentList.First(), result.First());
        }

        [TestMethod]
        public void StudentRepository_Update()
        {
            _testStudent.Id = 7;

            _repository.Update(_testStudent);

            Assert.IsTrue(_dbSet.Object.First().Id == 7);
        }
    }
}
