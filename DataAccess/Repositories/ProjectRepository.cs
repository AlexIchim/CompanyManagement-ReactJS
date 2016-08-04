using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Domain.Models;
using DataAccess.Context;
using DataAccess.Extensions;

namespace DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbContext _context;
        public ProjectRepository(DbContext context)
        {
            _context = context;
        }


        public IEnumerable<Employee> GetEmployeesByProjectId(int projectid, int? pageSize, int? pageNr)
        {
            List<Employee> employees = new List<Employee>();
            var proj = _context.EmployeeProjects.Where(ep => ep.ProjectId == projectid);

            foreach (EmployeeProject ep in proj)
            {
                Employee emp = new Employee();
                emp = _context.Employees.SingleOrDefault(e => e.Id == ep.EmployeeId);
                employees.Add(emp);
            }
            var queryableEmployees= employees.AsQueryable();
            return queryableEmployees.OrderBy(d => d.Name).Paginate(pageSize, pageNr).ToArray();

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
            if (employeeProjects.Any())
            {
                foreach (EmployeeProject employeeProject in employeeProjects)
                {
                    var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeProject.EmployeeId);
                    //////////////////////////////////////////////////

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

        public IEnumerable<Project> GetAllDepartmentProjects(Department department, int? pageSize, int? pageNr)
        {
            return _context.Projects.Where(d => d.DepartmentId == department.Id).OrderBy(d => d.Name).Paginate(pageSize, pageNr).ToArray();
        }

        public IEnumerable<Project> FilterProjectByStatus(string status, int? pageSize, int? pageNr)
        {
            return _context.Projects.Where(p => p.Status.ToString() == status);
        }
    }
}
