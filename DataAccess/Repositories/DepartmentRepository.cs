using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;
using System;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DbContext _context;

        public DepartmentRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToArray();
        }

        public Department GetById(int Id)
        {
            return _context.Departments.SingleOrDefault(d => d.Id == Id);
        }
        public Department GetByName(string name, int officeId)
        {
            return _context.Departments.SingleOrDefault(d => d.Name == name && d.OfficeId == officeId);
        }
        public IEnumerable<Project> GetProjectsByDepartmentId(int id)
        {
            var dept = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return new Project[0];
            }
            else
            {
                return dept.Projects.ToArray();
            }
        }

        public IEnumerable<Employee> GetEmployeesByDepartmentId(int id)
        {
            var dept = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return new Employee[0];
            }
            else
            {
                return dept.Employees.ToArray();
            }
        }

        public void Add(Department department, int? departmentManagerId)
        {
            if (departmentManagerId != null)
            {
                Employee departmentManager = _context.Employees.First(e => e.Id == departmentManagerId);
                department.DepartmentManager = departmentManager;
            }
            _context.Departments.Add(department);
            Save();
        }
        public void Update(Department department, int? departmentManagerId)
        {
            if (departmentManagerId != null)
            {
                Employee departmentManager = _context.Employees.First(e => e.Id == departmentManagerId);
                department.DepartmentManager = departmentManager;
                _context.Departments.First(d => d.Id == department.Id).DepartmentManager = departmentManager;
            }
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
