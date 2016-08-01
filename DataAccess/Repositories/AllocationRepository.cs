using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class AllocationRepository : IAllocationRepository
    {
        private readonly DbContext _context;

        public AllocationRepository(DbContext context)
        {
            _context = context;
        }

        public ProjectAllocation GetById(int id)
        {
            return _context.ProjectAllocations.SingleOrDefault(pa => pa.Id == id);
        }

        public void Add(ProjectAllocation allocation)
        {
            _context.ProjectAllocations.Add(allocation);
            Save();
        }

        public void Delete(ProjectAllocation allocation)
        {
            _context.ProjectAllocations.Remove(allocation);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}