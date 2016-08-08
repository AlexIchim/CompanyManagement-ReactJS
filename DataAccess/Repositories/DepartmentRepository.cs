using Contracts;
using DataAccess.Context;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Manager.Services;

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

        public void AddDepartment(Department department, int departmentManagerId)
        {

            Employee employee = _context.Employees.SingleOrDefault(e => e.Id == departmentManagerId);
            department.DepartmentManager = employee;
            _context.Departments.Add(department);
            Save();
        }

        public bool IsDepartmentManager(int employeeId)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);

            if (employee != null && employee.PositionType == PositionType.DepartmentManager)
            {
                return true;
            }

            return false;
        }

        public bool DepartmentWithNameExists(string name)
        {
            var department = _context.Departments.SingleOrDefault(d => d.Name == name);

            if (department == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Employee> GetAllDepartmentManagers()
        {
            var departments = _context.Departments;
            var departmentManagers= _context.Employees.Where(e => e.PositionType == PositionType.DepartmentManager);
            
            List<Employee> array=new List<Employee>();
            foreach (var dm in departmentManagers)
            {
                bool empty = true;
                foreach (var dep in departments)
                {
                    if (dep.DepartmentManager == dm)
                    {
                        empty = false;
                    }
                } 
                if (empty==true)
                    array.Add(dm);
            }
            return array;
        }

        public Employee GetEmployeeById(int? id)
        {
            return _context.Employees.SingleOrDefault(e => e.Id == id);
        }

        public bool EmployeeExists(int id)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == id);
            if (employee != null)
                return true;
            return false;
        }

    }
}
