using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentCourses.Domain.Context;
using StudentCourses.Domain.Entities;
using StudentCourses.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentCourses.Tests.Repository_Tests
{
    [TestClass]
    public class GroupRepositoryTests
    {
        private readonly Mock<IDbContext> _context = new Mock<IDbContext>();
        private readonly Mock<DbSet<Group>> _dbSet = new Mock<DbSet<Group>>();
        private GroupRepository _repository;
        private Course _testCourse;
        private Group _testGroup;
        private List<Group> _testGroupList;

        [TestInitialize]
        public void GroupTestInit()
        {
            _testCourse = new Course { Id = 5, Description = "description", Name = "coursename" };
            _testGroup = new Group() { Id = 5, Name = "groupname", Course = _testCourse };
            _testCourse.Groups = new List<Group> { _testGroup };
            _testGroupList = new List<Group> { _testGroup };
            _dbSet.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(_testGroupList.AsQueryable().Provider);
            _dbSet.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(_testGroupList.AsQueryable().Expression);
            _dbSet.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(_testGroupList.AsQueryable().ElementType);
            _dbSet.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(_testGroupList.GetEnumerator());
            _repository = new GroupRepository(_context.Object);
            _context.Setup(x => x.Groups).Returns(_dbSet.Object);
        }

        [TestMethod]
        public void GroupRepository_Add()
        {
            _context.Setup(x => x.Groups.Add(It.IsAny<Group>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository.Add(new Group());

            _context.Verify();
        }

        [TestMethod]
        public void GroupRepository_Delete()
        {
            _context.Setup(x => x.Groups.Remove(It.IsAny<Group>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository.Delete(5);

            _context.Verify();
        }

        [TestMethod]
        public void GroupRepository_Find()
        {
            var result = _repository.Find(x => x.Id == 5);

            Assert.AreEqual(_testGroup, result);
        }

        [TestMethod]
        public void GroupRepository_Get()
        {
            var result = _repository.Get(5);

            Assert.AreEqual(_testGroup, result);
        }

        [TestMethod]
        public void GroupRepository_GetAll()
        {
            var result = _repository.GetAll().ToList();

            Assert.AreEqual(_testGroupList.First(), result.First());
        }

        [TestMethod]
        public void GroupRepository_GetAllQueryable()
        {
            var result = _repository.GetAllQueryable().ToList();

            Assert.AreEqual(_testGroupList.First(), result.First());
        }

        [TestMethod]
        public void GroupRepository_Update()
        {
            _testGroup.Id = 7;

            _repository.Update(_testGroup);

            Assert.IsTrue(_dbSet.Object.First().Id == 7);
        }
    }
}
