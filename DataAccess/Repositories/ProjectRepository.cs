using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DbContext _context;

        public ProjectRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToArray();
        }

        public Project GetById(int id)
        {
            return _context.Projects.SingleOrDefault(d => d.Id == id);
        }

        public IEnumerable<Tuple<Employee, int>> GetEmployeesByProjectId(int id)
        {
            return _context.Projects.SingleOrDefault(d => d.Id == id)
                .Allocations
                .Select(a => new Tuple<Employee, int>(a.Employee, a.AllocationPercentage));
        }

        public void Delete(Project project)
        {
            _context.Projects.Remove(project);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
