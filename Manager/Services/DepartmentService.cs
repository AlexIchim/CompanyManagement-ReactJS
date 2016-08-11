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
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository, IOfficeRepository officeRepository)
        {
            _departmentRepository = departmentRepository;
            _officeRepository = officeRepository;
            _mapper = mapper;
        }

        public IEnumerable<DepartmentInfo> GetAll()
        {
            var departments = _departmentRepository.GetAll();
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;
        }

        public DepartmentInfo GetById(int id)
        {
            var department = _departmentRepository.GetById(id);
            var departmentInfo = _mapper.Map<DepartmentInfo>(department);

            return departmentInfo;
        }

        public IEnumerable<ProjectInfo> GetProjectsByDepartmentId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "", string statusFilter = "")
        {
            var projects = _departmentRepository.GetProjectsByDepartmentId(id, pageSize, pageNumber, searchString, statusFilter);
            var projectInfos = _mapper.Map<IEnumerable<ProjectInfo>>(projects);

            return projectInfos;
        }

        public int GetProjectCountByDepartmentId(int id, string searchString = "", string statusFilter = "")
        {
            return _departmentRepository.GetProjectCountByDepartmentId(id, searchString, statusFilter);
        }

        public IEnumerable<EmployeeInfo> GetEmployeesByDepartmentId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "",
                                                                    int? positionIdFilter = null, int? employmentFilter = null,
                                                                    int? allocationFromFilter = null, int? allocationToFilter = null)
        {
            var employees = _departmentRepository.GetEmployeesByDepartmentId(
                id, pageSize, pageNumber, searchString,
                positionIdFilter, employmentFilter,
                allocationFromFilter, allocationToFilter
            );
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);

            return employeeInfos;
        }

        public int GetEmployeeCountByDepartmentId(int id, string searchString = "", int? positionIdFilter = null, int? employmentFilter = null,
                                                  int? allocationFromFilter = null, int? allocationToFilter = null)
        {
            return _departmentRepository.GetEmployeeCountByDepartmentId(
                id, searchString,
                positionIdFilter, employmentFilter,
                allocationFromFilter, allocationToFilter
            );
        }

        public OperationResult Add(AddDepartmentInputInfo inputInfo)
        {
            var Office = _officeRepository.GetById(inputInfo.OfficeId);
            if (Office != null)
            {
                var department = _departmentRepository.GetByName(inputInfo.Name, inputInfo.OfficeId);
                if (department == null)
                {
                    var result = Validators.DepartmentValidator.Validate(inputInfo);
                    if (result.Success == true)
                    {
                        var newDepartment = _mapper.Map<Department>(inputInfo);
                        _departmentRepository.Add(newDepartment, inputInfo.DepartmentManagerId);
                    }
                    return result;
                }
                else return new OperationResult(false, Messages.ErrorWhileAddingDepartment);
            }
            else return new OperationResult(false, Messages.ErrorWhileAddingDepartment_OfficeIdInvalid);
        }

        public OperationResult Update(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _departmentRepository.GetById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment_InvalidId);
            }

            var result = Validators.DepartmentValidator.Validate(inputInfo);
            if (result.Success == true)
            {
                department.Name = inputInfo.Name;
                _departmentRepository.Update(department, inputInfo.DepartmentManagerId);
            }
            return result;
        }
    }
}
