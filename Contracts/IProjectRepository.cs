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
        IEnumerable<Employee> GetEmployeesByProjectId(int projectId, int? pageSize, int? pageNr);
        IQueryable<EmployeeProject> GetEmployeesAllocation(int projectId);

        void Save();
        Project GetProjectById(int projectId);

        void Add(Project project);

        void Delete(Project project, IEnumerable <EmployeeProject> employeeProject);

        IEnumerable<EmployeeProject> GetEmployeeProjectById(int projectId);

        IEnumerable<Project> GetAllDepartmentProjects(Department department, int? pageSize, int? pageNr);
        IEnumerable<Project> FilterProjectByStatus(string status, int? pageSize, int? pageNr);
    }
}
