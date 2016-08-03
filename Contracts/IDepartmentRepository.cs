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

        void Add(Department department, int departmentManagerId);

        bool IsDepartmentManager(int employeeId);

        IEnumerable<Employee> GetAllDepartmentManagers();

        Employee GetEmployeeById(int? id);
    }
}
