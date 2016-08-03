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

        private Office CreateOffice(string name, string address, string phoneNumber, byte[] image, int? id = null)
        {
            var office = new Office
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Image = image
            };
            if (id != null)
            {
                office.Id = (int)id;
            }

            return office;
        }

        private OfficeInfo CreateOfficeInfo(int id, string name, string address, string phoneNumber, byte[] image)
        {
            var officeInfo = new OfficeInfo
            {
                Id = id,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Image = image

            };


            return officeInfo;
        }

        //private Department CreateDepartment(string name, int officeId, int? id = null)
        //{
        //    var department = new Department
        //    {
        //        Name = name,
        //        OfficeId=officeId
        //    };
        //    if (id != null)
        //    {
        //        department.Id = (int)id;
        //    }
        //    return department;
        //}

        //private DepartmentInfo CreateDepartmentInfo(int id, string name, int officeId)
        //{
        //    return new DepartmentInfo
        //    {
        //        Id = id,
        //        Name = name
        //    };
        //}

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

            var officesInfo = new List<OfficeInfo>
            {
                CreateOfficeInfo(1,"Tokyo","Tokyo","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
                CreateOfficeInfo(2, "Mumbai","Mumbai","123",new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 })
            };



            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "java", OfficeId = 1},
                new Department { Id = 2, Name = ".net", OfficeId = 2}
            };
            _officeRepositoryMock.Setup(m => m.GetAllDepartmentsOfAnOffice(1)).Returns(departments);
            

        }

        [Test]
        public void Add_CallsAddOfficeFromRepository()
        {
            //Arrange
            var officeInputInfo = new AddOfficeInputInfo { Id = 1, Name = "Zalau", Address = "Str Alexandrescu", PhoneNumber = "0752029547", Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } };
            var office = new Office
            {
                Id = 1,
                Name = "Zalau",
                Address = "Str Alexandrescu",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            _mapperMock.Setup(m => m.Map<Office>(officeInputInfo)).Returns(office);
            _officeRepositoryMock.Setup(m => m.AddOffice(office));

            //Act
            _officeService.Add(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.AddOffice(office), Times.Once);

        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var officeInputInfo = new AddOfficeInputInfo
            {
                Id = 1,
                Name = "Zalau",
                Address = "Str Alexandrescu",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var office = CreateOffice("Zalau", "Str Alexandrescu", "0752029547",
                                new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, 1);

            _mapperMock.Setup(m => m.Map<Office>(officeInputInfo)).Returns(office);
            _officeRepositoryMock.Setup(m => m.AddOffice(office));

            //Act
            var result = _officeService.Add(officeInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedOffice, result.Message);
        }


        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenOfficeExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo
            {
                Id = 1,
                Name = "php",
                Address = "Zalau",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };
            var office = new Office
            {
                Id = 1,
                Name = "Zalau",
                Address = "Str Alexandrescu",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            _officeRepositoryMock.Setup(m => m.GetOfficeById(officeInputInfo.Id)).Returns(office);

            //Act
            var result = _officeService.UpdateOffice(officeInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedOffice, result.Message);
        }
        [Test]
        public void Update_CallsGetByIdFromRepository_WhenOfficeExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo
            {
                Id = 1,
                Name = "php",
                Address = "Zalau",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            }; ;
            var office = new Office
            {
                Id = 1,
                Name = "Zalau",
                Address = "Str Alexandrescu",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            _officeRepositoryMock.Setup(m => m.GetOfficeById(officeInputInfo.Id)).Returns(office);

            //Act
            _officeService.UpdateOffice(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.GetOfficeById(officeInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenOfficeExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo()
            {
                Id = 1,
                Name = "php",
                Address = "Zalau",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var office = new Office
            {
                Id = 1,
                Name = "Zalau",
                Address = "Str Alexandrescu",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            _officeRepositoryMock.Setup(m => m.GetOfficeById(officeInputInfo.Id)).Returns(office);

            //Act
            _officeService.UpdateOffice(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenOfficeDoesNotExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo
            {
                Id = 1,
                Name = "php",
                Address = "Zalau",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            _officeRepositoryMock.Setup(m => m.GetOfficeById(officeInputInfo.Id)).Returns((Office)null);

            //Act
            var result = _officeService.UpdateOffice(officeInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingOffice, result.Message);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenOffceDoesNotExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo
            {
                Id = 1,
                Name = "php",
                Address = "Zalau",
                PhoneNumber = "0752029547",
                Image = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            _officeRepositoryMock.Setup(m => m.GetOfficeById(officeInputInfo.Id)).Returns((Office)null);

            //Act
            _officeService.UpdateOffice(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Save(), Times.Never);
        }


    }
}
