using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        Department GetByName(string name, int officeId);
        int GetProjectCountByDepartmentId(int id, string searchString = "", string statusFilter = "");
        IEnumerable<Project> GetProjectsByDepartmentId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "", string statusFilter = "");
        IEnumerable<Employee> GetEmployeesByDepartmentId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "",
                                                         int? positionIdFilter = null, int? employmentFilter = null,
                                                         int? allocationFromFilter = null, int? allocationToFilter = null);
        int GetEmployeeCountByDepartmentId(int id, string searchString = "",
                                           int? positionIdFilter = null, int? employmentFilter = null,
                                           int? allocationFromFilter = null, int? allocationToFilter = null);
        void Add(Department department, int? departmentManagerId);
        void Update(Department department, int? departmentManagerId);
        void Save();
    }
}
