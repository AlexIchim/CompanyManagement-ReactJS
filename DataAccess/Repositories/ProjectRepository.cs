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

    }
}
