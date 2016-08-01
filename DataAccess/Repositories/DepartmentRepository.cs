using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;

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

        public void Add(Department department)
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
