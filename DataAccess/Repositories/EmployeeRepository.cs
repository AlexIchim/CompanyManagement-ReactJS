using Contracts;
using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Extensions;
using Domain.Enums;
using Manager.Descriptors;

namespace DataAccess.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly DbContext _context;
        public EmployeeRepository(DbContext context)
        {
            _context = context;
        }
        public IEnumerable<EmployeeProject> GetProjectByEmployeeId(int employeeId)
        {
            return _context.EmployeeProjects.Where(e => e.EmployeeId == employeeId).ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void ReleaseEmployee(int employeeId)
        {
            var employees=_context.EmployeeProjects.Where(e => e.EmployeeId == employeeId).ToList();


            if (employees.Count != 0)
            {

                foreach (var ep in employees)
                {
                    _context.EmployeeProjects.Remove(ep);
                }
            }

            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);

            if (employee != null)
            {
                employee.ReleaseDate = DateTime.Now;
                Save();
            }
            
        }

        public Employee GetById(int employeeId)
        {
            return _context.Employees.SingleOrDefault(o => o.Id == employeeId);
        }

        public int ComputeTotalAllocation(int employeeId)
        {
            return _context.EmployeeProjects.Where(ep => ep.EmployeeId == employeeId).ToList().Sum(ep => ep.Allocation);         
        }


        public void AddEmployeeToProject(EmployeeProject ep)
        {
            Employee employee = _context.Employees.SingleOrDefault(e => e.Id == ep.EmployeeId);
            Project project = _context.Projects.SingleOrDefault(p => p.Id == ep.ProjectId);
            ep.Employee = employee;
            ep.Project = project;
            _context.EmployeeProjects.Add(ep);
            Save();
        }

        public IEnumerable<EmployeeProject> GetEmployeeProjectById(int projectId)
        {
            return _context.EmployeeProjects.Where(ep => ep.ProjectId == projectId).ToList();
        }

        public IEnumerable<Employee> GetAllDepartmentEmployees(Department department, int? pageSize, int? pageNr)
        {
            return
                _context.Employees.Where(e => e.DepartmentId == department.Id)
                    .OrderBy(e => e.Name)
                    .Paginate(pageSize, pageNr)
                    .ToArray();    
        }
        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            Save();
        }

        public IEnumerable<Employee> GetAllUnAllocatedEmployeesOnProject()
        {
            var employees = _context.Employees.ToList();
            List<Employee> unallocatedEmployees = new List<Employee>();
            foreach (Employee employee in employees)
            {
                if (ComputeTotalAllocation(employee.Id) == 0)
                {
                    unallocatedEmployees.Add(employee);
                }
            } 
            return unallocatedEmployees;
        }

        public IEnumerable<Employee> GetEmployeesThatAreNotFullyAllocated()
        {
            var employees = _context.Employees.ToList();
            List<Employee> fullyAllocatedEmployees = new List<Employee>();
            foreach (Employee employee in employees)
            {
                if (ComputeTotalAllocation(employee.Id) < 100)
                {
                    fullyAllocatedEmployees.Add(employee);
                }
            }
            return fullyAllocatedEmployees;
        }

    }
}
