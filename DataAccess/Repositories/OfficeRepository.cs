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

        public Department GetById(int id)
        {
            return _context.Departments.SingleOrDefault(d => d.Id == id);
        }

        public void AddOffice(Office office)
        {
            _context.Offices.Add(office);
            Save();
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

        public Office GetOfficeById(int officeId)
        {
            return _context.Offices.SingleOrDefault(o => o.Id == officeId);
        }
        public Employee GetEmployeeById(int? id)
        {

            return _context.Employees.SingleOrDefault(e => e.Id == id);
        }

    }
}
