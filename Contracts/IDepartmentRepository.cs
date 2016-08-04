using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentById(int departmentId);
        IEnumerable<Project> GetProjectsOfDepartment(int departmentId, int? status = null);
        IEnumerable<Employee> GetMembersOfDepartment(int departmentId, string name = "", int? jobType = null, int? position = null, int? allocation = null);
        void AddDepartment(Department department);
        void Save();
    }
}
