using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

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

        public IEnumerable<DepartmentInfo> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllDepartments();
            var departmentsInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentsInfos;
        }

        public DepartmentInfo GetDepartmentById(int departmentId) {
            var department = _departmentRepository.GetDepartmentById(departmentId);
            var departmentInfo = _mapper.Map<DepartmentInfo>(department);

            return departmentInfo;
        }

        public IEnumerable<EmployeeInfo> GetAllMembersOfADepartment(int departmentId)
        {
            var employees = _departmentRepository.GetAllMembersOfADepartment(departmentId);
            var employeesInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);

            return employeesInfos;
        }

        public IEnumerable<ProjectInfo> GetAllProjectsOfADepartment(int departmentId) {
            var projects = _departmentRepository.GetAllProjectsOfADepartment(departmentId);
            var projectsInfos = _mapper.Map<IEnumerable<ProjectInfo>>(projects);

            return projectsInfos;
        }

        public OperationResult AddDepartment(AddDepartmentInputInfo inputInfo)
        {
            var newDepartment = _mapper.Map<Department>(inputInfo);
            _departmentRepository.AddDepartment(newDepartment);

            return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }

        public OperationResult UpdateDepartment(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _departmentRepository.GetDepartmentById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            department.Name = inputInfo.Name;

            _departmentRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }

        public OperationResult DeleteDepartment(int departmentId) {
            var department = _departmentRepository.GetDepartmentById(departmentId);

            if (department == null) {
                return new OperationResult(false, Messages.ErrorWhileDeletingDepartment);
            }

            _departmentRepository.DeleteDepartment(department);

            return new OperationResult(true, Messages.SuccessfullyDeletedDepartment);
        }
    }
}
