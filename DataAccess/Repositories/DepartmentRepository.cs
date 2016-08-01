using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Contracts;
using Domain.Models;
using DbContext = DataAccess.Context.DbContext;

namespace DataAccess.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly DbContext _context;

        public DepartmentRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToArray();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return _context.Departments.SingleOrDefault(d=>d.Id == departmentId);
        }

        public IEnumerable<Employee> GetAllMembersOfADepartment(int departmentId)
        {
            Department department = _context.Departments.SingleOrDefault(d => d.Id == departmentId);
            return department.Employees;
        }

        public IEnumerable<Project> GetAllProjectsOfADepartment(int departmentId) {
            Department department = _context.Departments.SingleOrDefault(d => d.Id == departmentId);
            return department.Projects;
        }

        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            Save();
        }

        public void DeleteDepartment(Department department)
        {
            _context.Departments.Remove(department);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
