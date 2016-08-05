using Contracts;
using DataAccess.Context;
using DataAccess.Extensions;
using Domain.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbContext _context;
        public ProjectRepository(DbContext context)
        {
            _context = context;
        }

        public int GetEmployeeProjectAllocationById(int projectId, int employeeID)
        {
            var employeeProject = _context.EmployeeProjects.SingleOrDefault(e => e.EmployeeId == employeeID && e.ProjectId == projectId);
            return employeeProject.Allocation;
        }

        public string GetEmployeeRoleById(int employeeId)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);
            string role = employee.PositionType.ToString();
            return role;
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
            var queryableEmployees = employees.AsQueryable();
            return queryableEmployees.OrderBy(d => d.Name).Paginate(pageSize, pageNr).ToArray();

        }

        public IQueryable<EmployeeProject> GetEmployeesAllocation(int projectId)
        {
            return _context.EmployeeProjects.Where(p => p.ProjectId == projectId);
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

        public EmployeeProject GetEmployeeProjectById(int employeeId, int projectId)
        {
            return
                _context.EmployeeProjects.SingleOrDefault(ep => ep.ProjectId == projectId && ep.EmployeeId == employeeId);
        }

        public void DeleteEmployeeProject(EmployeeProject ep)
        {
            _context.EmployeeProjects.Remove(ep);
            Save();
        }
    }
}
