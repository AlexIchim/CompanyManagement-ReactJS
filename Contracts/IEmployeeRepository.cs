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
    }
}
