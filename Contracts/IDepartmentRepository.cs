using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        Department GetByName(string name, int officeId);
        IEnumerable<Project> GetProjectsByDepartmentId(int id, int? pageSize = null, int? pageNumber = null);
        IEnumerable<Employee> GetEmployeesByDepartmentId(int id, int? pageSize = null, int? pageNumber = null);
        void Add(Department department, int? departmentManagerId);
        void Update(Department department, int? departmentManagerId);
        void Save();
    }
}
