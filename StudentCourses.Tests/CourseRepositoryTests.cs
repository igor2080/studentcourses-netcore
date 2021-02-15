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
        private Mock<IDbContext> _context = new Mock<IDbContext>();
        private Mock<DbSet<Course>> _dbSet = new Mock<DbSet<Course>>();
        private CourseRepository _repository;

        [TestInitialize]
        public void CourseTestInit()
        {
            _repository = new CourseRepository(_context.Object);
        }

        [TestMethod]
        public void Repository_AddToDb()
        {
            _context.Setup(x => x.Courses.Add(It.IsAny<Course>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository.Add(new Course());

            _context.Verify();

        }

        [TestMethod]
        public void Repository_DeleteFromDb()
        {
            _context.Setup(x => x.Courses.Remove(It.IsAny<Course>())).Verifiable();
            _context.Setup(x => x.SaveChanges()).Verifiable();
            
            _repository.Delete(5);

            _context.Verify();

        }
    }
}
