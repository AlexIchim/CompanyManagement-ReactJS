using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Collections.Generic;
using System.Linq;

namespace Manager.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository, IDepartmentRepository departmentRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public OperationResult Add(AddProjectInputInfo infoInput)
        {
          
            Project newProject = _mapper.Map<Project>(infoInput);
            newProject.Department = _departmentRepository.GetById(infoInput.DepartmentId);
            _projectRepository.Add(newProject);

            return new OperationResult(true, Messages.SuccessfullyAddedProject);
        }

        public IEnumerable<ProjectInfo> GetAll()
        {
            var projects = _projectRepository.GetAll();
            var projectsInfo = _mapper.Map<IEnumerable<ProjectInfo>>(projects);
            return projectsInfo;
        }
        public ProjectInfo GetById(int id)
        {

            Project project = _projectRepository.GetById(id);
            var newProject = _mapper.Map<ProjectInfo>(project);
            return newProject;

        }

        public IEnumerable<AssignmentInfo> GetAllMembersFromProject(int projectId)
        {
            var assignments = _projectRepository.GetAllAssignmentsFromProject(projectId);
            var assignmentsInfo = new List<AssignmentInfo>();
            foreach (Assignment assignment in assignments)
            {
                AssignmentInfo assignmentInfo = new AssignmentInfo(assignment.Employee.Name, assignment.Employee.Position, assignment.Allocation);
                assignmentsInfo.Add(assignmentInfo);
            }
            return assignmentsInfo;
        }

        public int GetAllocationOfEmployeeFromProject(int projectId, int employeeId)
        {
            return _projectRepository.GetAllocationOfEmployeeFromProject(projectId, employeeId);
        }

        public int GetNrTeamMembers(int projectId)
        {
            return _projectRepository.GetNrTeamMembers(projectId);
        }

        public OperationResult Delete(int projectId)
        {
            Project project = _projectRepository.GetById(projectId);
            if (project == null)
            {
                return new OperationResult(false, Messages.ErrorWhileDeletingProject);
            }
            _projectRepository.Delete(project);
            return new OperationResult(true, Messages.SuccessfullyDeletedProject);
        }

        public OperationResult Update(UpdateProjectInputInfo inputInfo)
        {
            var project = _projectRepository.GetById(inputInfo.Id);
            if (project == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingProject);
            }

            project.Name = inputInfo.Name;
            project.Duration = inputInfo.Duration;
            project.Status = inputInfo.Status;

            _projectRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedProject);
        }
    }
}
