using AutoMapper;
using Contracts;
using Domain.Models;
using Manager;
using Manager.InfoModels;
using Manager.InputInfoModels;
using Manager.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;

namespace ManagementApp.Manager.Tests
{
    [TestFixture]
    public class DepartmentServiceTests
    {
        private DepartmentService _departmentService;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private Mock<IDepartmentValidator> _departmentValidatorMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _departmentValidatorMock = new Mock<IDepartmentValidator>();
            _mapperMock = new Mock<IMapper>();
            _departmentService = new DepartmentService(_mapperMock.Object, _departmentRepositoryMock.Object, _departmentValidatorMock.Object);
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

        [Test]
        public void GetAllDepartmentManagers()
        {
            //Arrange
            var employee1 = new Employee { Id = 1, Name = "Adi", PositionType = PositionType.DepartmentManager };
            var employee2 = new Employee { Id = 2, Name = "Ion", PositionType = PositionType.DepartmentManager };
            /*var departments = new List<Department>
            {
                CreateDepartment("Java", 1),
                CreateDepartment(".Net", 2)
            };*/
            var employees = new List<Employee>
            {
                employee1,
                employee2
            };
            /*departments[0].DepartmentManager = employee1;
            departments[1].DepartmentManager = employee2;*/
            var departmentsInfo = new List<DepartmentInfo>
            {
                CreateDepartmentInfo(1, "Java"),
                CreateDepartmentInfo(2, ".Net")
            };

            var employeeInfo = new List<EmployeeInfo>
            {
                CreateEmployeeInfo(1,"Adi",0),
                CreateEmployeeInfo(2,"Ion",0),
            };

            _departmentRepositoryMock.Setup(m => m.GetAllDepartmentManagers()).Returns(employees);
            _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeInfo>>(employees)).Returns(employeeInfo);

            //Act
            var result = _departmentService.GetAllDepartmentManagers();

            //Assert
            Assert.AreEqual(2, result.Count());
        }



        /* [Test]
         public void GetAllUnAllocatedEmployeesOnProject()
         {
             //Arrange
             var employees = new List<Employee>
             {
                 CreateEmployee("Adi",0,1),
                 CreateEmployee("Cristina",0,2),
                 CreateEmployee("Patricia",60,3)
             };
             var employeesinfo = new List<EmployeeInfo>
             {
                 CreateEmployeeInfo(1,"Adi",0),
                 CreateEmployeeInfo(2,"Cristina",0),
                 CreateEmployeeInfo(3,"Patricia",60)
             };

             _departmentRepositoryMock.Setup(m => m.GetAllUnAllocatedEmployeesOnProject()).Returns(employees);
             _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeInfo>>(employees)).Returns(employeesinfo);



             //Act
             var result = _departmentService.GetAllUnAllocatedEmployeesOnProject();

             //Assert
             Assert.AreEqual(3,result.Count());

         }*/

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
            var employee = new Employee { Id = 1, Name = "Adi", PositionType = PositionType.DepartmentManager };
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "javascript", DepartmentManagerId = employee.Id };
            var department = new Department { Id = 1, Name = "javascript", DepartmentManager = employee };


            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentValidatorMock.Setup(m => m.ValidateAddDepartmentInfo(departmentInputInfo)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.DepartmentWithNameExists(department.Name)).Returns(false);
            _departmentRepositoryMock.Setup(m => m.IsDepartmentManager(employee.Id)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.AddDepartment(department, employee.Id));
            

            //Act
            _departmentService.AddDepartment(departmentInputInfo);

            //Assert
           
            _departmentRepositoryMock.Verify(x => x.AddDepartment(department, employee.Id), Times.Once);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage_WhenDepartmentManagerExistsAndNameDoesent()
        {
            //Arrange
            var employee = new Employee { Id = 1, Name = "Adi", PositionType = PositionType.DepartmentManager };
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "javascript", DepartmentManagerId = employee.Id };
            var department = new Department { Id = 1, Name = "javascript", DepartmentManager = employee };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentValidatorMock.Setup(m => m.ValidateAddDepartmentInfo(departmentInputInfo)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.IsDepartmentManager(employee.Id)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.DepartmentWithNameExists(department.Name)).Returns(false);
            _departmentRepositoryMock.Setup(m => m.AddDepartment(department, employee.Id));

            //Act
            var result = _departmentService.AddDepartment(departmentInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedDepartment, result.Message);
        }

        [Test]
        public void Add_DoesentReturnSucces_WhenDepartmentManagerDoesentExist()
        {
            var employee = new Employee { Id = 1, Name = "Adi", PositionType = PositionType.DepartmentManager };
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "javascript", DepartmentManagerId = employee.Id };
            var department = new Department { Id = 1, Name = "javascript", DepartmentManager = employee };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentValidatorMock.Setup(m => m.ValidateAddDepartmentInfo(departmentInputInfo)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.DepartmentWithNameExists(department.Name)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.IsDepartmentManager(employee.Id)).Returns(false);

            //Act
            var result = _departmentService.AddDepartment(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorAddingDepartment, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenDepartmentWithNameExists()
        {
            var employee = new Employee {Id = 1,Name = "Adi",PositionType =PositionType.DepartmentManager};
            var departmentInputInfo = new AddDepartmentInputInfo
            {
                Name = "javascript",
                DepartmentManagerId = employee.Id
            };
            var department = new Department {Id = 1,Name = "javascript",DepartmentManager = employee};
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentRepositoryMock.Setup(m => m.IsDepartmentManager(employee.Id)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.DepartmentWithNameExists(department.Name)).Returns(false);

            //Act
            var result = _departmentService.AddDepartment(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorAddingDepartment,result.Message);
        }


        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenDepartmentExists()
        {
            //Arrange
            var employee = new Employee { Id = 1, Name = "Adi", PositionType = PositionType.DepartmentManager };
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java",DepartmentManager = employee};
            _departmentValidatorMock.Setup(m => m.ValidateUpdateDepartmentInfo(departmentInputInfo)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.GetEmployeeById(departmentInputInfo.DepartmentManagerId)).Returns(employee);
            _departmentRepositoryMock.Setup(m => m.GetDepartmentById(departmentInputInfo.Id)).Returns(department);

            //Act
            var result = _departmentService.UpdateDepartment(departmentInputInfo);

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

            _departmentValidatorMock.Setup(m => m.ValidateUpdateDepartmentInfo(departmentInputInfo)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.GetDepartmentById(departmentInputInfo.Id)).Returns(department);

            //Act
            _departmentService.UpdateDepartment(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.GetDepartmentById(departmentInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenDepartmentExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java" };
            _departmentValidatorMock.Setup(m => m.ValidateUpdateDepartmentInfo(departmentInputInfo)).Returns(true);
            _departmentRepositoryMock.Setup(m => m.GetDepartmentById(departmentInputInfo.Id)).Returns(department);

            //Act
            _departmentService.UpdateDepartment(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenDepartmentDoesNotExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };

            _departmentRepositoryMock.Setup(m => m.GetDepartmentById(departmentInputInfo.Id)).Returns((Department)null);

            //Act
            var result = _departmentService.UpdateDepartment(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingDepartment, result.Message);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenDepartmentDoesNotExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };

            _departmentRepositoryMock.Setup(m => m.GetDepartmentById(departmentInputInfo.Id)).Returns((Department)null);

            //Act
            _departmentService.UpdateDepartment(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.Save(), Times.Never);
        }

        private Department CreateDepartment(string name, int? id = null)
        {
            var department = new Department
            {
                Name = name
            };
            if (id != null)
            {
                department.Id = (int)id;
            }
            return department;
        }

        private Employee CreateEmployee(string name, int? id = null)
        {
            var employee = new Employee
            {
                Name = name,
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

        private EmployeeInfo CreateEmployeeInfo(int id, string name, int allocation)
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
