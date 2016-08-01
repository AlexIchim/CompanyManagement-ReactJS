using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        Department GetByName(string name, int officeId);
        IEnumerable<Project> GetProjectsByDepartmentId(int id);
        IEnumerable<Employee> GetEmployeesByDepartmentId(int id);
        void Add(Department department, int? departmentManagerId);
        void Update(Department department, int? departmentManagerId);
        void Save();
    }
}
