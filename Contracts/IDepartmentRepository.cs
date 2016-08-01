﻿using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        IEnumerable<Project> GetProjectsByDepartmentId(int id);
        void Add(Department department);
        void Save();
    }
}
