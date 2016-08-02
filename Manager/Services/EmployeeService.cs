using AutoMapper;
using Contracts;
using Manager.InfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<ProjectsOfAnEmployeeInfo> GetProjectByEmployeeId(int employeeId)
        {
            return _employeeRepository.GetProjectByEmployeeId(employeeId).
                Select(emp => new ProjectsOfAnEmployeeInfo
                {
                    Id = emp.ProjectId,
                    Name = emp.Project.Name,
                    Allocation = emp.Allocation
                }).ToList();
        }

        public OperationResult ReleaseEmployee(int employeeId)
        {
            _employeeRepository.ReleaseEmployee(employeeId);
            return new OperationResult(true, Messages.SuccessfullyDeletedEmployee);
        }

        public OperationResult UpdateEmployee(UpdateEmployeeInputInfo inputInfo)
        {
            var employee = _employeeRepository.GetById(inputInfo.Id);
            //verify department

            if (employee == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingEmployee);
            }

            employee.Name = inputInfo.Name;
            employee.Address = inputInfo.Address;
            employee.EmploymentDate = inputInfo.EmploymentDate;
            employee.ReleaseDate = inputInfo.ReleaseDate;
            employee.TotalAllocation = inputInfo.TotalAllocation;
            employee.JobType = inputInfo.JobType;
            employee.PositionType = inputInfo.PositionType;

            _employeeRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedEmployee);

        }
    }
}
