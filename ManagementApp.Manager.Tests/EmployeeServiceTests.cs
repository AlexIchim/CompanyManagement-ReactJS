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
            var employeeInputInfo = new AddEmployeeInputInfo {Name = "Andrew"};
            var employee = new Employee { Name = "Andrew" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfo)).Returns(employee);
            _employeeRepositoryMock.Setup(m => m.Add(employee));

            //Act
            _employeeService.Add(employeeInputInfo);

            //Assert
            _employeeRepositoryMock.Verify(x => x.Add(employee), Times.Once);
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
            var employeeInputInfo = new UpdateEmployeeInputInfo {Id = 1, Name = "Lily" };
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

        private Employee CreateEmployee(string name, int? id = null)
        {
            var employee =  new Employee
            {
                Name = name
            };
            if (id != null)
            {
                employee.Id = (int) id;
            }
            return employee;
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
