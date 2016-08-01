using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

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

        public EmployeeInfo GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            var employeeInfo = _mapper.Map<EmployeeInfo>(employee);

            return employeeInfo;
        }

        public OperationResult Add(AddEmployeeInputInfo inputInfo)
        {
            var newEmployee = _mapper.Map<Employee>(inputInfo);
            _employeeRepository.Add(newEmployee);

            return new OperationResult(true, Messages.SuccessfullyAddedEmployee);
        }

        public OperationResult Update(UpdateEmployeeInputInfo inputInfo)
        {
            var employee = _employeeRepository.GetById(inputInfo.Id);

            if (employee == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingEmployee);
            }

            employee.Name = inputInfo.Name;
            _employeeRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedEmployee);
        }
    }
}
