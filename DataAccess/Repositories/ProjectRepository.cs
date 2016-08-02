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


        public IEnumerable<Employee> GetEmployeeByProjectId(int projectid)
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

        public IQueryable<EmployeeProject> GetEmployeesAllocation(int projectId)
        {
            return _context.EmployeeProjects.Where (p => p.ProjectId == projectId);
        }

        public Project GetProjectById(int projectId)
        {
            return _context.Projects.SingleOrDefault(p => p.Id == projectId);
        }

       
        public void Add(Project project)
        {
            _context.Projects.Add(project);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Delete(Project project, IEnumerable<EmployeeProject> employeeProjects)
        {
            if (employeeProjects.Count() != 0)
            {
                foreach (EmployeeProject employeeProject in employeeProjects)
                {
                    var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeProject.EmployeeId);

                    int totalAllocation = employee.TotalAllocation;

                    int newTotalAllocation = totalAllocation - employeeProject.Allocation;

                    employee.TotalAllocation = newTotalAllocation;

                    _context.EmployeeProjects.Remove(employeeProject);
                    

                }
            }
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            Save();
        }

        public IEnumerable<EmployeeProject> GetEmployeeProjectById(int projectId)
        {
            return _context.EmployeeProjects.Where(ep => ep.ProjectId == projectId).ToList();
        }

        public IEnumerable<Project> GetAllDepartmentProjects(Department department)
        { 
            return department.Projects;
        }
    }
}
