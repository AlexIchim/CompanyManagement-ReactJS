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
    public class OfficeServiceTests
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
        public void GetAll_ReturnsAListOfOffices()
        {
            //Arrange
            var offices = new List<Office>
            {
                CreateOffice("Cluj-Napoca", "Motilor nr 16", "4016161251", new byte[] { 0x20, 0x20, 0x20}),
                CreateOffice("London", "HighWay 13 Nr", "401616125411", new byte[] { 0x20, 0x20, 0x20})
            };
            var officesInfo = new List<OfficeInfo>
            {
                CreateOfficeInfo(1,"Cluj-Napoca", "Motilor nr 16", "4016161251", new byte[] { 0x20, 0x20, 0x20}),
                CreateOfficeInfo(2,"London", "HighWay 13 Nr", "401616125411", new byte[] { 0x20, 0x20, 0x20})
            };

            _officeRepositoryMock.Setup(m => m.GetAll()).Returns(offices);
            _mapperMock.Setup(m => m.Map<IEnumerable<OfficeInfo>>(offices)).Returns(officesInfo);

            //Act
            var result = _officeService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAll_CallsGetAllFromRepository()
        {
            //Arrange

            //Act
            _officeService.GetAll();

            //Assert
            _officeRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Add_CallsAddFromRepository()
        {
            //Arrange
            var officeInputInfo = new AddOfficeInputInfo { Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };
            var office = new Office { Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            _mapperMock.Setup(m => m.Map<Office>(officeInputInfo)).Returns(office);
            _officeRepositoryMock.Setup(m => m.Add(office));

            //Act
            _officeService.Add(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Add(office), Times.Once);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var officeInputInfo = new AddOfficeInputInfo { Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };
            var office = CreateOffice("Cluj-Napoca", "Motilor nr 16", "4016161251", new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 });

            _mapperMock.Setup(m => m.Map<Office>(officeInputInfo)).Returns(office);
            _officeRepositoryMock.Setup(m => m.Add(office));

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
            var officeInputInfo = new UpdateOfficeInputInfo { Id = 1, Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };
            var office = new Office { Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            _officeRepositoryMock.Setup(m => m.GetById(officeInputInfo.Id)).Returns(office);

            //Act
            var result = _officeService.Update(officeInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedOffice, result.Message);
        }


        [Test]
        public void Update_CallsGetByIdFromRepository_WhenOfficeExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo { Id = 1, Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };
            var office = new Office { Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            _officeRepositoryMock.Setup(m => m.GetById(officeInputInfo.Id)).Returns(office);

            //Act
            _officeService.Update(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.GetById(officeInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenOfficeExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo { Id = 1, Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };
            var office = new Office { Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            _officeRepositoryMock.Setup(m => m.GetById(officeInputInfo.Id)).Returns(office);

            //Act
            _officeService.Update(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenOfficeDoesNotExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo { Id = 1, Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            _officeRepositoryMock.Setup(m => m.GetById(officeInputInfo.Id)).Returns((Office)null);

            //Act
            var result = _officeService.Update(officeInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingOffice_InvalidId, result.Message);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenOfficeDoesNotExists()
        {
            //Arrange
            var officeInputInfo = new UpdateOfficeInputInfo { Id = 1, Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            _officeRepositoryMock.Setup(m => m.GetById(officeInputInfo.Id)).Returns((Office)null);

            //Act
            _officeService.Update(officeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Save(), Times.Never);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnInvalidNameInput()
        {
            //Arrange
            var officeInputInfoNameEmpty = new AddOfficeInputInfo { Name = "", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };
            var officeNameEmpty = new Office() {Name = "Cluj-Napoca", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            var officeInputInfoNameTooLong = new AddOfficeInputInfo { Name = new string('*', 101), Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };
            var officeNameTooLong = new Office() { Name = "", Address = "Motilor nr12", Phone = "0740179200", Image = new byte[3] };

            _mapperMock.Setup(m => m.Map<Office>(officeInputInfoNameEmpty)).Returns(officeNameEmpty);
            _mapperMock.Setup(m => m.Map<Office>(officeInputInfoNameTooLong)).Returns(officeNameTooLong);
            _officeRepositoryMock.Setup(m => m.Add(officeNameEmpty));
            _officeRepositoryMock.Setup(m => m.Add(officeNameTooLong));

            //Act
            var resultNameEmpty = _officeService.Add(officeInputInfoNameEmpty);
            var resultNameTooLong = _officeService.Add(officeInputInfoNameTooLong);


            //Assert
            Assert.IsFalse(resultNameEmpty.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingOffice_EmptyName, resultNameEmpty.Message);
            Assert.IsFalse(resultNameTooLong.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingOffice_NameTooLong, resultNameTooLong.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnInvalidAddressInput()
        {
            //Arrange
            var officeInputInfoAddressEmpty = new AddOfficeInputInfo { Name = "Tokio", Address = "", Phone = "0740179200", Image = new byte[3] };
            var officeAddressEmpty = new Office() { Name = "Cluj-Napoca", Address = "", Phone = "0740179200", Image = new byte[3] };

            var officeInputInfoAddressTooLong = new AddOfficeInputInfo { Name = "Tokio", Address = new string('*', 301), Phone = "0740179200", Image = new byte[3] };
            var officeAddressTooLong = new Office() { Name = "Cluj-Napoca", Address = new string('*', 301), Phone = "0740179200", Image = new byte[3] };

            _mapperMock.Setup(m => m.Map<Office>(officeInputInfoAddressEmpty)).Returns(officeAddressEmpty);
            _mapperMock.Setup(m => m.Map<Office>(officeInputInfoAddressTooLong)).Returns(officeAddressTooLong);
            _officeRepositoryMock.Setup(m => m.Add(officeAddressEmpty));
            _officeRepositoryMock.Setup(m => m.Add(officeAddressTooLong));

            //Act
            var resultAddressEmpty = _officeService.Add(officeInputInfoAddressEmpty);
            var resultAddressTooLong = _officeService.Add(officeInputInfoAddressTooLong);


            //Assert
            Assert.IsFalse(resultAddressEmpty.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingOffice_EmptyAddress, resultAddressEmpty.Message);
            Assert.IsFalse(resultAddressTooLong.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingOffice_AddressTooLong, resultAddressTooLong.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnInvalidPhoneInput()
        {
            //Arrange
            var officeInputInfoPhoneEmpty = new AddOfficeInputInfo { Name = "Tokio", Address = "Motilor nr1", Phone = "", Image = new byte[3] };
            var officePhoneEmpty = new Office() { Name = "Cluj-Napoca", Address = "", Phone = "0740179200", Image = new byte[3] };

            var officeInputInfoPhoneTooLong = new AddOfficeInputInfo { Name = "Tokio", Address = "Motilor nr1", Phone = new string('*', 21), Image = new byte[3] };
            var officePhoneTooLong = new Office() { Name = "Cluj-Napoca", Address = "Motilor nr1", Phone = "0740179200", Image = new byte[3] };

            _mapperMock.Setup(m => m.Map<Office>(officeInputInfoPhoneEmpty)).Returns(officePhoneEmpty);
            _mapperMock.Setup(m => m.Map<Office>(officeInputInfoPhoneTooLong)).Returns(officePhoneTooLong);
            _officeRepositoryMock.Setup(m => m.Add(officePhoneEmpty));
            _officeRepositoryMock.Setup(m => m.Add(officePhoneTooLong));

            //Act
            var resultPhoneEmpty = _officeService.Add(officeInputInfoPhoneEmpty);
            var resultPhoneTooLong = _officeService.Add(officeInputInfoPhoneTooLong);


            //Assert
            Assert.IsFalse(resultPhoneEmpty.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingOffice_EmptyPhone, resultPhoneEmpty.Message);
            Assert.IsFalse(resultPhoneTooLong.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingOffice_PhoneTooLong, resultPhoneTooLong.Message);
        }




        private Office CreateOffice(string name, string address, string phone, byte[] image)
        {
            var office = new Office
            {
                Name = name,
                Address = address,
                Phone = phone,
                Image = image
            };

            return office;
        }

        private OfficeInfo CreateOfficeInfo(int id, string name, string address, string phone, byte[] image)
        {
            return new OfficeInfo
            {
                Id = id,
                Name = name,
                Address = address,
                Phone = phone,
                Image = image
            };
        }
    }
}
