using AutoMapper;
using Contracts;
using Manager.InfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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

        public IEnumerable<EmployeeInfo> GetAvailableEmployees(int? departmentId, int? positionId)
        {
            var employees = _employeeRepository.GetAvailable(departmentId, positionId);
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
            var employeeAllocationInfos = employeeAllocations.Select(a => new EmployeeAllocationInfo() {
                ProjectName = a.Item1,
                AllocationPercentage = a.Item2
            });

            return employeeAllocationInfos;
        }


        public OperationResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee == null)
            {
                return new OperationResult(false, Messages.ErrorWhileDeletingEmployee);
            }

            _employeeRepository.Delete(employee);
            return new OperationResult(true, Messages.SuccessfullyDeletedEmployee);
        }
    }
}
