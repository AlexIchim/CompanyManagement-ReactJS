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

        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            Save();
        }

        public void DeleteDepartment(int departmentId)
        {
            Department department = _context.Departments.SingleOrDefault(d => d.Id == departmentId);
            _context.Departments.Remove(department);
            Save();
        }

        public void UpdateDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
