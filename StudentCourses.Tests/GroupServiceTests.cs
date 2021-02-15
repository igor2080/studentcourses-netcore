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
    public class GroupServiceTests
    {
        private GroupService _service;
        private readonly Mock<IRepository<Group>> _repository = new Mock<IRepository<Group>>();
        private Course _testCourse;

        [TestInitialize]
        public void GroupTestInit()
        {
            _service = new GroupService(_repository.Object);
            _testCourse = new Course { Id = 1, Description = "description", Name = "name" };
        }


        [TestMethod]
        public void Service_CreateValidModel()
        {
            _repository.Setup(x => x.Add(It.IsAny<Group>())).Returns(new Group() { Name = "name", Course = _testCourse }).Verifiable();
            GroupModel model = new GroupModel { Name = "name", Course = _testCourse.ToModel() };

            _service.Create(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_CreateInvalidModelthrows()
        {
            _repository.Setup(x => x.Add(It.IsAny<Group>())).Returns(new Group() { Name = "name", Course = _testCourse });

            _service.Create(null);
        }

        [TestMethod]
        public void Service_DeleteValidModel()
        {
            _repository.Setup(x => x.Delete(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Verifiable();
            GroupModel model = new GroupModel { Id = 5, Name = "name", Course = _testCourse.ToModel() };

            _service.Delete(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_DeleteInvalidModelthrows()
        {
            _repository.Setup(x => x.Add(It.IsAny<Group>())).Returns(new Group() { Name = "name", Course = _testCourse });

            _service.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Service_DeleteInvalidIdthrows()
        {
            _repository.Setup(x => x.Delete(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive)));
            GroupModel model = new GroupModel { Id = 0, Name = "name", Course = new CourseModel() };

            _service.Delete(model);
        }

        [TestMethod]
        public void Service_GetValidId()
        {
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Group() { Name = "name", Course = _testCourse }).Verifiable();

            _service.Get(5);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Service_GetInvalidId()
        {
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Group() { Name = "name", Course = _testCourse });

            _service.Get(0);
        }

        [TestMethod]
        public void Service_GetAllReturns()
        {
            _repository.Setup(x => x.GetAll()).Returns(new Group[10]).Verifiable();

            _service.GetAll();

            _repository.Verify();
        }

        [TestMethod]
        public void Service_UpdateValidModelEntity()
        {
            _repository.Setup(x => x.Update(It.IsAny<Group>())).Returns(new Group() { Name = "name", Course = _testCourse }).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Group() { Name = "name", Course = _testCourse }).Verifiable();
            GroupModel model = new GroupModel { Id = 5, Name = "name", Course = new CourseModel() };

            _service.Update(model);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Service_UpdateInvalidModel()
        {
            _repository.Setup(x => x.Update(It.IsAny<Group>())).Returns(new Group() { Name = "name", Course = _testCourse }).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns(new Group() { Name = "name", Course = _testCourse }).Verifiable();

            _service.Update(null);

            _repository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Service_UpdateInvalidEntity()
        {
            _repository.Setup(x => x.Update(It.IsAny<Group>())).Returns(new Group() { Name = "name", Course = _testCourse }).Verifiable();
            _repository.Setup(x => x.Get(It.IsInRange<int>(0, int.MaxValue, Moq.Range.Exclusive))).Returns<Group>(null).Verifiable();
            GroupModel model = new GroupModel { Id = 5, Name = "name", Course = new CourseModel() };

            _service.Update(model);

            _repository.Verify();
        }
    }
}
