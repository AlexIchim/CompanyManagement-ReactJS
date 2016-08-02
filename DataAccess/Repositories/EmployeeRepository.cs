using Contracts;
using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var array = _context.EmployeeProjects.Where(ep => ep.EmployeeId == employeeId).ToList();

            int totalAllocation = 0;

            foreach (var ep in array)
            {
                totalAllocation += ep.Allocation;
            }

            return totalAllocation;
        }

        public void UpdateTotalAllocation(int employeeId, int totalAllocation)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);

            if (employee != null)
            {
                employee.TotalAllocation = totalAllocation;
                Save();
            }
            
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

    }
}
