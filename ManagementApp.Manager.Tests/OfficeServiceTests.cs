using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
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

        private Office CreateOffice( string name, string address, string phoneNumber, byte[] image, int? id = null)
        {
            var office = new Office
            {
                Name = name,
                Address=address,
                PhoneNumber = phoneNumber,
                Image= image
            };
            if (id != null)
            {
                office.Id = (int)id;
            }
        
            return office;
        }

        private OfficeInfo CreateOfficeInfo(int id,string name, string address, string phoneNumber, byte[] image)
        {
            var officeInfo = new OfficeInfo
            {
                Id=id,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Image = image

            };
         

            return officeInfo;
        }

        private Department CreateDepartment(string name, int officeId, int? id = null)
        {
            var department = new Department
            {
                Name = name,
                OfficeId=officeId
            };
            if (id != null)
            {
                department.Id = (int)id;
            }
            return department;
        }

        private DepartmentInfo CreateDepartmentInfo(int id, string name, int officeId)
        {
            return new DepartmentInfo
            {
                Id = id,
                Name = name
            };
        }

        [Test]
        public void GetAll_ReturnsAListOfOffices()
        {
            //Arrange
            var offices = new List<Office>
            {
                CreateOffice("Tokyo","Tokyo","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
                CreateOffice("Mumbai","Mumbai","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
            };
            var officesInfo = new List<OfficeInfo>
            {
                CreateOfficeInfo(1,"Tokyo","Tokyo","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
                CreateOfficeInfo(2, "Mumbai","Mumbai","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 })
            };

            _officeRepositoryMock.Setup(m => m.GetAll()).Returns(offices);
            _mapperMock.Setup(m => m.Map<IEnumerable<OfficeInfo>>(offices)).Returns(officesInfo);

            //Act
            var result = _officeService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAllDepartmentsOfAnOffice_ReturnsAListOfDepartments()
        {

            var offices = new List<Office>
            {
                CreateOffice("Tokyo","Tokyo","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
                CreateOffice("Mumbai","Mumbai","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
            };

        }

    }
}
