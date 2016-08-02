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
        private readonly IMapper _mapper;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeAllocationInfo> GetEmployeesAllocation(int projectId)
        {
            return _projectRepository.GetEmployeesAllocation(projectId)
                .Select(proj => new EmployeeAllocationInfo
                {
                    Id = proj.EmployeeId,
                    Name = proj.Employee.Name,
                    Allocation = proj.Allocation,
                    PositionType = proj.Employee.PositionType
                }).ToList();
        }

        public IEnumerable<EmployeeInfo> GetByProjectId(int projectId)
        {
            var employees = _projectRepository.GetEmployeeByProjectId(projectId);
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
            return employeeInfos;
        }

        public OperationResult Add(AddProjectInputInfo inputInfo)
        {
            var newProject = _mapper.Map<Project>(inputInfo);
            _projectRepository.Add(newProject);
            return new OperationResult(true, Messages.SuccessfullyAddedProject);
        }
       

        public OperationResult Delete(int projectId)
        {
            Project project = _projectRepository.GetProjectById(projectId);
            IEnumerable<EmployeeProject> employeeProject = _projectRepository.GetEmployeeProjectById(projectId);
            _projectRepository.Delete(project, employeeProject);
            return new OperationResult(true, Messages.SuccessfullyDeletedProject);
        }

        
        public OperationResult UpdateProject(UpdateProjectInputInfo inputInfo)
        {
            var updatedProject = _projectRepository.GetProjectById(inputInfo.Id);

            if (updatedProject == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingProject);
            }
            //update
            updatedProject.Name = inputInfo.Name;
            updatedProject.Status = inputInfo.Status;
            if (inputInfo.Duration != null)
            {
                updatedProject.Duration = inputInfo.Duration;
            }
            //save
            _projectRepository.Save();
            //result
            return new OperationResult(true, Messages.SuccessfullyUpdatedProject);
        }

    }
}
