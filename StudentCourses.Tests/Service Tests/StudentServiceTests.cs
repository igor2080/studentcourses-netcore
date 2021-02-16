using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentCourse.Infrastructure.Services;
using StudentCourses.Common.Models;
using StudentCourses.Common.Utils;
using StudentCourses.Domain.Entities;
using StudentCourses.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace StudentCourses.Tests
{
    [TestClass]
    public class StudentServiceTests
    {
        private StudentService _service;
        private readonly Mock<IRepository<Student>> _repository = new Mock<IRepository<Student>>();
        private Group _testGroup;
        private Course _testCourse;

        [TestInitialize]
        public void StudentTestInit()
        {
            _service = new StudentService(_repository.Object);            
            _testCourse = new Course { Id = 1, Description = "description", Name = "name" };
            _testGroup = new Group() { Name = "name", Course = _testCourse };
        }


        [TestMethod]
        public void Service_CreateValidModel()
        {
            _repository.Setup(x => x.Add(It.IsAny<Student>())).Returns(new Student() { FirstName = "name",LastName="last",Group=_testGroup}).Verifiable();
            StudentModel model = new StudentModel { FirstName="first",LastName="last",Group=_testGroup.ToModel()};

            _service.Create(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_CreateInvalidModelthrows()
        {
            _repository.Setup(x => x.Add(It.IsAny<Student>())).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup });

            _service.Create(null);
        }

        [TestMethod]
        public void Service_DeleteValidModel()
        {
            _repository.Setup(x => x.Delete(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Verifiable();
            StudentModel model = new StudentModel {Id=5, FirstName = "first", LastName = "last", Group = _testGroup.ToModel() };
            _service.Delete(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_DeleteInvalidModelthrows()
        {
            _repository.Setup(x => x.Add(It.IsAny<Student>())).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup });

            _service.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Service_DeleteInvalidIdthrows()
        {
            _repository.Setup(x => x.Delete(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive)));
            StudentModel model = new StudentModel { FirstName = "first", LastName = "last", Group = _testGroup.ToModel() };
            _service.Delete(model);
        }

        [TestMethod]
        public void Service_GetValidId()
        {
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup }).Verifiable();

            _service.Get(5);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Service_GetInvalidId()
        {
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup });

            _service.Get(0);
        }

        [TestMethod]
        public void Service_GetAllReturns()
        {
            _repository.Setup(x => x.GetAll()).Returns(new Student[10]).Verifiable();

            _service.GetAll();

            _repository.Verify();
        }

        [TestMethod]
        public void Service_UpdateValidModelEntity()
        {
            _repository.Setup(x => x.Update(It.IsAny<Student>())).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup }).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup }).Verifiable();
            StudentModel model = new StudentModel {Id=5, FirstName = "first", LastName = "last", Group = _testGroup.ToModel() };
            _service.Update(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_UpdateInvalidModel()
        {
            _repository.Setup(x => x.Update(It.IsAny<Student>())).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup }).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup }).Verifiable();

            _service.Update(null);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Service_UpdateInvalidEntity()
        {
            _repository.Setup(x => x.Update(It.IsAny<Student>())).Returns(new Student() { FirstName = "name", LastName = "last", Group = _testGroup }).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns<Student>(null).Verifiable();
            StudentModel model = new StudentModel { FirstName = "first", LastName = "last", Group = _testGroup.ToModel() };
            _service.Update(model);

            _repository.Verify();
        }
    }
}
