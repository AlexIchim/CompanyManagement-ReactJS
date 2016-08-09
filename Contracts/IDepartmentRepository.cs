using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentById(int departmentId);
        IEnumerable<Project> GetProjectsOfDepartment(int departmentId, int pageSize, int pageNumber, int? status = null);
        IEnumerable<Employee> GetMembersOfDepartment(int departmentId, int pageSize, int pageNumber, string name = "", int? jobType = null, int? position = null, int? allocation = null);
        IEnumerable<Project> FilterProjectsOfADepartmentByStatus(int departmentId, string status);
        void AddDepartment(Department department);
        void Save();
    }
}
