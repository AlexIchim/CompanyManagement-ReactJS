using System;
using Domain.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        
        Department GetDepartmentById(int id);
        
        void Save();

        bool IsDepartmentManager(int employeeId);

        void AddDepartment(Department department, int departmentManagerId);

        bool DepartmentWithNameExists(string name);

        IEnumerable<Employee> GetAllDepartmentManagers();

        Employee GetEmployeeById(int? id);

        bool EmployeeExists(int id);
    }
}
