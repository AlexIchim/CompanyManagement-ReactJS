﻿using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentById(int departmentId);
        IEnumerable<Employee> GetAllMembersOfADepartment(int departmentId);
        IEnumerable<Project> GetAllProjectsOfADepartment(int departmentId);
        void AddDepartment(Department department);
        void Save();
    }
}
