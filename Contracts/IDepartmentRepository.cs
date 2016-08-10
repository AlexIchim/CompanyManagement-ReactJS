using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        Department GetByName(string name, int officeId);
        int GetProjectCountByDepartmentId(int id);
        IEnumerable<Project> GetProjectsByDepartmentId(int id, int? pageSize = null, int? pageNumber = null);
        IEnumerable<Employee> GetEmployeesByDepartmentId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "", int? positionIdFilter = null);
        int GetEmployeeCountByDepartmentId(int id, string searchString = "", int? positionIdFilter = null);
        void Add(Department department, int? departmentManagerId);
        void Update(Department department, int? departmentManagerId);
        void Save();
    }
}
