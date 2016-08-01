using Contracts;
using DataAccess.Context;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

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

        public Department GetById(int id)
        {
            return _context.Departments.SingleOrDefault(d => d.Id == id);
        }

        public void Add(Department department)
        {
            _context.Departments.Add(department);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        /*
        public IEnumerable<Project> GetAllDepartmentProjects(int id)
        {
            var array = _context.Projects.Where(project => project.Department.Id == id);
            return array.ToArray();
        }*/


        public IEnumerable<Project> GetAllDepartmentProjects(int id)
        {
            var array = _context.Departments.Include("Projects").SingleOrDefault(d => d.Id == id);
            return array.Projects;
        }

        public IEnumerable<Employee> GetAllUnAllocatedEmployeesOnProject()
        {
            var array = _context.Employees.Where(e => e.TotalAllocation==0);
            return array.ToArray();
        }

        public IEnumerable<Employee> GetEmployeesThatAreNotFullyAllocated()
        {
            var array = _context.Employees.Where(e => e.TotalAllocation < 100);
            return array.ToArray();
        }

        public IEnumerable<Employee> GetAllDepartmentMembers(int id)
        {
            var array = _context.Departments.Include("Employees").SingleOrDefault(d => d.Id == id);
            return array.Employees;
        }
    }
}
