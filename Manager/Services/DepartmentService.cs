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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, IOfficeRepository officeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _officeRepository = officeRepository;
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
            newDepartment.DepartmentManager = _employeeRepository.GetById(inputInfo.DepartmentManagerId);
            newDepartment.Office = _officeRepository.GetById(inputInfo.OfficeId);

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
            department.DepartmentManager = _employeeRepository.GetById(inputInfo.DepartmentManagerId);

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
