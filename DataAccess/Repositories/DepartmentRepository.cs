using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Contracts;
using DataAccess.Extensions;
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
            var department = GetDepartmentById(departmentId);
            return department.Employees;
        }

        public IEnumerable<Project> GetAllProjectsOfADepartment(int departmentId) {
            //var department = GetDepartmentById(departmentId);
            return _context.Projects.Where(p => p.Department.Id == departmentId).OrderBy(o => o.Id).Paginate(5, 1).ToArray();
            //return department.Projects;
        }

        public IEnumerable<Project> FilterProjectsOfADepartmentByStatus(int departmentId, string status)
        {
            return _context.Projects.Where(p => p.Department.Id == departmentId && p.Status.ToString() == status).ToArray();
        }
        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
