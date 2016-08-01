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
        IEnumerable<Employee> GetByProjectId(int ProjectId);
        IQueryable<EmployeeProject> GetEmployeesAllocation(int ProjectId);
    }
}
