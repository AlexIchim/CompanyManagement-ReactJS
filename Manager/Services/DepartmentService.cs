using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Collections.Generic;
using Domain.Enums;

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



        /*public OperationResult Update(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _departmentRepository.GetDepartmentById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            department.Name = inputInfo.Name;
            _departmentRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }*/

        

        public OperationResult AddDepartment(AddDepartmentInputInfo inputInfo)
        {
            var newDepartment = _mapper.Map<Department>(inputInfo);

            var depManagerId = inputInfo.DepartmentManagerId;

            if (!_departmentRepository.DepartmentWithNameExists(inputInfo.Name))
            { 
                if (_departmentRepository.IsDepartmentManager(depManagerId))
                {
                _departmentRepository.AddDepartment(newDepartment, inputInfo.DepartmentManagerId);
                return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
                }
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

            if (dm != null && dm.PositionType == PositionType.DepartmentManager)
            {
                department.Name = inputInfo.Name;    
                return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
            }
            department.DepartmentManager = dm;
            _departmentRepository.Save();
            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }
    }
}
