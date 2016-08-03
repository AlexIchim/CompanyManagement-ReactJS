using System;
using Domain.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetDepartmentById(int? id);

        void Save();

        IEnumerable<Employee> GetAllUnAllocatedEmployeesOnProject();

        IEnumerable<Employee> GetEmployeesThatAreNotFullyAllocated();


        void AddDepartment(Department department, int? departmentManagerId);

        bool IsDepartmentManager(int? employeeId);

        bool DepartmentWithNameExists(string name);

        IEnumerable<Employee> GetAllDepartmentManagers();

        Employee GetEmployeeById(int? id);

        bool EmployeeExists(int id);
    }
}
