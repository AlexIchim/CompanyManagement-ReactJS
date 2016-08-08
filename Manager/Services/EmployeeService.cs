using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Linq;

namespace Manager.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeInfo> GetAll()
        {
            var employees = _employeeRepository.GetAll();
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);

            return employeeInfos;
        }

        public IEnumerable<EmployeeInfo> GetAllDepartmentManagers()
        {
            var employees = _employeeRepository.GetAllDepartmentManagers();
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);

            return employeeInfos;
        }

        public IEnumerable<EmployeeInfo> GetAvailableEmployees(int? departmentId, int? positionId, int? projectId, int? pageSize = null, int? pageNumber = null)
        {
            var employees = _employeeRepository.GetAvailable(departmentId, positionId, projectId, pageSize, pageNumber);
            var employeeInfos = _mapper.Map<IEnumerable<EmployeeInfo>>(employees);

            return employeeInfos;
        }

        public EmployeeInfo GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            var employeeInfo = _mapper.Map<EmployeeInfo>(employee);

            return employeeInfo;
        }

        public IEnumerable<EmployeeAllocationInfo> GetAllocationsByEmployeeId(int id)
        {
            var employeeAllocations = _employeeRepository.GetAllocationsByEmployeeId(id);

            var employeeAllocationInfos = employeeAllocations.Select(a => new EmployeeAllocationInfo()
            {
                ProjectName = a.Project.Name,
                AllocationPercentage = a.AllocationPercentage
            });

            return employeeAllocationInfos;
        }


        public OperationResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee == null)
            {
                return new OperationResult(false, Messages.ErrorWhileReleasingEmployee);
            }

            _employeeRepository.Delete(id);
            return new OperationResult(true, Messages.SuccessfullyReleasedEmployee);
        }

        public OperationResult Add(AddEmployeeInputInfo inputInfo)
        {
            var result = Validators.EmployeeValidator.Validate(inputInfo);
            var department = _departmentRepository.GetById(inputInfo.DepartmentId);
            var position = _positionRepository.GetById(inputInfo.PositionId);

            if (result.Success == true &&
                department != null &&
                position != null)
            {
                _employeeRepository.Add(_mapper.Map<Employee>(inputInfo));
            }
            else if (department == null)
            {
                return new OperationResult(false, Messages.NoSuchDepartment);
            }
            else if (position == null)
            {
                return new OperationResult(false, Messages.NoSuchPosition);
            }

            return result;
        }

        public OperationResult Update(UpdateEmployeeInputInfo inputInfo)
        {
            var employee = _employeeRepository.GetById(inputInfo.Id);
            OperationResult result = Validators.EmployeeValidator.Validate(inputInfo);

            if (employee != null && result.Success == true)
            {
                employee.Name = inputInfo.Name;
                employee.Email = inputInfo.Email;
                employee.Address = inputInfo.Address;
                employee.EmploymentHours = inputInfo.EmploymentHours;
                employee.EmploymentDate = inputInfo.EmploymentDate;
                _employeeRepository.Save();
                return new OperationResult(true, Messages.SuccessfullyUpdatedEmployee);
            }

            return result;
        }
    }
}
