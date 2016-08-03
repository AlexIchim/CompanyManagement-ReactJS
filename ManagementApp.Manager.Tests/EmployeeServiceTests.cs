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
using System;

namespace ManagementApp.Manager.Tests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private EmployeeService _employeeService;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private Mock<IPositionRepository> _positionRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _positionRepositoryMock = new Mock<IPositionRepository>();
            _mapperMock = new Mock<IMapper>();
            _employeeService = new EmployeeService(_mapperMock.Object, _employeeRepositoryMock.Object, _departmentRepositoryMock.Object, _positionRepositoryMock.Object);
        }

        [Test]
        public void GetAll_ReturnsAListOfEmployees()
        {
            //Arrange
            var employees = new List<Employee>
            {
                CreateEmployee("Claudiu Cretu", "claudiu.cretu@evozon.com", "An address", DateTime.Now, 8),
                CreateEmployee("Ion Popescu", "popescu.ion@evozon.com", "Another address", DateTime.Now, 6)
            };

            var employeesInfo = new List<EmployeeInfo>
            {
                CreateEmployeeInfo(1, "Claudiu Cretu", "claudiu.cretu@evozon.com", "An address", DateTime.Now, 8),
                CreateEmployeeInfo(2, "Ion Popescu", "popescu.ion@evozon.com", "Another address", DateTime.Now, 6)
            };

            _employeeRepositoryMock.Setup(m => m.GetAll()).Returns(employees);
            _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeInfo>>(employees)).Returns(employeesInfo);

            //Act
            var result = _employeeService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenEmployeeNameIsEmptyOrTooLong()
        {
            //Arrange
            var employeeInputInfoNameEmpty = new AddEmployeeInputInfo { Name = "", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 1, PositionId = 1 };
            var employeeInputInfoTooLongName = new AddEmployeeInputInfo { Name = new string('*', 101), Address = "another address", Email = "another email", EmploymentDate = DateTime.Now, EmploymentHours = 8, DepartmentId = 1, PositionId = 1 };
            var employeeNameEmpty = new Employee() { Name = "", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 1, PositionId = 1 };
            var employeeTooLongName = new Employee() { Name = new string('*', 101), Address = "another address", Email = "another email", EmploymentDate = DateTime.Now, EmploymentHours = 8, DepartmentId = 1, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoNameEmpty)).Returns(employeeNameEmpty);
            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooLongName)).Returns(employeeTooLongName);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);

            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoNameEmpty.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongName.PositionId)).Returns(position);

            //Act

            var resultNameEmpty = _employeeService.Add(employeeInputInfoNameEmpty);
            var resultTooLongName = _employeeService.Add(employeeInputInfoTooLongName);

            //Assert

            Assert.IsFalse(resultNameEmpty.Success);
            Assert.AreEqual(Messages.EmptyEmployeeName, resultNameEmpty.Message);
            Assert.IsFalse(resultTooLongName.Success);
            Assert.AreEqual(Messages.TooLongEmployeeName, resultTooLongName.Message);

        }

        [Test]
        public void Add_ReturnsSuccessfulMessage_WhenEmployeeNameIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectName = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectName = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectName)).Returns(employeeCorrectName);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectName.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectName.PositionId)).Returns(position);

            //Act
            var resultCorrectName = _employeeService.Add(employeeInputInfoCorrectName);

            //Assert
            Assert.IsTrue(resultCorrectName.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedEmployee, resultCorrectName.Message);

        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenEmployeeEmailIsTooLong()
        {
            //Arrange
            var employeeInputInfoTooLongEmail = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = "an address", Email = new string('*', 300), EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeTooLongEmail = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = new string('*', 300), EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooLongEmail)).Returns(employeeTooLongEmail);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongEmail.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongEmail.PositionId)).Returns(position);

            //Act

            var resultTooLongEmail = _employeeService.Add(employeeInputInfoTooLongEmail);

            //Assert

            Assert.IsFalse(resultTooLongEmail.Success);
            Assert.AreEqual(Messages.TooLongEmployeeEmail, resultTooLongEmail.Message);


        }

        [Test]
        public void Add_ReturnsSuccessfulMessage_WhenEmployeeEmailIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectEmail = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectEmail = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectEmail)).Returns(employeeCorrectEmail);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmail.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmail.PositionId)).Returns(position);

            //Act

            var resultCorrectEmail = _employeeService.Add(employeeInputInfoCorrectEmail);

            //Assert

            Assert.IsTrue(resultCorrectEmail.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedEmployee, resultCorrectEmail.Message);


        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenEmployeeAddressIsTooLong()
        {
            //Arrange
            var employeeInputInfoTooLongAddress = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = new string('1', 301), Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeTooLongAddress = new Employee() { Name = "Claudiu Cretu", Address = new string('1', 301), Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooLongAddress)).Returns(employeeTooLongAddress);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongAddress.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongAddress.PositionId)).Returns(position);

            //Act

            var resultTooLongAddress = _employeeService.Add(employeeInputInfoTooLongAddress);

            //Assert

            Assert.IsFalse(resultTooLongAddress.Success);
            Assert.AreEqual(Messages.TooLongEmployeeAddress, resultTooLongAddress.Message);


        }

        [Test]
        public void Add_ReturnsSuccessfulMessage_WhenEmployeeAddressIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectAddress = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectAddress = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectAddress)).Returns(employeeCorrectAddress);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectAddress.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectAddress.PositionId)).Returns(position);

            //Act

            var resultCorrectAddress = _employeeService.Add(employeeInputInfoCorrectAddress);

            //Assert

            Assert.IsTrue(resultCorrectAddress.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedEmployee, resultCorrectAddress.Message);


        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenEmployeeHasTooFewEmploymentHours()
        {
            //Arrange
            var employeeInputInfoTooFewEmploymentHours = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 0, DepartmentId = 2, PositionId = 1 };
            var employeeTooFewEmploymentHours = new Employee() { Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 0, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooFewEmploymentHours)).Returns(employeeTooFewEmploymentHours);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooFewEmploymentHours.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooFewEmploymentHours.PositionId)).Returns(position);

            //Act

            var resultTooFewEmploymentHours = _employeeService.Add(employeeInputInfoTooFewEmploymentHours);

            //Assert

            Assert.IsFalse(resultTooFewEmploymentHours.Success);
            Assert.AreEqual(Messages.TooFewEmploymentHours, resultTooFewEmploymentHours.Message);


        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenEmployeeHasTooManyEmploymentHours()
        {
            //Arrange
            var employeeInputInfoTooManyEmploymentHours = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 10, DepartmentId = 2, PositionId = 1 };
            var employeeTooManyEmploymentHours = new Employee() { Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 10, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooManyEmploymentHours)).Returns(employeeTooManyEmploymentHours);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooManyEmploymentHours.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooManyEmploymentHours.PositionId)).Returns(position);

            //Act

            var resultTooManyEmploymentHours = _employeeService.Add(employeeInputInfoTooManyEmploymentHours);

            //Assert

            Assert.IsFalse(resultTooManyEmploymentHours.Success);
            Assert.AreEqual(Messages.TooManyEmploymentHours, resultTooManyEmploymentHours.Message);


        }

        [Test]
        public void Add_ReturnsSuccessfulMessage_WhenEmployeeEmploymentHoursIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectEmploymentHours = new AddEmployeeInputInfo { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectEmploymentHours = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new AddDepartmentInputInfo() { Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new AddPositionInputInfo() { Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectEmploymentHours)).Returns(employeeCorrectEmploymentHours);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmploymentHours.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmploymentHours.PositionId)).Returns(position);

            //Act

            var resultCorrectEmploymentHours = _employeeService.Add(employeeInputInfoCorrectEmploymentHours);

            //Assert

            Assert.IsTrue(resultCorrectEmploymentHours.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedEmployee, resultCorrectEmploymentHours.Message);


        }


        [Test]
        public void Update_ReturnsErrorMessage_WhenEmployeeNameIsEmptyOrTooLong()
        {
            //Arrange
            var employeeInputInfoNameEmpty = new UpdateEmployeeInputInfo { Id = 1, Name = "", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 1, PositionId = 1 };
            var employeeInputInfoTooLongName = new UpdateEmployeeInputInfo { Id = 1, Name = new string('*', 101), Address = "another address", Email = "another email", EmploymentDate = DateTime.Now, EmploymentHours = 8, DepartmentId = 1, PositionId = 1 };
            var employeeNameEmpty = new Employee() { Name = "", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 1, PositionId = 1 };
            var employeeTooLongName = new Employee() { Name = new string('*', 101), Address = "another address", Email = "another email", EmploymentDate = DateTime.Now, EmploymentHours = 8, DepartmentId = 1, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoNameEmpty)).Returns(employeeNameEmpty);
            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooLongName)).Returns(employeeTooLongName);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);

            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoNameEmpty.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongName.PositionId)).Returns(position);

            //Act

            var resultNameEmpty = _employeeService.Update(employeeInputInfoNameEmpty);
            var resultTooLongName = _employeeService.Update(employeeInputInfoTooLongName);

            //Assert

            Assert.IsFalse(resultNameEmpty.Success);
            Assert.AreEqual(Messages.EmptyEmployeeName, resultNameEmpty.Message);
            Assert.IsFalse(resultTooLongName.Success);
            Assert.AreEqual(Messages.TooLongEmployeeName, resultTooLongName.Message);

        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenEmployeeNameIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectName = new UpdateEmployeeInputInfo { Id = 1, Name = "Claudiu Cretu", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectName = new Employee() { Id = 1, Name = "Claudiu Cretu", Address = "an address", Email = "an email", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET" };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer" };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectName)).Returns(employeeCorrectName);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectName.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectName.PositionId)).Returns(position);

            //Act
            var resultCorrectName = _employeeService.Update(employeeInputInfoCorrectName);

            //Assert
            Assert.IsTrue(resultCorrectName.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedEmployee, resultCorrectName.Message);

        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenEmployeeEmailIsTooLong()
        {
            //Arrange
            var employeeInputInfoTooLongEmail = new UpdateEmployeeInputInfo { Id = 1, Name = "Claudiu Cretu", Address = "an address", Email = new string('*', 300), EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeTooLongEmail = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = new string('*', 300), EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET" };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer" };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooLongEmail)).Returns(employeeTooLongEmail);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongEmail.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongEmail.PositionId)).Returns(position);

            //Act

            var resultTooLongEmail = _employeeService.Update(employeeInputInfoTooLongEmail);

            //Assert

            Assert.IsFalse(resultTooLongEmail.Success);
            Assert.AreEqual(Messages.TooLongEmployeeEmail, resultTooLongEmail.Message);


        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenEmployeeEmailIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectEmail = new UpdateEmployeeInputInfo { Id = 1, Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectEmail = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectEmail)).Returns(employeeCorrectEmail);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmail.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmail.PositionId)).Returns(position);

            //Act

            var resultCorrectEmail = _employeeService.Update(employeeInputInfoCorrectEmail);

            //Assert

            Assert.IsTrue(resultCorrectEmail.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedEmployee, resultCorrectEmail.Message);


        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenEmployeeAddressIsTooLong()
        {
            //Arrange
            var employeeInputInfoTooLongAddress = new UpdateEmployeeInputInfo { Name = "Claudiu Cretu", Address = new string('1', 301), Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1, Id = 1 };
            var employeeTooLongAddress = new Employee() { Name = "Claudiu Cretu", Address = new string('1', 301), Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooLongAddress)).Returns(employeeTooLongAddress);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongAddress.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooLongAddress.PositionId)).Returns(position);

            //Act

            var resultTooLongAddress = _employeeService.Update(employeeInputInfoTooLongAddress);

            //Assert

            Assert.IsFalse(resultTooLongAddress.Success);
            Assert.AreEqual(Messages.TooLongEmployeeAddress, resultTooLongAddress.Message);


        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenEmployeeAddressIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectAddress = new UpdateEmployeeInputInfo { Id = 1, Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectAddress = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectAddress)).Returns(employeeCorrectAddress);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectAddress.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectAddress.PositionId)).Returns(position);

            //Act

            var resultCorrectAddress = _employeeService.Update(employeeInputInfoCorrectAddress);

            //Assert

            Assert.IsTrue(resultCorrectAddress.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedEmployee, resultCorrectAddress.Message);


        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenEmployeeHasTooFewEmploymentHours()
        {
            //Arrange
            var employeeInputInfoTooFewEmploymentHours = new UpdateEmployeeInputInfo { Id = 1, Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 0, DepartmentId = 2, PositionId = 1 };
            var employeeTooFewEmploymentHours = new Employee() { Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 0, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooFewEmploymentHours)).Returns(employeeTooFewEmploymentHours);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooFewEmploymentHours.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooFewEmploymentHours.PositionId)).Returns(position);

            //Act

            var resultTooFewEmploymentHours = _employeeService.Update(employeeInputInfoTooFewEmploymentHours);

            //Assert

            Assert.IsFalse(resultTooFewEmploymentHours.Success);
            Assert.AreEqual(Messages.TooFewEmploymentHours, resultTooFewEmploymentHours.Message);


        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenEmployeeHasTooManyEmploymentHours()
        {
            //Arrange
            var employeeInputInfoTooManyEmploymentHours = new UpdateEmployeeInputInfo { Id = 1, Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 10, DepartmentId = 2, PositionId = 1 };
            var employeeTooManyEmploymentHours = new Employee() { Name = "Claudiu Cretu", Address = "", Email = "", EmploymentDate = DateTime.Now, EmploymentHours = 10, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoTooManyEmploymentHours)).Returns(employeeTooManyEmploymentHours);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooManyEmploymentHours.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoTooManyEmploymentHours.PositionId)).Returns(position);

            //Act

            var resultTooManyEmploymentHours = _employeeService.Update(employeeInputInfoTooManyEmploymentHours);

            //Assert

            Assert.IsFalse(resultTooManyEmploymentHours.Success);
            Assert.AreEqual(Messages.TooManyEmploymentHours, resultTooManyEmploymentHours.Message);


        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenEmployeeEmploymentHoursIsCorrect()
        {
            //Arrange
            var employeeInputInfoCorrectEmploymentHours = new UpdateEmployeeInputInfo { Id = 1, Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var employeeCorrectEmploymentHours = new Employee() { Name = "Claudiu Cretu", Address = "an address", Email = "shdgbasldasd", EmploymentDate = DateTime.Now, EmploymentHours = 6, DepartmentId = 2, PositionId = 1 };
            var department = new Department() { Name = ".NET", Id = 1 };
            var departmentInputInfo = new UpdateDepartmentInputInfo() { Id = 1, Name = ".NET" };
            var position = new Position() { Name = "Developer", Id = 1 };
            var positionInputInfo = new UpdatePositionInputInfo() { Id = 1, Name = "Developer" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeInputInfoCorrectEmploymentHours)).Returns(employeeCorrectEmploymentHours);
            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _departmentRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmploymentHours.DepartmentId)).Returns(department);
            _positionRepositoryMock.Setup(m => m.GetById(employeeInputInfoCorrectEmploymentHours.PositionId)).Returns(position);

            //Act

            var resultCorrectEmploymentHours = _employeeService.Update(employeeInputInfoCorrectEmploymentHours);

            //Assert

            Assert.IsTrue(resultCorrectEmploymentHours.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedEmployee, resultCorrectEmploymentHours.Message);


        }

        private EmployeeInfo CreateEmployeeInfo(int id, string name, string email, string address, DateTime employmentDate, int employmentHours)
        {
            return new EmployeeInfo
            {
                Id = id,
                Name = name,
                Email = email,
                Address = address,
                EmploymentDate = employmentDate,
                EmploymentHours = employmentHours
            };
        }

        private Employee CreateEmployee(string name, string email, string address, DateTime employmentDate, int employmentHours, int? id = null)
        {
            var employee = new Employee
            {
                Name = name,
                Email = email,
                Address = address,
                EmploymentDate = employmentDate,
                EmploymentHours = employmentHours
            };

            if (id != null)
            {
                employee.Id = (int)id;
            }

            return employee;
        }
    }
}

/*public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public DateTime EmploymentDate { get; set; }
        
        public int EmploymentHours { get; set; }

        // custom:
        public int TotalAllocation { get; set; }*/
