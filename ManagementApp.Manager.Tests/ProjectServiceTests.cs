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
            _projectService = new ProjectService(_mapperMock.Object, _projectRepositoryMock.Object,
                _departmentRepositoryMock.Object);
        }

        [Test]
        public void Add_ReturnsSuccessfulMessage()
        {
            //Arrange
            var projectInputInfo = new AddProjectInputInfo
            {
                Name = "Project1",
                DepartmentId = 1,
                Duration = "3 months",
                Status = "In progress"
            };
            var project = new Project
            {
                Name = "Project1",
                Department = new Department(),
                Duration = "3 months",
                Status = Status.InProgress
            };

            _mapperMock.Setup(m => m.Map<Project>(projectInputInfo)).Returns(project);
            _projectRepositoryMock.Setup(m => m.Add(project));

            //Act
            var result = _projectService.Add(projectInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyAddedProject, result.Message);
        }


        [Test]
        public void GetAll_CallsGetAllFromRepository()
        {
            //Act
            _projectService.GetAll();

            //Assert
            _projectRepositoryMock.Verify(x => x.GetAll(), Times.Once);
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

        [Test]
        public void GetProjectById_ReturnsProject()
        {
            //Arrange
            var project = CreateProject("Project1", new Department(), "2 months", Status.Finished);
            var projectInfo = CreateProjectInfo("Project1", 1, "2 months", Status.Finished);

            _mapperMock.Setup(m => m.Map<ProjectInfo>(project)).Returns(projectInfo);
            _projectRepositoryMock.Setup(m => m.GetById(1)).Returns(project);

            //Act
            var result = _projectService.GetById(1);

            //Assert
            Assert.AreEqual(projectInfo, result);
        }

        [Test]
        public void GetProjectById_CallsGetByIdFromRepository()
        {
            //Act
            _projectService.GetById(1);
            
            //Assert
            _projectRepositoryMock.Verify(x => x.GetById(1), Times.Once);
        }

        [Test]
        public void GetAllAssignmentsFromProject_ReturnsAssignments()
        {
            
            //Arrange
            Project project = CreateProject("Project1", new Department(), "2 months", Status.Finished,1);
            Employee employee = CreateEmployee("Employee1", "Address1",1);
            var assignment = new List<Assignment>
            {
                CreateAssignment(1, 1, employee, project, 20)
            };
            var assignmentInfo = new List<ProjectMemberInfo>
            {
                CreateAssignmentInfo("Employee1", Position.Developer, 20)
            };
            project.Assignments.Add(assignment[0]);
            employee.Assignments.Add(assignment[0]);

            _mapperMock.Setup(m => m.Map < IEnumerable<ProjectMemberInfo>>(assignment)).Returns(assignmentInfo);
            _projectRepositoryMock.Setup(m => m.GetMembersFromProject(1)).Returns(assignment);

            //Act
            var result = _projectService.GetMembersFromProject(1);

            //Assert
            CollectionAssert.AreEqual(assignmentInfo, result);

        }

        private Employee CreateEmployee(string name, string address, int?  id=null)
        {
            var employee = new Employee
            {
                Name = name,
                Address = address
            };
            if (id != null)
            {
                employee.Id = (int)id;
            }
            return employee;
        }
        private Assignment CreateAssignment(int employeeId, int projectId, Employee employee, Project project, int allocation)
        {
            return new Assignment
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
                Project = project,
                Employee = employee,
                Allocation = allocation
            };
        }
        private Project CreateProject(string name, Department department, string duration, Status status, int? id=null)
        {
            var project = new Project
            {
                Name = name,
                Department = department,
                Duration = duration,
                Status = status
            };
            if (id != null)
            {
                project.Id = (int)id;
            }
            return project;
        }

        private ProjectInfo CreateProjectInfo(string name, int nrMembers, string duration, Status status)
        {
            return new ProjectInfo
            {
                Name = name,
                NrMembers = nrMembers,
                Duration = duration,
                Status = status
            };
        }

        private ProjectMemberInfo CreateAssignmentInfo(string name, Position position, int allocation)
        {
            return new ProjectMemberInfo
            {
                Name = name,
                Position = position,
                Allocation = allocation
            };
        }
        private AddProjectInputInfo CreateProjectAddInputInfo(string name, int departmentId, string duration,
            string status)
        {
            var addProjectInputInfo = new AddProjectInputInfo
            {
                Name = name,
                DepartmentId = departmentId,
                Duration = duration,
                Status = status
            };
            return addProjectInputInfo;
        }

        [Test]
        public void Delete_CallsDeleteFromRepository_WhenProjectExists()
        {
            //Arrange
            var projectId = 1;
            var project = CreateProject("Project1", new Department(), "2 months", Status.Finished);
            _projectRepositoryMock.Setup(m => m.GetById(projectId)).Returns(project);
            _projectRepositoryMock.Setup(m => m.Delete(project));

            //Act
            _projectService.Delete(projectId);

            //Assert
            _projectRepositoryMock.Verify(m => m.Delete(project), Times.Once);
        }

        [Test]
        public void Delete_DoesNotCallDeleteFromRepository_WhenProjectDoesNotExist()
        {
            //Arrange
            var projectId = 1;
            _projectRepositoryMock.Setup(m => m.GetById(projectId)).Returns((Project)null);

            //Act
            _projectService.Delete(projectId);

            //Assert
            _projectRepositoryMock.Verify(m => m.Delete(null), Times.Never);
        }

        [Test]
        public void Update_ReturnsSuccessfulMessage_WhenProjectExists()
        {
            //Arrange
            var updateProjectInputInfo = new UpdateProjectInputInfo
            {
                Id = 1,
                Name = "NewName",
                Duration = "2 MONTHS",
                Status = Status.InProgress
            };
            var project = new Project
            {
                Id = 1,
                Name = "OldName",
                Duration = "2 MONTHS",
                Status = Status.InProgress
            };

            _projectRepositoryMock.Setup(m => m.GetById(updateProjectInputInfo.Id)).Returns(project);

            //Act
            var result = _projectService.Update(updateProjectInputInfo);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(Messages.SuccessfullyUpdatedProject, result.Message);
        }
        /* [Test]
         public void Update_CallsGetByIdFromRepository_WhenOfficeExists()
         {
             //Arrange
             var officeInputInfo = new UpdateProjectInputInfo
             {
                 Id = 1,
                 Name = "NewName",
                 Duration = "2 MONTHS",
                 Status = Status.InProgress
             };
             var project = new Project()
             {
                 Id = 1,
                 Name = "OldName",
                 Duration = "2 MONTHS",
                 Status = Status.InProgress
             };

             _projectRepositoryMock.Setup(m => m.GetById(officeInputInfo.Id)).Returns(project);

             //Act
             _projectService.Update(officeInputInfo);

             //Assert
             _departmentRepositoryMock.Verify(x => x.GetById(officeInputInfo.Id), Times.Never);
         }*/

        [Test]
        public void Update_CallsSaveFromRepository_WhenProjectExists()
        {
            //Arrange
            var updateProjectInputInfo = new UpdateProjectInputInfo
            {
                Id = 1,
                Name = "NewName",
                Duration = "2 MONTHS",
                Status = Status.InProgress
            };
            var project = new Project()
            {
                Id = 1,
                Name = "OldName",
                Duration = "2 MONTHS",
                Status = Status.InProgress
            };

            _projectRepositoryMock.Setup(m => m.GetById(updateProjectInputInfo.Id)).Returns(project);
            //Act
            var result = _projectService.Update(updateProjectInputInfo);

            //Assert
            _projectRepositoryMock.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void Update_ReturnsErrormessage_WhenProjectDoesNotExist()
        {
            //Arrange
            var updateProjectInputInfo = new UpdateProjectInputInfo
            {
                Id = 1,
                Name = "NewName",
                Duration = "2 MONTHS",
                Status = Status.InProgress
            };

            _projectRepositoryMock.Setup(m => m.GetById(updateProjectInputInfo.Id)).Returns((Project)null);
            //Act
            var result = _projectService.Update(updateProjectInputInfo);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Message, Messages.ErrorWhileUpdatingProject);
        }

        [Test]
        public void Update_DoesNotCallSaveFromRepository_WhenProjectDoesNotExist()
        {
            //Arrange
            var updateProjectInputInfo = new UpdateProjectInputInfo
            {
                Id = 1,
                Name = "NewName",
                Duration = "2 MONTHS",
                Status = Status.InProgress
            };

            _projectRepositoryMock.Setup(m => m.GetById(updateProjectInputInfo.Id)).Returns((Project)null);
            //Act
            var result = _projectService.Update(updateProjectInputInfo);

            //Assert
            _projectRepositoryMock.Verify(x => x.Save(), Times.Never);
        }
    }
}
