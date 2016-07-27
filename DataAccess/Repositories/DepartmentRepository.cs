using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
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
            return _context.Departments.SingleOrDefault(d=>d.Id == id);
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
