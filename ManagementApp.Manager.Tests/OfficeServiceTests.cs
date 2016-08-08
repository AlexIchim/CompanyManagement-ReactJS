﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void GetAll_ReturnsListOfOffices()
        {
            //Arrange
            var demoImg = new byte[]
            {
                0,0,0
            };
            var offices = new List<Office>
            {
                CreateOffice(0,"TestName1","TestAddress1","0700000000",demoImg),
                CreateOffice(0,"TestName2","TestAddress2","0800000000",demoImg)
            };
            var officeInfos = new List<OfficeInfo>
            {
                CreateOfficeInfo(0, "TestName1", "TestAddress1", "0700000000", demoImg),
                CreateOfficeInfo(0, "TestName2", "TestAddress2", "0800000000", demoImg)
            };

            _officeRepositoryMock.Setup(m => m.GetAll()).Returns(offices);
            _mapperMock.Setup(m => m.Map<IEnumerable<OfficeInfo>>(offices)).Returns(officeInfos);

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
            var addOfficeInputInfo = CreateOfficeAddInputInfo(
                "TestName1",
                "TestAddress1",
                "0700000000", 
                new byte[] {0,0,0}
                );
            var office = CreateOffice(null,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 }
                );

            _mapperMock.Setup(m => m.Map<Office>(addOfficeInputInfo)).Returns(office);
            _officeRepositoryMock.Setup(m => m.Add(office));

            //Act
            _officeService.Add(addOfficeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Add(office), Times.Once);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var addOfficeInputInfo = CreateOfficeAddInputInfo(
                   "TestName1",
                   "TestAddress1",
                   "0700000000",
                   new byte[] { 0, 0, 0 });
            var office = CreateOffice(null,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });

            _mapperMock.Setup(m => m.Map<Office>(addOfficeInputInfo)).Returns(office);
            _officeRepositoryMock.Setup(m => m.Add(office));
            //Act
            var result = _officeService.Add(addOfficeInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.MessageList.Count, 1);
            Assert.AreEqual(Messages.SuccessfullyAddedOffice, result.MessageList[0]);
        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenOfficeExists()
        {
            //Arrange
            var updateOfficeInputInfo = CreateOfficeUpdateInputInfo(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });
            var office = CreateOffice(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });
            var newOffice = CreateOffice(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });

            _officeRepositoryMock.Setup(m => m.GetById(updateOfficeInputInfo.Id)).Returns(office);
            _mapperMock.Setup(m => m.Map<Office>(updateOfficeInputInfo)).Returns(newOffice);
            //Act
            var result = _officeService.Update(updateOfficeInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.MessageList.Count, 1);
            Assert.AreEqual(result.MessageList[0], Messages.SuccessfullyUpdatedOffice);
        }

        [Test]
        public void Update_CallsGetByIdFromRepository_WhenOfficeExists()
        {
            //Arrange
            var updateOfficeInputInfo = CreateOfficeUpdateInputInfo(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });
            var office = CreateOffice(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });
            var newOffice = CreateOffice(
                null,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });

            _officeRepositoryMock.Setup(m => m.GetById(updateOfficeInputInfo.Id)).Returns(office);

            _mapperMock.Setup(m => m.Map<Office>(updateOfficeInputInfo)).Returns(newOffice);
            //Act
            var result = _officeService.Update(updateOfficeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.GetById(updateOfficeInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenOfficeExists()
        {
            //Arrange
            var updateOfficeInputInfo = CreateOfficeUpdateInputInfo(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });
            var office = CreateOffice(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });
            var newOffice = CreateOffice(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });


            _officeRepositoryMock.Setup(m => m.GetById(updateOfficeInputInfo.Id)).Returns(office);
            _mapperMock.Setup(m => m.Map<Office>(updateOfficeInputInfo)).Returns(newOffice);
            //Act
            var result = _officeService.Update(updateOfficeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_ReturnsErrormessage_WhenOfficeDoesNotExist()
        {
            //Arrange
            var updateOfficeInputInfo = CreateOfficeUpdateInputInfo(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });

            _officeRepositoryMock.Setup(m => m.GetById(updateOfficeInputInfo.Id)).Returns((Office)null);
            //Act
            var result = _officeService.Update(updateOfficeInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.MessageList.Count, 1);
            Assert.AreEqual(result.MessageList[0],Messages.ErrorWhileUpdatingOffice);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenOfficeDoesNotExist()
        {
            //Arrange
            var updateOfficeInputInfo = CreateOfficeUpdateInputInfo(
                1,
                "TestName1",
                "TestAddress1",
                "0700000000",
                new byte[] { 0, 0, 0 });

            _officeRepositoryMock.Setup(m => m.GetById(updateOfficeInputInfo.Id)).Returns((Office)null);
            //Act
            var result = _officeService.Update(updateOfficeInputInfo);

            //Assert
            _officeRepositoryMock.Verify(x => x.Save(), Times.Never);
        }

        #region helpers

        private Office CreateOffice(int? id, string name, string address, string phone, byte[] image)
        {
            var office = new Office()
            {
                Name = name,
                Address = address,
                Phone = phone,
                Image = image
            };
            if (id != null)
            {
                office.Id = (int)id;
            }
            return office;
        }
        private OfficeInfo CreateOfficeInfo(int id, string name, string address, string phone, byte[] image)
        {
            var officeInfo = new OfficeInfo()
            {
                Id = id,
                Name = name,
                Address = address,
                Phone = phone,
                Image = GetString(image)
            };
            return officeInfo;
        }

        private AddOfficeInputInfo CreateOfficeAddInputInfo(string name, string address, string phone, byte[] image)
        {
            var addOfficeInputInfo = new AddOfficeInputInfo
            {
                Name = name,
                Address = address,
                Phone = phone,
                Image = GetString(image)
            };
            return addOfficeInputInfo;
        }

        private UpdateOfficeInputInfo CreateOfficeUpdateInputInfo(int id, string name, string address, string phone, byte[] image)
        {
            var updateOfficeInputInfo = new UpdateOfficeInputInfo()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Address = address,
                Image = GetString(image)
            };
            return updateOfficeInputInfo;
        }

        static byte[] GetBytes(string str)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            return buffer;
        }

        static string GetString(byte[] bytes)
        {
            string s = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return s;
        }

        #endregion 
    }
}
