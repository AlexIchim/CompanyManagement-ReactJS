using System;
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
    public class PositionServiceTests
    {
        private PositionService _positionService;
        private Mock<IPositionRepository> _positionRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _positionRepositoryMock = new Mock<IPositionRepository>();
            _mapperMock = new Mock<IMapper>();
            _positionService = new PositionService(_mapperMock.Object, _positionRepositoryMock.Object);
        }

        [Test]
        public void GetAll_ReturnsAListOfPositions()
        {
            //Arrange
            var positions = new List<Position>
            {
                new Position() { Id = 1, Name = "Department Manager"},
                new Position() { Id = 2, Name = "Developer"},
            };
            var positionInfos = new List<PositionInfo>
            {
                new PositionInfo() { Id = 1, Name = "Department Manager"},
                new PositionInfo() { Id = 2, Name = "Developer"}
            };

            _positionRepositoryMock.Setup(m => m.GetAll()).Returns(positions);
            _mapperMock.Setup(m => m.Map<IEnumerable<PositionInfo>>(positions)).Returns(positionInfos);

            //Act
            var result = _positionService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(positionInfos, result);
        }

        [Test]
        public void GetAll_CallsGetAllFromRepository()
        {
            //Arrange

            //Act
            _positionService.GetAll();

            //Assert
            _positionRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var positionInputInfo = new AddPositionInputInfo { Name = "QA" };
            var position = new Position() { Name = "QA" };

            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _positionRepositoryMock.Setup(m => m.Add(position));

            //Act
            var result = _positionService.Add(positionInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedPosition, result.Message);
        }

        [Test]
        public void Add_CallsAddFromRepository()
        {
            //Arrange
            var positionInputInfo = new AddPositionInputInfo { Name = "QA" };
            var position = new Position { Name = "QA" };

            _mapperMock.Setup(m => m.Map<Position>(positionInputInfo)).Returns(position);
            _positionRepositoryMock.Setup(m => m.Add(position));

            //Act
            _positionService.Add(positionInputInfo);

            //Assert
            _positionRepositoryMock.Verify(x => x.Add(position), Times.Once);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnEmptyName()
        {
            //Arrange
            var positionInputInfoNameEmpty = new AddPositionInputInfo { Name = "" };
            var positionNameEmpty = new Position() { Name = "" };

            _mapperMock.Setup(m => m.Map<Position>(positionInputInfoNameEmpty)).Returns(positionNameEmpty);
            _positionRepositoryMock.Setup(m => m.Add(positionNameEmpty));

            //Act
            var resultNameEmpty = _positionService.Add(positionInputInfoNameEmpty);


            //Assert
            Assert.IsFalse(resultNameEmpty.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingPosition_EmptyName, resultNameEmpty.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnNameTooLong()
        {
            //Arrange
            var positionInputInfoNameTooLong = new AddPositionInputInfo { Name = new string('*', 101) };
            var positionNameTooLong = new Position() { Name = new string('*', 101) };

            _mapperMock.Setup(m => m.Map<Position>(positionInputInfoNameTooLong)).Returns(positionNameTooLong);
            _positionRepositoryMock.Setup(m => m.Add(positionNameTooLong));

            //Act
            var resultNameTooLong = _positionService.Add(positionInputInfoNameTooLong);


            //Assert
            Assert.IsFalse(resultNameTooLong.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingPosition_NameTooLong, resultNameTooLong.Message);
        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenPositionExists()
        {
            //Arrange
            var positionInputInfo = new UpdatePositionInputInfo { Id = 1, Name = "NewProjectName" };
            var position = new Position { Id = 1, Name = "Northern Safety" };

            _positionRepositoryMock.Setup(m => m.GetById(positionInputInfo.Id)).Returns(position);

            //Act
            var result = _positionService.Update(positionInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedPosition, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenIdInvalid()
        {
            //Arrange
            var positionInputInfo = new UpdatePositionInputInfo { Id = 123, Name = "NewProjectName" };
            Position position = null;

            _positionRepositoryMock.Setup(m => m.GetById(positionInputInfo.Id)).Returns(position);

            //Act
            var result = _positionService.Update(positionInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingPosition_InvalidId, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenNameEmpty()
        {
            //Arrange
            var positionInputInfo = new UpdatePositionInputInfo { Id = 1, Name = "" };
            var position = new Position() { Id = 1, Name = "OldName" };

            _positionRepositoryMock.Setup(m => m.GetById(positionInputInfo.Id)).Returns(position);

            //Act
            var result = _positionService.Update(positionInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingPosition_EmptyName, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenNameTooLong()
        {
            //Arrange
            var positionInputInfo = new UpdatePositionInputInfo { Id = 1, Name = new String('a', 5000) };
            var position = new Position() { Id = 1, Name = "OldName" };

            _positionRepositoryMock.Setup(m => m.GetById(positionInputInfo.Id)).Returns(position);

            //Act
            var result = _positionService.Update(positionInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingPosition_NameTooLong, result.Message);
        }

        [Test]
        public void Update_CallsGetByIdFromRepository_WhenPositionExists()
        {
            //Arrange
            var positionInputInfo = new UpdatePositionInputInfo { Id = 1, Name = "QA" };
            var position = new Position { Id = 1, Name = "Department Manager" };

            _positionRepositoryMock.Setup(m => m.GetById(positionInputInfo.Id)).Returns(position);

            //Act
            _positionService.Update(positionInputInfo);

            //Assert
            _positionRepositoryMock.Verify(x => x.GetById(positionInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenPositionExists()
        {
            //Arrange
            var positionInputInfo = new UpdatePositionInputInfo { Id = 1, Name = "QA" };
            var position = new Position { Id = 1, Name = "Department Manager" };

            _positionRepositoryMock.Setup(m => m.GetById(positionInputInfo.Id)).Returns(position);

            //Act
            _positionService.Update(positionInputInfo);

            //Assert
            _positionRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenPositionDoesNotExists()
        {
            //Arrange
            var positionInputInfo = new UpdatePositionInputInfo { Id = 1, Name = "QA" };
            Position position = null;

            _positionRepositoryMock.Setup(m => m.GetById(positionInputInfo.Id)).Returns(position);

            //Act
            _positionService.Update(positionInputInfo);

            //Assert
            _positionRepositoryMock.Verify(x => x.Save(), Times.Never);
        }

    }
}
