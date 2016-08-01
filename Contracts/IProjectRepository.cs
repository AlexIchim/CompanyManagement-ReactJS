using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Contracts
{
    public interface IProjectRepository
    {
        IEnumerable<Employee> GetEmployeeByProjectId(int ProjectId);
        IQueryable<EmployeeProject> GetEmployeesAllocation(int ProjectId);

        Project GetProjectById(int projectId);

        IEnumerable<EmployeeProject> GetEmployeeProjectByid(int projectId);

        void Add(Project project);

        void Delete(Project project, IEnumerable <EmployeeProject> employeeProject);
    }
}
