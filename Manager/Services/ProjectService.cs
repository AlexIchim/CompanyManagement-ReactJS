using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System;
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
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProjectInfo> GetAll()
        {
            var projects = _projectRepository.GetAll();
            var projectInfos = _mapper.Map<IEnumerable<ProjectInfo>>(projects);

            return projectInfos;
        }

        public ProjectInfo GetById(int id)
        {
            var project = _projectRepository.GetById(id);
            var projectInfo = _mapper.Map<ProjectInfo>(project);

            return projectInfo;
        }

        public IEnumerable<ProjectEmployeeInfo> GetEmployeesByProjectId(int id, int? pageSize = null, int? pageNumber = null)
        {
            var allocations = _projectRepository.GetEmployeesByProjectId(id, pageSize, pageNumber);

            var res = allocations.Select(a => new ProjectEmployeeInfo()
            {
                AllocationId = a.Id, 
                Employee = _mapper.Map<EmployeeInfo>(a.Employee),
                Allocation = a.AllocationPercentage
            });

            return res;
        }

        public OperationResult Delete(int? id)
        {
            if (id == null)
            {
                return new OperationResult(false, Messages.ErrorWhileDeletingProject);
            }
            else
            {
                var project = _projectRepository.GetById(id.Value);

                if (project == null)
                {
                    return new OperationResult(false, Messages.ErrorWhileDeletingProject);
                }

                _projectRepository.Delete(project);
                _projectRepository.Save();
            }
            return new OperationResult(true, Messages.SuccessfullyDeletedProject);
        }

        public OperationResult Add(AddProjectInputInfo inputInfo)
        {
            var Department = _departmentRepository.GetById(inputInfo.DepartmentId);

            if (Department != null)
            {
                var result = Validators.ProjectValidator.Validate(inputInfo);
                if (result.Success == true)
                {
                    var newProject = _mapper.Map<Project>(inputInfo);
                    _projectRepository.Add(newProject);
                    return new OperationResult(true, Messages.SuccessfullyAddedProject);
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return new OperationResult(false, Messages.ErrorWhileAddingProject_DepartmentIdInvalid);
            }
        }

        public OperationResult Update(UpdateProjectInputInfo inputInfo)
        {
            var Department = _departmentRepository.GetById(inputInfo.Id);
            if (Department != null)
            {
                var project = _projectRepository.GetById(inputInfo.Id);
                if (project == null)
                {
                    return new OperationResult(false, Messages.ErrorWhileUpdatingProject_InvalidId);
                }

                var result = Validators.ProjectValidator.Validate(inputInfo);
                if (result.Success == true)
                {
                    project.Name = inputInfo.Name;
                    project.Duration = inputInfo.Duration;
                    project.Status = inputInfo.Status;
                    project.DepartmentId = inputInfo.DepartmentId;
                    _projectRepository.Save();
                    return new OperationResult(true, Messages.SuccessfullyUpdatedProject);
                }
                else return result;
            }
            else return new OperationResult(false, Messages.ErrorWhileUpdatingProject_DepartmentIdInvalid);
        }
    }
}
