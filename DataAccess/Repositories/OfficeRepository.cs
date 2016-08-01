using Contracts;
using DataAccess.Context;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly DbContext _context;

        public OfficeRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Office> GetAll()
        {
            return _context.Offices.ToArray();
        }

        public IEnumerable<Department> GetAllDepartmentsOfAnOffice(int officeId)
        {
            var array = _context.Offices.SingleOrDefault(d => d.Id == officeId);

            return array.Departments;

        }
        public void Add(Department department, int? departmentManagerId)
        {
            Employee employee = _context.Employees.SingleOrDefault(e => e.Id == departmentManagerId);
            department.DepartmentManager = employee;
            _context.Departments.Add(department);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
