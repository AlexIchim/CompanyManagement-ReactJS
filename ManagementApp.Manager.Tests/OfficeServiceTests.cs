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
    public class OfficeServiceTests
    {
        private OfficeService _officeService;
        private Mock<IOfficeRepository> _officeRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _officeRepositoryMock=new Mock<IOfficeRepository>();
            _mapperMock=new Mock<IMapper>();
            _officeService=new OfficeService(_mapperMock.Object, _officeRepositoryMock.Object);
        }

        [Test]
        public void GetAll_ReturnsListOfOffices()
        {
            //Arrange
            var offices = new List<Office>
            {
                
            };
            //Act

            //Assert
        }


        #region helpers

        private Office CreateOffice()
        {
            return null;
        }
        private OfficeInfo CreateOfficeInfo()
        {
            return null;
        }
        #endregion 
    }
}
