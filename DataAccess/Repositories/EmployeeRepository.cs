using Contracts;
using System.Collections.Generic;
using Domain.Models;
using DataAccess.Context;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbContext _context;

        public EmployeeRepository(DbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToArray();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.SingleOrDefault(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
