using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using Manager.Validators;

namespace Manager.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly AddEmployeeValidator _addEmployeeValidator;
        private readonly UpdateEmployeeValidator _updateEmployeeValidator;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;

            _addEmployeeValidator = new AddEmployeeValidator();
            _updateEmployeeValidator = new UpdateEmployeeValidator();
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
            var validationResult = _addEmployeeValidator.Validate(inputInfo);
            if (!validationResult.IsValid) {
                return new OperationResult(false, validationResult);
            }

            var newEmployee = _mapper.Map<Employee>(inputInfo);
            _employeeRepository.Add(newEmployee);

            return new OperationResult(true, Messages.SuccessfullyAddedEmployee);
        }

        public OperationResult Update(UpdateEmployeeInputInfo inputInfo)
        {
            var validationResult = _updateEmployeeValidator.Validate(inputInfo);
            if (!validationResult.IsValid) {
                return new OperationResult(false, validationResult);
            }

            var employee = _employeeRepository.GetById(inputInfo.Id);

            if (employee == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingEmployee);
            }

            employee.Name = inputInfo.Name;
            _employeeRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedEmployee);
        }

        public OperationResult Delete(int employeeId)
        {
            var office = _employeeRepository.GetById(employeeId);

            if (office == null)
            {
                return new OperationResult(false, Messages.ErrorWhileDeletingEmployee);
            }

            _employeeRepository.Delete(office);
            return new OperationResult(true, Messages.SuccessfullyDeletedEmployee);
        }

    }
}
