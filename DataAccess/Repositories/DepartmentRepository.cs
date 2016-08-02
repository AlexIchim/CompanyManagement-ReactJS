using Contracts;
using DataAccess.Context;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;

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

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.SingleOrDefault(d => d.Id == id);
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAllUnAllocatedEmployeesOnProject()
        {
            var array = _context.Employees.Where(e => e.TotalAllocation == 0);
            return array.ToArray();
        }

        public IEnumerable<Employee> GetEmployeesThatAreNotFullyAllocated()
        {
            var array = _context.Employees.Where(e => e.TotalAllocation < 100);
            return array.ToArray();
        } 

       

        public void Add(Department department, int? departmentManagerId)
        {
            Employee employee = _context.Employees.SingleOrDefault(e => e.Id == departmentManagerId);
            department.DepartmentManager = employee;
            _context.Departments.Add(department);
            Save();
        }

        public bool IsDepartmentManager(int? employeeId)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);


            if (employee != null && employee.PositionType == PositionType.DepartmentManager)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Employee> GetAllDepartmentManagers()
        {
            return _context.Employees.Where(e => e.PositionType == PositionType.DepartmentManager);
        }

        public Employee GetEmployeeById(int? id)
        {

            return _context.Employees.SingleOrDefault(e => e.Id == id);
        }

    }
}
