using System;
using Contracts;
using DataAccess.Context;
using DataAccess.Extensions;
using Domain.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Domain.Enums;
using Manager.Descriptors;
using Manager.InfoModels;
using System.Data.Entity;

namespace DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly Context.DbContext _context;
        public ProjectRepository(Context.DbContext context)
        {
            _context = context;
        }

        public int GetEmployeeProjectAllocationById(int projectId, int employeeID)
        {
            var employeeProject = _context.EmployeeProjects.SingleOrDefault(e => e.EmployeeId == employeeID && e.ProjectId == projectId);
            return employeeProject.Allocation;
        }

        public IEnumerable<Employee> GetEmployeesByProjectId(int projectid,PositionType? ptype, int? pageSize, int? pageNr)
        {
            var employees = _context.EmployeeProjects.
                Where(ep => ep.ProjectId == projectid).
                Select(ep => ep.Employee).
                Where(e=> ptype==null || e.PositionType==ptype).
                OrderBy(d => d.Name).
                Paginate(pageSize, pageNr).
                ToArray();
            
            return employees;
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

        public void Delete(Project project)
        {
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

        public IEnumerable<Project> GetAllDepartmentProjects(Department department, ProjectStatus? status, int? pageSize, int? pageNr)
        {
            string s = status.ToString();
            return _context.Projects.Where(p => (status==null || p.Status == status) && p.DepartmentId == department.Id).OrderBy(d => d.Name).Paginate(pageSize, pageNr).ToArray();
        }

        /*public IEnumerable<string> GetProjectStatusDescriptions()
        {
            return Enum.GetNames(typeof(ProjectStatus));
        }*/

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
