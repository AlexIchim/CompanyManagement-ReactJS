﻿using Domain.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IOfficeRepository
    {
        IEnumerable<Office> GetAll();

        IEnumerable<Department> GetAllDepartmentsOfAnOffice(int officeId);

        void Add(Department department, int? departmentManagerId);
        void AddOffice(Office office);

        Department GetById(int id);
        Employee GetEmployeeById(int? id);

        void Save();


    }
}
