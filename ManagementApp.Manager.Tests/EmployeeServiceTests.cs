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
    public class EmployeeServiceTests
    {
        private EmployeeService _employeeService;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _employeeService = new EmployeeService(_mapperMock.Object, _employeeRepositoryMock.Object);
        }

        [Test]
        public void GetAll_ReturnsAListOfAllEmployees()
        {
            //Arrange - aranjezi ce date vrei sa iti intre, ce rezultate de pe moc sa intoarca
            var employees = new List<Employee>
            {
                CreateEmployee("Mike", 1),
                CreateEmployee("Gerard", 2)
            };
            var employeesInfo = new List<EmployeeInfo>
            {
                CreateEmployeeInfo(1, "Mike"),
                CreateEmployeeInfo(2, "Gerard")
            };

            _employeeRepositoryMock.Setup(m => m.GetAll()).Returns(employees);
            _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeInfo>>(employees)).Returns(employeesInfo);

            //Act - apeleaza metoda care vreau sa fie testata
            var result = _employeeService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAll_CallsGetAllFromRepository()
        {
            //Arrange

            //Act
            _employeeService.GetAll();

            //Assert
            _employeeRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Add_CallsAddFromRepository()
        {
            //Arrange
            var employeeInputInfo = new AddEmployeeInputInfo { Name = "Andrew" };
            var employee = new Employee { Name = "Andrew" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfo)).Returns(employee);
            _employeeRepositoryMock.Setup(m => m.Add(employee));

            //Act
            _employeeService.Add(employeeInputInfo);

            //Assert
            _employeeRepositoryMock.Verify(x => x.Add(employee), Times.Once);
        }

        [Test]
        public void GetById_CallsGetByIdFromRepository()
        {
            //Arrange
            var employee = new Employee { Id = 1, Name = "George" };
            var employeeInfo = new EmployeeInfo { Id = 1, Name = "George" };
            //Act
            _employeeService.GetById(employee.Id);

            //Assert
            _employeeRepositoryMock.Verify(x => x.GetById(employee.Id), Times.Once);
        }

        [Test]
        public void GetById_ReturnsTheCorrectEmployee()
        {
            //Arrange
            var employee = new Employee { Id = 1, Name = "George" };
            var employeeInfo = new EmployeeInfo { Id = 1, Name = "George" };

            _employeeRepositoryMock.Setup(m => m.GetById(employee.Id)).Returns(employee);
            _mapperMock.Setup(m => m.Map<EmployeeInfo>(employee)).Returns(employeeInfo);

            //Act - apeleaza metoda care vreau sa fie testata
            var result = _employeeService.GetById(employee.Id);

            //Assert
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var employeeInputInfo = new AddEmployeeInputInfo { Name = "Andrew" };
            var employee = CreateEmployee("Andrew");

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfo)).Returns(employee);
            _employeeRepositoryMock.Setup(m => m.Add(employee));

            //Act
            var result = _employeeService.Add(employeeInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedEmployee, result.Message);
        }


        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenEmployeeExists()
        {
            //Arrange
            var employeeInputInfo = new UpdateEmployeeInputInfo { Id = 1, Name = "Lily" };
            var employee = new Employee { Id = 1, Name = "Mike" };

            _employeeRepositoryMock.Setup(m => m.GetById(employeeInputInfo.Id)).Returns(employee);

            //Act
            var result = _employeeService.Update(employeeInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedEmployee, result.Message);
        }

        [Test]
        public void Update_CallsGetByIdFromRepository_WhenEmployeeExists()
        {
            //Arrange
            var employeeInputInfo = new UpdateEmployeeInputInfo { Id = 1, Name = "Lily" };
            var employee = new Employee { Id = 1, Name = "Mike" };

            _employeeRepositoryMock.Setup(m => m.GetById(employeeInputInfo.Id)).Returns(employee);

            //Act
            _employeeService.Update(employeeInputInfo);

            //Assert
            _employeeRepositoryMock.Verify(x => x.GetById(employeeInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenEmployeeExists()
        {
            //Arrange
            var employeeInputInfo = new UpdateEmployeeInputInfo { Id = 1, Name = "Lily" };
            var employee = new Employee { Id = 1, Name = "Mike" };

            _employeeRepositoryMock.Setup(m => m.GetById(employeeInputInfo.Id)).Returns(employee);

            //Act
            _employeeService.Update(employeeInputInfo);

            //Assert
            _employeeRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenEmployeeDoesNotExists()
        {
            //Arrange
            var employeeInputInfo = new UpdateEmployeeInputInfo { Id = 1, Name = "Lily" };

            _employeeRepositoryMock.Setup(m => m.GetById(employeeInputInfo.Id)).Returns((Employee)null);

            //Act
            var result = _employeeService.Update(employeeInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingEmployee, result.Message);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenEmployeeDoesNotExists()
        {
            //Arrange
            var employeeInputInfo = new UpdateEmployeeInputInfo { Id = 1, Name = "Lily" };

            _employeeRepositoryMock.Setup(m => m.GetById(employeeInputInfo.Id)).Returns((Employee)null);

            //Act
            _employeeService.Update(employeeInputInfo);

            //Assert
            _employeeRepositoryMock.Verify(x => x.Save(), Times.Never);
        }

        [Test]
        public void Delete_CallsGetByIdFromRepository()
        {
            var employee = new Employee { Id = 1, Name = "George", ReleasedDate = null };
            _employeeRepositoryMock.Setup(m => m.GetById(employee.Id)).Returns(employee);
            //_employeeRepositoryMock.Setup(m => m.Delete(employee.Id, employee.ReleasedDate));

            //Act
            _employeeService.Delete(employee.Id, DateTime.Now);

            //Assert
            _employeeRepositoryMock.Verify(m => m.GetById(employee.Id), Times.Once);
        }
        [Test]
        public void Delete_CallsDeleteFromRepository_WhenEmployeeDoesExist()
        {
            //Arrange
            var employee = new Employee { Id = 1, Name = "George", ReleasedDate = null };
            _employeeRepositoryMock.Setup(m => m.GetById(employee.Id)).Returns(employee);
            //_employeeRepositoryMock.Setup(m => m.Delete(employee.Id, employee.ReleasedDate));

            DateTime currentTime = DateTime.Now;

            //Act
            _employeeService.Delete(employee.Id, currentTime);

            //Assert
            _employeeRepositoryMock.Verify(m => m.Delete(employee.Id, currentTime), Times.Once);
        }
        [Test]
        public void Delete_DoesNotCallDeleteFromRepository_WhenEmployeeDoesNotExist()
        {
            //Arrange
            var employee = new Employee { Id = 1, Name = "George", ReleasedDate = DateTime.Now };
            _employeeRepositoryMock.Setup(m => m.GetById(employee.Id)).Returns((Employee)null);

            DateTime currentTime = DateTime.Now;
            //Act
            _employeeService.Delete(employee.Id, currentTime);

            //Assert
            _employeeRepositoryMock.Verify(m => m.Delete(employee.Id, currentTime), Times.Never);
        }

        [Test]
        public void GetRemainingAllocation_ReturnsCorrectValueFromDB()
        {
            //Arrange
            var employee = new Employee { Id = 2 };
            var assignment1 = CreateAssignment(2, 0, 20);
            var assignment2 = CreateAssignment(2, 1, 40);
            employee.Assignments.Add(assignment1);
            employee.Assignments.Add(assignment2);

            _employeeRepositoryMock.Setup(m => m.GetById(employee.Id)).Returns(employee);
            //Act 
            var result = _employeeService.GetRemainingAllocation(employee.Id);
            //Assert
            Assert.AreEqual(40, result);
        }

        [Test]
        public void GetProjects_ReturnsTheCorrectListOfProjectsForAnEmployee()
        {
            //Arrange
            var employee = new Employee { Id = 2 };

            var assignment1 = CreateAssignment(2, 0, 20);
            var assignment2 = CreateAssignment(2, 1, 40);

            var project1 = CreateProject("Bubble", 5);
            var project2 = CreateProject("Butter", 4);
            var projects = new List<Project>
            {
                project1,
                project2
            };

            var projectInfo1 = CreateProjectInfo("Bubble", 5);
            var projectInfo2 = CreateProjectInfo("Butter", 4);
            var projectsInfo = new List<ProjectInfo>
            {
                projectInfo1,
                projectInfo2
            };

            assignment1.Project = project1;
            assignment2.Project = project2;
            employee.Assignments.Add(assignment1);
            employee.Assignments.Add(assignment2);

            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectInfo>>(projects)).Returns(projectsInfo);
            _employeeRepositoryMock.Setup(m => m.GetById(employee.Id)).Returns(employee);
            //Act 
            var result = _employeeService.GetProjectsOfEmployee(employee.Id);
            //Assert
            Assert.AreEqual(projectsInfo, result);
        }

        private Assignment CreateAssignment(int employeeId, int projectId, int allocation)
        {
            var assignment = new Assignment
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
                Allocation = allocation
            };
            return assignment;
        }
        private Employee CreateEmployee(string name, int? id = null)
        {
            var employee = new Employee
            {
                Name = name
            };
            if (id != null)
            {
                employee.Id = (int)id;
            }
            return employee;
        }

        private ProjectInfo CreateProjectInfo(string name, int? id = null)
        {
            var projectInfo = new ProjectInfo
            {
                Name = name,
            };
            if (id != null)
            {
                projectInfo.Id = (int)id;
            }
            return projectInfo;
        }
        private Project CreateProject(string name, int? id = null)
        {
            var project = new Project
            {
                Name = name,
            };
            if (id != null)
            {
                project.Id = (int)id;
            }
            return project;
        }
        private EmployeeInfo CreateEmployeeInfo(int id, string name)
        {
            return new EmployeeInfo
            {
                Id = id,
                Name = name
            };
        }
    }
}
