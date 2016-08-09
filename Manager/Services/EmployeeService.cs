using System;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Manager.Descriptors;

namespace Manager.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IEmployeeProjectValidator _employeeProjectValidator;
        private readonly IProjectRepository _projectRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IEmployeeValidator employeeValidator, IEmployeeProjectValidator employeeProjectValidator, IProjectRepository projectRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _employeeValidator = employeeValidator;
            _employeeProjectValidator = employeeProjectValidator;
            _projectRepository = projectRepository;
        }

        public IEnumerable<ProjectsOfAnEmployeeInfo> GetProjectByEmployeeId(int employeeId)
        {
            if (_employeeValidator.ValidateId(employeeId))
            {
                return _employeeRepository.GetProjectByEmployeeId(employeeId).
                    Select(emp => new ProjectsOfAnEmployeeInfo
                    {
                        Id = emp.ProjectId,
                        Name = emp.Project.Name,
                        Allocation = emp.Allocation
                    }).ToList();
            }
            return null;                                                                                           //?
        }

        public OperationResult ReleaseEmployee(int employeeId)
        {
            if (_employeeValidator.ValidateId(employeeId))
            {
                var employee = _employeeRepository.GetById(employeeId);

                if (employee != null)
                {
                    _employeeRepository.ReleaseEmployee(employeeId);
                    return new OperationResult(true, Messages.SuccessfullyDeletedEmployee);
                }
            }
            return new OperationResult(false, Messages.ErrorDeletingEmployee);
        }

        public OperationResult UpdateEmployee(UpdateEmployeeInputInfo inputInfo)
        {
            if (_employeeValidator.ValidateUpdateEmployeeInfo(inputInfo))
            {
                var department = _departmentRepository.GetDepartmentById(inputInfo.DepartmentId);

                if (department != null)
                {
                    var employee = _employeeRepository.GetById(inputInfo.Id);

                    if (employee != null)
                    {
                        employee.Name = inputInfo.Name;
                        employee.Address = inputInfo.Address;
                        employee.EmploymentDate = inputInfo.EmploymentDate;
                        employee.ReleaseDate = inputInfo.ReleaseDate;
                        employee.JobType = inputInfo.JobType;
                        employee.PositionType = inputInfo.PositionType;

                        _employeeRepository.Save();

                        return new OperationResult(true, Messages.SuccessfullyUpdatedEmployee);

                    }
                }
            }
            return new OperationResult(false, Messages.ErrorWhileUpdatingEmployee);
        }

        public OperationResult AddEmployeeToProject(AddEmployeeToProjectInputInfo inputInfo)
        {
            if (_employeeValidator.ValidateAddEmployeeToProjectInfo(inputInfo))
            {
                var newEp = _mapper.Map<EmployeeProject>(inputInfo);

                int totalAllocation = _employeeRepository.ComputeTotalAllocation(newEp.EmployeeId);

                int newTotalAllocation = totalAllocation + newEp.Allocation;

                if (newTotalAllocation <= 100)
                {
                    _employeeRepository.AddEmployeeToProject(newEp);
                    return new OperationResult(true, Messages.SuccessfullyAddedEmployeeToProject);
                }
            }
            return new OperationResult(false, Messages.ErrorAddingEmployeeToProject);
        }

        public OperationResult UpdatePartialAllocation(UpdateAllocationInputInfo inputInfo)
        {
            if (_employeeProjectValidator.ValidateUpdateAllocationInfo(inputInfo))
            {
                var employeeProjects = _employeeRepository.GetEmployeeProjectById(inputInfo.ProjectId).ToList();

                if (employeeProjects.Any())
                {
                    EmployeeProject ep = employeeProjects.SingleOrDefault(e => e.EmployeeId == inputInfo.EmployeeId);
                    if (ep != null)
                    {
                        int totalAllocation = _employeeRepository.ComputeTotalAllocation(ep.EmployeeId);

                        int newTotalAllocation = totalAllocation - ep.Allocation + inputInfo.Allocation;

                        if (newTotalAllocation <= 100)
                        {
                            ep.Allocation = inputInfo.Allocation;
                            _employeeRepository.Save();
                            return new OperationResult(true, Messages.SuccessfullyUpdatedPartialAllocation);
                        }
                    }
                }
            }
            return new OperationResult(false, Messages.ErrorWhileUpdatingPartialAllocation);
        }

        public IEnumerable<MemberInfo> GetAllDepartmentEmployees(int departmentId, int? pageSize, int? pageNr)
        {
            if (_employeeValidator.ValidateId(departmentId))
            {

                var department = _departmentRepository.GetDepartmentById(departmentId);

                if (department != null)
                {
                    var members = _employeeRepository.GetAllDepartmentEmployees(department, pageSize, pageNr);


                    if (members.Any())
                    {
                        var memberInfos = _mapper.Map<IEnumerable<MemberInfo>>(members);
                        foreach (MemberInfo mi in memberInfos)
                        {
                            mi.TotalAllocation = _employeeRepository.ComputeTotalAllocation(mi.Id);
                        }
                        return memberInfos;
                    }

                }
            }

            return null;
        }
        public OperationResult AddEmployee(AddEmployeeToDepartmentInputInfo inputInfo)
        {
            if (_employeeValidator.ValidateAddEmployeeToDepartmentInfo(inputInfo))
            {
                var newEp = _mapper.Map<Employee>(inputInfo);

                var department = _departmentRepository.GetDepartmentById(inputInfo.DepartmentId);
                if (department != null)
                {
                    _employeeRepository.AddEmployee(newEp);
                    return new OperationResult(true, Messages.SuccessfullyAddedEmployee);
                }
            }
            return new OperationResult(false, Messages.ErrorAddingEmployee);
        }

        public IEnumerable<EmployeeInfo> GetAllUnAllocatedEmployeesOnProject()
        {
            var employees = _employeeRepository.GetAllUnAllocatedEmployeesOnProject();
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
            foreach (EmployeeInfo ei in employeeInfos)
            {
                ei.TotalAllocation = _employeeRepository.ComputeTotalAllocation(ei.Id);
                ei.Role = _projectRepository.GetEmployeeRoleById(ei.Id);

            }
            return employeeInfos;
        }

        public IEnumerable<NotFullyAllocatedEmployeesInfo> GetEmployeesThatAreNotFullyAllocated(int projectId,string departmentName, int? pageSize, int? pageNr,PositionType? ptype)
        {
            var employees = _employeeRepository.GetEmployeesThatAreNotFullyAllocated(projectId,departmentName, pageSize,pageNr,ptype);
            var employeeInfos = _mapper.Map<IEnumerable<NotFullyAllocatedEmployeesInfo>>(employees);
            foreach (NotFullyAllocatedEmployeesInfo ei in employeeInfos)
            {
                ei.RemainingAllocation = 100-_employeeRepository.ComputeTotalAllocation(ei.Id);
                ei.Role = _projectRepository.GetEmployeeRoleById(ei.Id);
                ei.DepartmentName = _departmentRepository.GetDepartmentById(ei.DepartmentId).Name;
            }
            return employeeInfos;
        }

        public int GetTotalAllocation(int employeeId)
        {
            return _employeeRepository.ComputeTotalAllocation(employeeId);
        }

        public IEnumerable<JobTypeInfo> GetJobTypesDescriptions()
        {
            Array values = Enum.GetValues(typeof(JobType));

            return (from JobType jobType in values
                select new JobTypeInfo
                {
                    Id = (int) jobType,
                    Description = jobType.GetDescription()
                }).ToList();
        }

        public IEnumerable<PositionTypeInfo> GetPositionTypesDescriptions()
        {
            Array values = Enum.GetValues(typeof(PositionType));
            foreach (PositionType positionType in values)
            {
                yield return new PositionTypeInfo
                {
                    Id = (int) positionType,
                    Description = positionType.GetDescription()
                };
            }
        }

        public OperationResult AssignEmployee(AssignEmployeeInputInfo inputInfo)
        {
            var project = _projectRepository.GetProjectById(inputInfo.ProjectId);
            var employee = _employeeRepository.GetById(inputInfo.EmployeeId);

            if (project != null && employee != null)
            {
                _employeeRepository.AssignEmployee(new EmployeeProject(employee, project, inputInfo.Allocation));

                return new OperationResult(true, Messages.SuccessfullyAssignEmployee);
            }
            return new OperationResult(false, Messages.ErrorAssignEmployee);
        }
    }
}
