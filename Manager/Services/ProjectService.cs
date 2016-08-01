using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.InputInfoModels;

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
            var employees = _projectRepository.GetByProjectId(projectId);
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
            return employeeInfos;
        }

        public OperationResult AddEmployeeToProject(AddEmployeeToProjectInputInfo inputInfo)
        {
            var newEp = _mapper.Map<EmployeeProject>(inputInfo);
            _projectRepository.AddEmployeeToProject(newEp);
            return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }
    }
}
