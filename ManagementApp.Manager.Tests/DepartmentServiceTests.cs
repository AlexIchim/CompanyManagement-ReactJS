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
    public class DepartmentServiceTests
    {
        private DepartmentService _departmentService;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _mapperMock = new Mock<IMapper>();
            _departmentService = new DepartmentService(_mapperMock.Object, _departmentRepositoryMock.Object);
        }

        [Test]
        public void GetAll_ReturnsAListOfDepartments()
        {
            //Arrange
            var departments = new List<Department>
            {
                CreateDepartment("Java", 1),
                CreateDepartment(".Net", 2)
            };
            var departmentsInfo = new List<DepartmentInfo>
            {
                CreateDepartmentInfo(1, "Java"),
                CreateDepartmentInfo(2, ".Net")
            };

            _departmentRepositoryMock.Setup(m => m.GetAll()).Returns(departments);
            _mapperMock.Setup(m => m.Map<IEnumerable<DepartmentInfo>>(departments)).Returns(departmentsInfo);

            //Act
           var result = _departmentService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
        }


        //nu merge inca
        [Test]
        public void GetAllUnAllocatedEmployeesOnProject()
        {
            //Arrange
            var employees = new List<Employee>
            {
                CreateEmployee("Adi",0,1),
                CreateEmployee("Cristina",0,2),
              //  CreateEmployee("Patricia",60,3)
            };
            var employeesinfo = new List<EmployeeInfo>
            {
                CreateEmployeeInfo(1,"Adi",0),
                CreateEmployeeInfo(2,"Cristina",0),
               // CreateEmployeeInfo(3,"Patricia",60)
            };
            _departmentRepositoryMock.Setup(m => m.GetAllUnAllocatedEmployeesOnProject()).Returns(employees);
            _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeInfo>>(employees)).Returns(employeesinfo);

            //Act
            var result = _departmentService.GetAllUnAllocatedEmployeesOnProject();

            //Assert
            Assert.AreEqual(2,result.Count());

        }

        [Test]
        public void GetAll_CallsGetAllFromRepository()
        {
            //Arrange

            //Act
            _departmentService.GetAll();

            //Assert
            _departmentRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Add_CallsAddFromRepository()
        {
            //Arrange
            var departmentInputInfo = new AddDepartmentInputInfo {Name = "javascript"};
            var department = new Department { Name = "javascript" };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentRepositoryMock.Setup(m => m.Add(department));

            //Act
            _departmentService.Add(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.Add(department), Times.Once);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "javascript" };
            var department = CreateDepartment("javascript");

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentRepositoryMock.Setup(m => m.Add(department));

            //Act
            var result = _departmentService.Add(departmentInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedDepartment, result.Message);
        }


        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenDepartmentExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo {Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java" };
            
            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            var result = _departmentService.Update(departmentInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedDepartment, result.Message);
        }

        [Test]
        public void Update_CallsGetByIdFromRepository_WhenDepartmentExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java" };

            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            _departmentService.Update(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.GetById(departmentInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenDepartmentExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java" };

            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            _departmentService.Update(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenDepartmentDoesNotExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };

            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns((Department)null);

            //Act
            var result = _departmentService.Update(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingDepartment, result.Message);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenDepartmentDoesNotExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };

            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns((Department)null);

            //Act
            _departmentService.Update(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.Save(), Times.Never);
        }

        private Department CreateDepartment(string name, int? id = null)
        {
            var department =  new Department
            {
                Name = name
            };
            if (id != null)
            {
                department.Id = (int) id;
            }
            return department;
        }

        private Employee CreateEmployee(string name, int allocation, int? id = null)
        {
            var employee = new Employee
            {
                Name = name,
                TotalAllocation = allocation
            };
            if (id != null)
            {
                employee.Id = (int)id;
            }
            return employee;
        }

        private DepartmentInfo CreateDepartmentInfo(int id, string name)
        {
            return new DepartmentInfo
            {
                Id = id,
                Name = name
            };
        }

        private EmployeeInfo CreateEmployeeInfo(int id, string name,int allocation)
        {
            return new EmployeeInfo
            {
                Id = id,
                Name = name,
                TotalAllocation = allocation
            };
        }
    }
}
