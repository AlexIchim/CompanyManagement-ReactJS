using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager;
using Manager.InfoModels;
using Manager.InputInfoModels;
using Manager.Services;
using Moq;
using NUnit.Framework;

namespace ManagementApp.Manager.Tests
{
    [TestFixture]
    public class AllocationServiceTests
    {
        private AllocationService _allocationService;
        private Mock<IAllocationRepository> _allocationRepositoryMock;
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _allocationRepositoryMock = new Mock<IAllocationRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _allocationService = new AllocationService(_mapperMock.Object, _allocationRepositoryMock.Object, _projectRepositoryMock.Object, _employeeRepositoryMock.Object);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var allocationInputInfo = new AddAllocationInputInfo { ProjectId = 1, EmployeeId = 1, AllocationPercentage = 10 };
            var allocation = new ProjectAllocation() { Id = 100, ProjectId = 1, EmployeeId = 1, AllocationPercentage = 10 };

            var allocationList = new List<ProjectAllocation> {
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 2, AllocationPercentage = 30 },
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 3, AllocationPercentage = 30 }
            };

            _mapperMock.Setup(m => m.Map<ProjectAllocation>(allocationInputInfo)).Returns(allocation);
            _allocationRepositoryMock.Setup(m => m.Add(allocation));
            _projectRepositoryMock.Setup(m => m.GetById(allocationInputInfo.ProjectId)).Returns(new Project()
            {
                Id = 1,
                Name = "Sample Project"
            });

            _employeeRepositoryMock.Setup(m => m.GetById(1)).Returns(new Employee()
            {
                Id = 1,
                Name = "Ion",
                ProjectAllocations = allocationList
            });

            //Act
            var result = _allocationService.Add(allocationInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedAllocation, result.Message);
        }

        [Test]
        public void Add_AddsAllocation()
        {
            //Arrange
            var allocationInputInfo = new AddAllocationInputInfo { ProjectId = 1, EmployeeId = 1, AllocationPercentage = 10 };
            var allocation = new ProjectAllocation() { Id = 100, ProjectId = 1, EmployeeId = 1, AllocationPercentage = 10 };

            var allocationList = new List<ProjectAllocation> {
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 2, AllocationPercentage = 30 },
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 3, AllocationPercentage = 30 }
            };

            _mapperMock.Setup(m => m.Map<ProjectAllocation>(allocationInputInfo)).Returns(allocation);
            _projectRepositoryMock.Setup(m => m.GetById(allocationInputInfo.ProjectId)).Returns(new Project()
            {
                Id = 1,
                Name = "Sample Project"
            });

            _employeeRepositoryMock.Setup(m => m.GetById(allocationInputInfo.EmployeeId)).Returns(new Employee()
            {
                Id = 1,
                Name = "Ion",
                ProjectAllocations = allocationList
            });

            //Act
            var result = _allocationService.Add(allocationInputInfo);

            //Assert
            Assert.AreEqual(3, allocationList.Count());
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenEmployeeIdInvalid()
        {
            //Arrange
            var allocationInputInfo = new AddAllocationInputInfo { ProjectId = 1, EmployeeId = 123, AllocationPercentage = 10 };

            _employeeRepositoryMock.Setup(m => m.GetById(allocationInputInfo.EmployeeId)).Returns((Employee)null);
            _projectRepositoryMock.Setup(m => m.GetById(allocationInputInfo.ProjectId)).Returns(new Project()
            {
                Id = 1,
                Name = "Sample Project"
            });

            //Act
            var result = _allocationService.Add(allocationInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingAllocation + " " + Messages.EmployeeIdInvalid, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenProjectIdInvalid()
        {
            //Arrange
            var allocationInputInfo = new AddAllocationInputInfo { ProjectId = 123, EmployeeId = 1, AllocationPercentage = 10 };
            var allocation = new ProjectAllocation() { Id = 100, ProjectId = 1, EmployeeId = 1, AllocationPercentage = 10 };

            var allocationList = new List<ProjectAllocation> {
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 2, AllocationPercentage = 30 },
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 3, AllocationPercentage = 30 }
            };

            _mapperMock.Setup(m => m.Map<ProjectAllocation>(allocationInputInfo)).Returns(allocation);
            _allocationRepositoryMock.Setup(m => m.Add(allocation));

            _employeeRepositoryMock.Setup(m => m.GetById(allocationInputInfo.EmployeeId)).Returns(new Employee()
            {
                Id = 1,
                Name = "Ion",
                ProjectAllocations = allocationList
            });

            //Act
            var result = _allocationService.Add(allocationInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingAllocation + " " + Messages.ProjectInvalidId, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenAlreadyAllocated()
        {
            //Arrange
            var allocationInputInfo = new AddAllocationInputInfo { ProjectId = 2, EmployeeId = 1, AllocationPercentage = 10 };
            var allocation = new ProjectAllocation() { Id = 100, ProjectId = 1, EmployeeId = 1, AllocationPercentage = 10 };

            var allocationList = new List<ProjectAllocation> {
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 2, AllocationPercentage = 30 },
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 3, AllocationPercentage = 30 }
            };

            _mapperMock.Setup(m => m.Map<ProjectAllocation>(allocationInputInfo)).Returns(allocation);
            _allocationRepositoryMock.Setup(m => m.Add(allocation));
            _projectRepositoryMock.Setup(m => m.GetById(allocationInputInfo.ProjectId)).Returns(new Project()
            {
                Id = 1,
                Name = "Sample Project"
            });
            _employeeRepositoryMock.Setup(m => m.GetById(allocationInputInfo.EmployeeId)).Returns(new Employee()
            {
                Id = 1,
                Name = "Ion",
                ProjectAllocations = allocationList
            });

            //Act
            var result = _allocationService.Add(allocationInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingAllocation + " " + Messages.EmployeeAlreadyOnProject, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenAllocationPercentageTooHigh()
        {
            //Arrange
            var allocationInputInfo = new AddAllocationInputInfo
            {
                ProjectId = 1,
                EmployeeId = 1,
                AllocationPercentage = 10
            };
            var allocation = new ProjectAllocation()
            {
                Id = 100,
                ProjectId = 1,
                EmployeeId = 1,
                AllocationPercentage = 10
            };

            var allocationList = new List<ProjectAllocation>
            {
                new ProjectAllocation() {EmployeeId = 1, ProjectId = 2, AllocationPercentage = 50},
                new ProjectAllocation() {EmployeeId = 1, ProjectId = 3, AllocationPercentage = 50}
            };

            _mapperMock.Setup(m => m.Map<ProjectAllocation>(allocationInputInfo)).Returns(allocation);
            _allocationRepositoryMock.Setup(m => m.Add(allocation));
            _projectRepositoryMock.Setup(m => m.GetById(allocationInputInfo.ProjectId)).Returns(new Project()
            {
                Id = 1,
                Name = "Sample Project"
            });

            _employeeRepositoryMock.Setup(m => m.GetById(allocationInputInfo.EmployeeId)).Returns(new Employee()
            {
                Id = 1,
                Name = "Ion",
                ProjectAllocations = allocationList
            });

            //Act
            var result = _allocationService.Add(allocationInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingAllocation + " " + Messages.EmployeeFreeTimeNotEnough, result.Message);
        }

        [Test]
        public void Update_ReturnsSuccessfulMessage()
        {
            //Arrange
            var allocationInputInfo = new UpdateAllocationInputInfo() { Id = 1, AllocationPercentage = 10 };

            var allocationList = new List<ProjectAllocation> {
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 2, AllocationPercentage = 30 },
                new ProjectAllocation() { EmployeeId = 1, ProjectId = 3, AllocationPercentage = 30 }
            };


            _allocationRepositoryMock.Setup(m => m.GetById(allocationInputInfo.Id)).Returns(new ProjectAllocation()
            {
                Id = 1,
                ProjectId = 1,
                EmployeeId = 1,
                AllocationPercentage = 50
            });

            _projectRepositoryMock.Setup(m => m.GetById(1)).Returns(new Project()
            {
                Id = 1,
                Name = "Sample Project"
            });

            _employeeRepositoryMock.Setup(m => m.GetById(1)).Returns(new Employee()
            {
                Id = 1,
                Name = "Ion",
                ProjectAllocations = allocationList
            });

            //Act
            var result = _allocationService.Update(allocationInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedAllocation, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenPercentageTooHigh()
        {
            //Arrange
            var allocationInputInfo = new UpdateAllocationInputInfo() { Id = 1, AllocationPercentage = 90 };

            var allocationList = new List<ProjectAllocation> {
                new ProjectAllocation() { Id = 2, EmployeeId = 1, ProjectId = 2, AllocationPercentage = 40 },
                new ProjectAllocation() { Id = 3, EmployeeId = 1, ProjectId = 3, AllocationPercentage = 40 }
            };


            _allocationRepositoryMock.Setup(m => m.GetById(allocationInputInfo.Id)).Returns(new ProjectAllocation()
            {
                Id = 1,
                ProjectId = 1,
                EmployeeId = 1,
                AllocationPercentage = 10
            });

            _projectRepositoryMock.Setup(m => m.GetById(1)).Returns(new Project()
            {
                Id = 1,
                Name = "Sample Project"
            });

            _employeeRepositoryMock.Setup(m => m.GetById(1)).Returns(new Employee()
            {
                Id = 1,
                Name = "Ion",
                ProjectAllocations = allocationList
            });

            //Act
            var result = _allocationService.Update(allocationInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingAllocation + " " + Messages.EmployeeFreeTimeNotEnough, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenIdInvalid()
        {
            //Arrange
            var allocationInputInfo = new UpdateAllocationInputInfo() { Id = 1, AllocationPercentage = 90 };

            _allocationRepositoryMock.Setup(m => m.GetById(allocationInputInfo.Id)).Returns((ProjectAllocation)null);


            //Act
            var result = _allocationService.Update(allocationInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingAllocation + " " + Messages.AllocationIdInvalid, result.Message);
        }

    }
}
