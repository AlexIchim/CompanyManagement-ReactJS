using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Domain.Models;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbContext _context;
        public ProjectRepository(DbContext context)
        {
            _context = context;
        }

        public void AddEmployeeToProject(EmployeeProject ep)
        {
            Employee employee = _context.Employees.SingleOrDefault(e => e.Id == ep.EmployeeId);
            Project project = _context.Projects.SingleOrDefault(p => p.Id == ep.ProjectId);
            ep.Employee = employee;
            ep.Project = project;
            _context.EmployeeProjects.Add(ep);
            _context.SaveChanges();
        }
        public IEnumerable<Employee> GetByProjectId(int projectid)
        {
            List<Employee> array = new List<Employee>();
            var proj = _context.EmployeeProjects.Where(ep => ep.ProjectId == projectid);

            foreach (EmployeeProject ep in proj)
            {
                Employee emp = new Employee();
                emp = _context.Employees.SingleOrDefault(e => e.Id == ep.EmployeeId);
                array.Add(emp);
            }
            return array.ToArray();

        }

        public IQueryable<EmployeeProject> GetEmployeesAllocation(int ProjectId)
        {
            return _context.EmployeeProjects.Where (p => p.ProjectId == ProjectId);
        }

        
    }
}
