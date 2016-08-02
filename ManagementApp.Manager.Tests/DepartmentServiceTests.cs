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
    /*[TestFixture]
    public class DepartmentServiceTests
    {
        private DepartmentService _departmentService;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private Mock<IOfficeRepository> _officeRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _officeRepositoryMock = new Mock<IOfficeRepository>();
            _mapperMock = new Mock<IMapper>();
            _departmentService = new DepartmentService(_mapperMock.Object, _departmentRepositoryMock.Object, _officeRepositoryMock.Object);
        }

        [Test]
        public void GetAll_ReturnsAListOfDepartments()
        {
            //Arrange
            var departments = new List<Department>
            {
                CreateDepartment("Java", 1, 1),
                CreateDepartment(".Net", 2, 1)
            };
            var departmentsInfo = new List<DepartmentInfo>
            {
                CreateDepartmentInfo(1, "Java", 1),
                CreateDepartmentInfo(2, ".Net", 2)
            };

            _departmentRepositoryMock.Setup(m => m.GetAll()).Returns(departments);
            _mapperMock.Setup(m => m.Map<IEnumerable<DepartmentInfo>>(departments)).Returns(departmentsInfo);

            //Act
           var result = _departmentService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
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
            var departmentInputInfo = new AddDepartmentInputInfo {Name = "javascript", OfficeId = 2, DepartmentManagerId = 4};
            var department = new Department { Name = "javascript", OfficeId = 2 };
            var office = new Office { Id = 2 };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentRepositoryMock.Setup(m => m.Add(department, departmentInputInfo.DepartmentManagerId));
            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);
            //Act
            _departmentService.Add(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.Add(department, departmentInputInfo.DepartmentManagerId), Times.Once);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "javascript", OfficeId = 1 };
            var department = CreateDepartment("javascript", 1);
            var office = new Office { Id = 1 };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _departmentRepositoryMock.Setup(m => m.Add(department, departmentInputInfo.DepartmentManagerId));
            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);

            //Act
            var result = _departmentService.Add(departmentInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedDepartment, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenOfficeDoesNotExist()
        {
            //Arrange
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "javascript", OfficeId = 1 };
            var department = CreateDepartment("javascript", 1);

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns((Office)null);

            //Act
            var result = _departmentService.Add(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingDepartment_OfficeIdInvalid, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenNameIsEmpty()
        {
            //Arrange
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "", OfficeId = 1 };
            var department = CreateDepartment("", 1);
            var office = new Office { Id = 1 };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);

            //Act
            var result = _departmentService.Add(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingDepartment_EmptyName, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessage_WhenNameIsTooLong()
        {
            //Arrange
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                OfficeId = 1 };
            var department = CreateDepartment("", 1);
            var office = new Office { Id = 1 };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);

            //Act
            var result = _departmentService.Add(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingDepartment_NameTooLong, result.Message);
        }

        [Test]
        public void Update_CallsGetByIdFromRepository_WhenDepartmentExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java" };
            var office = new Office { Id = 1 };

            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);
            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            _departmentService.Update(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.GetById(departmentInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsUpdateFromRepository_WhenDepartmentExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java" };
            var office = new Office { Id = 1 };

            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);
            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            _departmentService.Update(departmentInputInfo);

            //Assert
            _departmentRepositoryMock.Verify(x => x.Update(department, departmentInputInfo.DepartmentManagerId), Times.Once);
        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenDepartmentExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo {Id = 1, Name = "php" };
            var department = new Department { Id = 1, Name = "java" };
            var office = new Office { Id = 1 };

            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);
            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            var result = _departmentService.Update(departmentInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedDepartment, result.Message);
        }    

        [Test]
        public void Update_ReturnsErrorMessage_WhenDepartmentDoesNotExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php" };
            var office = new Office { Id = 1 };

            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);
            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns((Department)null);

            //Act
            var result = _departmentService.Update(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingDepartment_InvalidId, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenDepartmentNameIsEmpty()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "" };
            var department = new Department { Id = 1, Name = "" };
            var office = new Office { Id = 1 };

            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);
            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            var result = _departmentService.Update(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingDepartment_EmptyName, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenDepartmentNameIsTooLong()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" };
            var department = new Department { Id = 1, Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" };
            var office = new Office { Id = 1 };

            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns(office);
            _departmentRepositoryMock.Setup(m => m.GetById(departmentInputInfo.Id)).Returns(department);

            //Act
            var result = _departmentService.Update(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingDepartment_NameTooLong, result.Message);
        }


        [Test]
        public void Update_ReturnsErrorMessage_WhenOfficeDoesNotExists()
        {
            //Arrange
            var departmentInputInfo = new UpdateDepartmentInputInfo { Id = 1, Name = "php", OfficeId = 4 };

            _officeRepositoryMock.Setup(m => m.GetById(departmentInputInfo.OfficeId)).Returns((Office)null);

            //Act
            var result = _departmentService.Update(departmentInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingDepartment_OfficeIdInvalid, result.Message);
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

        private Department CreateDepartment(string name, int officeId, int? id = null)
        {
            var department =  new Department
            {
                Name = name,
                OfficeId = officeId
            };
            if (id != null)
            {
                department.Id = (int) id;
            }
            return department;
        }

        private DepartmentInfo CreateDepartmentInfo(int id, string name, int officeId)
        {
            return new DepartmentInfo
            {
                Id = id,
                Name = name,
                OfficeId = officeId
            };
        }
    }*/
}
