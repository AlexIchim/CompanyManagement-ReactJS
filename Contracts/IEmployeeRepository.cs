using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeProject> GetProjectByEmployeeId(int employeeId);

        void ReleaseEmployee(int employeeId);

        Employee GetById(int employeeId);

        void Save();

        int ComputeTotalAllocation(int employeeId);

        void UpdateTotalAllocation(int employeeId, int totalAllocation);

        void AddEmployeeToProject(EmployeeProject ep);

        IEnumerable<EmployeeProject> GetEmployeeProjectById(int projectId);

        IEnumerable<Employee> GetAllDepartmentEmployees(Department department);

        void AddEmployee(Employee employee);
    }
}
