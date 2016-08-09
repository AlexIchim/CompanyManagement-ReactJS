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
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetAvailable(int? departmentId, int? positionId, int? projectId, int? pageSize = null, int? pageNumber = null);
        int GetAvailableEmployeesCount(int? departmentId, int? positionId, int? projectId);
        IEnumerable<Employee> GetAllDepartmentManagers();
        IEnumerable<ProjectAllocation> GetAllocationsByEmployeeId(int id);
        Employee GetById(int id);
        void Delete(int id);
        void Add(Employee employee);
        void Save();

    }
}
