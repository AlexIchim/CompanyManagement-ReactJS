using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;
using System;

namespace DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbContext _context;

        public ProjectRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(Project project)
        {
            _context.Projects.Add(project);
            Save();
        }

        public void Delete(Project project) {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public Boolean editAllocation(int projectId, int employeeId, int newAllocation) {
            Assignment assignment =
                _context.Assignments.SingleOrDefault(a => a.ProjectId == projectId && a.EmployeeId == employeeId);
            if (assignment == null) {
                return false;
            }
            assignment.Allocation = newAllocation;
            _context.SaveChanges();
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToArray();
        }
        public Project GetById(int id)
        {
            return _context.Projects.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Assignment> GetAllAssignmentsFromProject(int projectId)

        {
            ICollection<Employee> allEmployees = new List<Employee>();
            var assignments = _context.Assignments.Where(ep => ep.ProjectId == projectId);
            return assignments.ToArray();
        }

        public int GetAllocationOfEmployeeFromProject(int projectId, int employeeId)
        {
            Assignment assignment = _context.Assignments.SingleOrDefault(ep => ep.ProjectId == projectId && ep.EmployeeId ==employeeId);
            return assignment.Allocation;
        }

        public int GetNrTeamMembers(int projectId)
        {
            var assignments = _context.Assignments.Where(asgn => asgn.ProjectId == projectId);
            int nr = assignments.Count();
            return nr;

        }
    }

}
