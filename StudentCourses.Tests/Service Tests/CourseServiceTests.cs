using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentCourse.Infrastructure.Services;
using StudentCourses.Common.Models;
using StudentCourses.Domain.Entities;
using StudentCourses.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace StudentCourses.Tests
{
    [TestClass]
    public class CourseServiceTests
    {
        private CourseService _service;
        private readonly Mock<IRepository<Course>> _repository = new Mock<IRepository<Course>>();

        [TestInitialize]
        public void CourseTestInit()
        {
            _service = new CourseService(_repository.Object);
        }

        [TestMethod]
        public void Service_CreateValidModel()
        {
            _repository.Setup(x => x.Add(It.IsAny<Course>())).Returns(new Course()).Verifiable();
            CourseModel model = new CourseModel { Name = "name", Description = "description" };

            _service.Create(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_CreateInvalidModelthrows()
        {
            _repository.Setup(x => x.Add(It.IsAny<Course>())).Returns(new Course());

            _service.Create(null);
        }

        [TestMethod]
        public void Service_DeleteValidModel()
        {
            _repository.Setup(x => x.Delete(It.IsInRange<int>(0,int.MaxValue,Moq.Range.Exclusive))).Verifiable();
            CourseModel model = new CourseModel {Id=5, Name = "name", Description = "description" };

            _service.Delete(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_DeleteInvalidModelthrows()
        {
            _repository.Setup(x => x.Add(It.IsAny<Course>())).Returns(new Course());

            _service.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Service_DeleteInvalidIdthrows()
        {
            _repository.Setup(x => x.Delete(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive)));
            CourseModel model = new CourseModel { Id = 0, Name = "name", Description = "description" };

            _service.Delete(model);
        }

        [TestMethod]
        public void Service_GetValidId()
        {
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Course()).Verifiable();

            _service.Get(5);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Service_GetInvalidId()
        {
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Course());

            _service.Get(0);
        }

        [TestMethod]
        public void Service_GetAllReturns()
        {
            _repository.Setup(x => x.GetAll()).Returns(new Course[10]).Verifiable();

            _service.GetAll();

            _repository.Verify();
        }

        [TestMethod]
        public void Service_UpdateValidModelEntity()
        {
            _repository.Setup(x => x.Update(It.IsAny<Course>())).Returns(new Course()).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Course()).Verifiable();
            CourseModel model = new CourseModel { Id = 5, Name = "name", Description = "description" };

            _service.Update(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_UpdateInvalidModel()
        {
            _repository.Setup(x => x.Update(It.IsAny<Course>())).Returns(new Course()).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Course()).Verifiable();

            _service.Update(null);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Service_UpdateInvalidEntity()
        {
            _repository.Setup(x => x.Update(It.IsAny<Course>())).Returns(new Course()).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns<Course>(null).Verifiable();
            CourseModel model = new CourseModel { Id = 5, Name = "name", Description = "description" };

            _service.Update(model);

            _repository.Verify();
        }

    }
}
