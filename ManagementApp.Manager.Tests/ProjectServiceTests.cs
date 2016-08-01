using System;
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
    public class ProjectServiceTests
    {
        private ProjectService _projectService;
        private Mock<IProjectRepository> _projectRepositoryMock;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void PerTestSetup()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _mapperMock = new Mock<IMapper>();
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _projectService = new ProjectService(_mapperMock.Object, _projectRepositoryMock.Object, _departmentRepositoryMock.Object);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var projectInputInfo = new AddProjectInputInfo{Name = "Project1", DepartmentId = 1, Duration  =  "3 months", Status = "In progress"};
            var project = new Project { Name = "Project1", Department = new Department(), Duration = "3 months", Status = Status.InProgress };

            _mapperMock.Setup(m => m.Map<Project>(projectInputInfo)).Returns(project);
            _projectRepositoryMock.Setup(m => m.Add(project));

            //Act
            var result = _projectService.Add(projectInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedProject, result.Message);
        }
        [Test]
        public void GetAll_ReturnsAListOfProjects()
        {
            //Arrange
            var projects = new List<Project>
            {
                CreateProject("Project1", new Department(),"2 months", Status.Finished),
                 CreateProject("Project2", new Department(),"3 months", Status.OnHold)
            };
            var projectsInfo = new List<ProjectInfo>
            {
                CreateProjectInfo("Project1", 1, "2 months", Status.Finished),
                CreateProjectInfo("Project2", 1, "3 months", Status.OnHold)
            };

            _projectRepositoryMock.Setup(m => m.GetAll()).Returns(projects);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectInfo>>(projects)).Returns(projectsInfo);

            //Act 
            var result = _projectService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
        }
        
        private Project CreateProject(string name, Department department, string duration, Status status)
        {
            var project = new Project
            {
                Name = name,
                Department = department,
                Duration = duration,
                Status = status
            };
            return project;
        }

        private ProjectInfo CreateProjectInfo(string Name, int NrMembers, string Duration, Status status)
        {
            return new ProjectInfo(Name, NrMembers, Duration, status);
        }

    }
}
