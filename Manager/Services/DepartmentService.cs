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
            var department = _departmentRepository.GetDepartmentById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            department.Name = inputInfo.Name;
            _departmentRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }

        public OperationResult AddEmployeeToDepartment(AddEmployeeToDepartmentInputInfo inputInfo)
        {
            var newEp = _mapper.Map<Employee>(inputInfo);
            newEp.TotalAllocation = 0;
            _departmentRepository.AddEmployeeToDepartment(newEp);
            return new OperationResult(true, Messages.SuccessfullyAddedEmployee);
        }

        public OperationResult AddDepartment(AddDepartmentInputInfo inputInfo)
        {
            var newDepartment = _mapper.Map<Department>(inputInfo);

            var depManagerId = inputInfo.DepartmentManagerId;

            if (_departmentRepository.IsDepartmentManager(depManagerId))
            {
                _departmentRepository.Add(newDepartment, inputInfo.DepartmentManagerId);
                return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
            }

            return new OperationResult(false, Messages.ErrorAddingDepartment);

        }

        public IEnumerable<EmployeeInfo> GetAllDepartmentManagers()
        {
            var departmentManagers= _departmentRepository.GetAllDepartmentManagers();
            var departmentManagersInfo= _mapper.Map<IEnumerable<EmployeeInfo>>(departmentManagers);

            return departmentManagersInfo;
        }

        public OperationResult UpdateDepartment(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _departmentRepository.GetDepartmentById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            var dm = _departmentRepository.GetEmployeeById(inputInfo.DepartmentManagerId);

            if (dm != null && _departmentRepository.IsDepartmentManager(dm.Id))
            {
                department.Name = inputInfo.Name;
                department.DepartmentManager = dm;
                _departmentRepository.Save();
                return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
            }

            return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
                     
            

            
        }
    }
}
