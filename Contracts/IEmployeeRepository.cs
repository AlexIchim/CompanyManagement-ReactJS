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
        IEnumerable<Employee> GetAvailable(int? departmentId, int? positionId);
        IEnumerable<Employee> GetAllDepartmentManagers();
        IEnumerable<Tuple<string, int>> GetAllocationsByEmployeeId(int id);
        Employee GetById(int id);
        void Delete(Employee employee);
        void Add(Employee employee);
        void Save();

    }
}
