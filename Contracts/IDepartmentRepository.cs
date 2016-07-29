using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        void Add(Department department);
        void Save();

        IEnumerable<Project> GetAllDepartmentProjects(int id);

        IEnumerable<Employee> GetAllUnAllocatedEmployeesOnProject();
    }
}
