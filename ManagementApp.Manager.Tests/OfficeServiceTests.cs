using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InputInfoModels;
using Manager.Services;
using Moq;
using NUnit.Framework;

namespace ManagementApp.Manager.Tests
{
    [TestFixture]
    class OfficeServiceTests
    {
        private OfficeService _officeService;
        private Mock<IOfficeRepository> _officeRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _officeRepositoryMock = new Mock<IOfficeRepository>();
            _mapperMock = new Mock<IMapper>();
            _officeService = new OfficeService(_mapperMock.Object, _officeRepositoryMock.Object);
        }

        [Test]
        public void Add_CallsAddFromRepository()
        {
            //Arrange
            var departmentInputInfo = new AddDepartmentInputInfo { Name = "javascript" };
            var department = new Department { Name = "javascript" };

            _mapperMock.Setup(m => m.Map<Department>(departmentInputInfo)).Returns(department);
            _officeRepositoryMock.Setup(m => m.Add(department, 1));

            //Act
            _officeService.Add(departmentInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Add(department, 1), Times.Once);
        }

    }
}
