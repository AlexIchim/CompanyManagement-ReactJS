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
    }
}
