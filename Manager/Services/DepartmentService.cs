using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Collections.Generic;

namespace Manager.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IEnumerable<DepartmentInfo> GetAll()
        {
            var departments = _departmentRepository.GetAll();
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;
        }

        public IEnumerable<ProjectInfo> GetAllDepartmentProjects(int inputInfo)
        {
            var newProject = _mapper.Map<int>(inputInfo);
            var projects = _departmentRepository.GetAllDepartmentProjects(newProject);
            if (projects == null)
            {
                return null;
            }
            var projectInfos = _mapper.Map<IEnumerable<ProjectInfo>>(projects);
            return projectInfos;
        }

        public IEnumerable<MemberInfo> GetAllDepartmentMembers(int inputInfo)
        {
            var newMember = _mapper.Map<int>(inputInfo);
            var members = _departmentRepository.GetAllDepartmentMembers(newMember);
            if (members == null)
            {
                return null;
            }
            var memberInfos = _mapper.Map<IEnumerable<MemberInfo>>(members);
            return memberInfos;
        }


        public IEnumerable<EmployeeInfo> GetAllUnAllocatedEmployeesOnProject()
        {
            var employees = _departmentRepository.GetAllUnAllocatedEmployeesOnProject();
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
            return employeeInfos;
        }

        public IEnumerable<EmployeeInfo> GetEmployeesThatAreNotFullyAllocated()
        {
            var employees = _departmentRepository.GetEmployeesThatAreNotFullyAllocated();
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
            return employeeInfos;
        }

       

        public OperationResult Update(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _departmentRepository.GetById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            department.Name = inputInfo.Name;
            _departmentRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }
    }
}
