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
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _mapperMock = new Mock<IMapper>();
            _projectService = new ProjectService(_mapperMock.Object, _projectRepositoryMock.Object, _departmentRepositoryMock.Object);
        }


        [Test]
        public void GetAll_ReturnsAListOfProjectsIfDepartmentExists()
        {
            //Arrange
            var departments = new List<Department>
            {
                CreateDepartment(1, "Javascript"),
                CreateDepartment(2, "Java")
            };
            var departmentsInfo = new List<DepartmentInfo>
            {
               CreateDepartmentInfo(1, 1,"Jaavascript"),
            };

            var projects = new List<Project>
            {
                CreateProject("Alex Project", "In progress", 6, 1),
                CreateProject("Robi Project", "Done", 8, 1),
            };
            var projectsInfo = new List<ProjectInfo>
            {
               CreateProjectInfo(1,"Alex Project", "In progress", 6, 1),
               CreateProjectInfo(2,"Robi Project", "Done", 8, 1 )
            };

            _departmentRepositoryMock.Setup(m => m.GetAll()).Returns(departments);
            _mapperMock.Setup(m => m.Map<IEnumerable<DepartmentInfo>>(departments)).Returns(departmentsInfo);

            _projectRepositoryMock.Setup(m => m.GetAll()).Returns(projects);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProjectInfo>>(projects)).Returns(projectsInfo);


            //Act
            var result = _projectService.GetAll();

            //Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void Add_CallsAddFromRepository()
        {
            //Arrange
            var projectInputInfo = new AddProjectInputInfo { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId)).Returns(department);


            _mapperMock.Setup(m => m.Map<Project>(projectInputInfo)).Returns(project);
            _projectRepositoryMock.Setup(m => m.Add(project));

            //Act
            _projectService.Add(projectInputInfo);

            //Assert
            _projectRepositoryMock.Verify(x => x.Add(project), Times.Once);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var projectInputInfo = new AddProjectInputInfo { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            _mapperMock.Setup(m => m.Map<Project>(projectInputInfo)).Returns(project);
            _projectRepositoryMock.Setup(m => m.Add(project));

            var department = new Department {OfficeId = 1, Name = "Java"};
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId)).Returns(department);

            //Act
            var result = _projectService.Add(projectInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedProject, result.Message);
        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenProjectExists()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id=1,  Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            _mapperMock.Setup(m => m.Map<Project>(projectInputInfo)).Returns(project);
            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns(project);

            var department = new Department {OfficeId = 1, Name = "Java"};
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            //Act
            var result = _projectService.Update(projectInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedProject, result.Message);
        }

        [Test]
        public void Update_ReturnsErrorMessage_WhenInvalidId()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns((Project)null);

            //Act
            var result = _projectService.Update(projectInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingProject_InvalidId, result.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnEmptyName()
        {
            //Arrange
            var department = new Department { OfficeId = 1, Name = "Java" };

            var projectInputInfoNameEmpty = new AddProjectInputInfo { Name = "", Status = "In progress", Duration = 6, DepartmentId = 1 };
            var projectNameEmpty = new Project { Name = "", Status = "In progress", Duration = 6, DepartmentId = 1 };

            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfoNameEmpty.DepartmentId)).Returns(department);


            _mapperMock.Setup(m => m.Map<Project>(projectInputInfoNameEmpty)).Returns(projectNameEmpty);
            _projectRepositoryMock.Setup(m => m.Add(projectNameEmpty));

            //Act
            var resultNameEmpty = _projectService.Add(projectInputInfoNameEmpty);


            //Assert
            Assert.IsFalse(resultNameEmpty.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingProject + Messages.ProjectNameEmpty, resultNameEmpty.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnNameTooLong()
        {
            //Arrange
            var department = new Department { OfficeId = 1, Name = "Java" };

            var projectInputInfoNameTooLong = new AddProjectInputInfo { Name = new string('$', 101), Status = "In progress", Duration = 6, DepartmentId = 1 };
            var projectNameTooLong = new Project { Name = new string('$', 101), Status = "In progress", Duration = 6, DepartmentId = 1 };

            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfoNameTooLong.DepartmentId)).Returns(department);


            _mapperMock.Setup(m => m.Map<Project>(projectInputInfoNameTooLong)).Returns(projectNameTooLong);
            _projectRepositoryMock.Setup(m => m.Add(projectNameTooLong));

            //Act
            var resultNameTooLong = _projectService.Add(projectInputInfoNameTooLong);


            //Assert
            Assert.IsFalse(resultNameTooLong.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingProject + Messages.ProjectNameTooLong, resultNameTooLong.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnStatusNotEmpty()
        {
            //Arrange
            var department = new Department { OfficeId = 1, Name = "Java" };

            var projectInputInfoStatusEmpty = new AddProjectInputInfo { Name = "New Project", Status = "", Duration = 6, DepartmentId = 1 };
            var projectStatusEmpty = new Project { Name = "New Project", Status = "", Duration = 6, DepartmentId = 1 };      

            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfoStatusEmpty.DepartmentId)).Returns(department);


            _mapperMock.Setup(m => m.Map<Project>(projectInputInfoStatusEmpty)).Returns(projectStatusEmpty);
            _projectRepositoryMock.Setup(m => m.Add(projectStatusEmpty));

            //Act
            var resultStatusEmpty = _projectService.Add(projectInputInfoStatusEmpty);

            //Assert
            Assert.IsFalse(resultStatusEmpty.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingProject + Messages.ProjectStatusEmpty, resultStatusEmpty.Message);
        }

        [Test]
        public void Add_ReturnsErrorMessageOnStatusTooLong()
        {
            //Arrange
            var department = new Department { OfficeId = 1, Name = "Java" };
            var projectInputInfoStatusTooLong = new AddProjectInputInfo { Name = "New Project", Status = new string('$', 101), Duration = 6, DepartmentId = 1 };
            var projectStatusTooLong = new Project { Name = "New project", Status = new string('$', 101), Duration = 6, DepartmentId = 1 };

            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfoStatusTooLong.DepartmentId)).Returns(department);

            _mapperMock.Setup(m => m.Map<Project>(projectInputInfoStatusTooLong)).Returns(projectStatusTooLong);
            _projectRepositoryMock.Setup(m => m.Add(projectStatusTooLong));

            //Act
            var resultStatusTooLong = _projectService.Add(projectInputInfoStatusTooLong);

            //Assert
            Assert.IsFalse(resultStatusTooLong.Success);
            Assert.AreEqual(Messages.ErrorWhileAddingProject + Messages.ProjectStatusTooLong, resultStatusTooLong.Message);
        }


        [Test]
        public void Update_ReturnsErrorMessage_WhenNameEmpty()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = "", Status = "In progress", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "", Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns(project);

            //Act
            var result = _projectService.Update(projectInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingProject + Messages.ProjectNameEmpty, result.Message);
        }


        [Test]
        public void Update_ReturnsErrorMessage_WhenNameTooLong()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = new string('*', 101), Status = "In progress", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = new string('*', 101), Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns(project);

            //Act
            var result = _projectService.Update(projectInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingProject + Messages.ProjectNameTooLong, result.Message);
        }


        [Test]
        public void Update_ReturnsErrorMessage_WhenStatusEmpty()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = "Project", Status = "", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns(project);

            //Act
            var result = _projectService.Update(projectInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingProject + Messages.ProjectStatusEmpty, result.Message);
        }


        [Test]
        public void Update_ReturnsErrorMessage_WhenStatusTooLong()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = "Project", Status = new string('*', 101), Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "Project", Status = new string('*', 101), Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns(project);

            //Act
            var result = _projectService.Update(projectInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Messages.ErrorWhileUpdatingProject + Messages.ProjectStatusTooLong, result.Message);
        }

        [Test]
        public void Update_CallsGetByIdFromRepository_WhenProjectExists()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns(project);

            //Act
            _projectService.Update(projectInputInfo);

            //Assert
            _projectRepositoryMock.Verify(x => x.GetById(projectInputInfo.Id), Times.Once);
        }

        [Test]
        public void Update_CallsSaveFromRepository_WhenProjectExists()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };
            var project = new Project { Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns(project);

            //Act
            _projectService.Update(projectInputInfo);

            //Assert
            _projectRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenProjectDoesNotExists()
        {
            //Arrange
            var projectInputInfo = new UpdateProjectInputInfo { Id = 1, Name = "Project", Status = "In progress", Duration = 6, DepartmentId = 1 };

            var department = new Department { OfficeId = 1, Name = "Java" };
            _departmentRepositoryMock.Setup(m => m.GetById(projectInputInfo.DepartmentId.Value)).Returns(department);

            _projectRepositoryMock.Setup(m => m.GetById(projectInputInfo.Id)).Returns((Project) null);

            //Act
            _projectService.Update(projectInputInfo);

            //Assert
            _projectRepositoryMock.Verify(x => x.Save(), Times.Never);
        }


        private Project CreateProject(string name, string status, int? duration, int? departmentId)
        {
            var project = new Project
            {
                Name = name,
                Status = status,
                Duration = duration,
                DepartmentId = departmentId
            };

            return project;
        }

        private ProjectInfo CreateProjectInfo(int id, string name, string status, int? duration, int? departmentId)
        {
            return new ProjectInfo
            {
                Id = id,
                Name = name,
                Status = status,
                Duration = duration,
                DepartmentId = departmentId
            };
        }



        private Department CreateDepartment(int officeId, string name)
        {
            var department = new Department
            {
                OfficeId = officeId,
                Name = name,
            };

            return department;
        }

        private DepartmentInfo CreateDepartmentInfo(int id, int officeId, string name)
        {
            return new DepartmentInfo
            {
                Id = id,
                OfficeId = officeId,
                Name = name,
            };
        }
    }
}
