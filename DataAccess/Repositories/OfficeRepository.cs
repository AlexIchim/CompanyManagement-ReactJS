using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;
using DataAccess.Extensions;

namespace DataAccess.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly DbContext _context;

        public OfficeRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Office> GetAll()
        {
            return _context.Offices.ToArray();
        }

        public Office GetById(int id)
        {
            return _context.Offices.SingleOrDefault(d => d.Id == id);
        }



        public IEnumerable<Department> GetDepartmentsByOfficeId(int officeId, int? pageSize = null, int? pageNumber = null)
        {
            return _context.Departments
                .Where(d => d.OfficeId == officeId)
                .OrderBy(d => d.Id)
                .Paginate(pageSize, pageNumber)
                .ToArray();
        }

        public int GetDepartmentCountByOfficeId(int officeId)
        {
            return _context.Departments
                .Where(d => d.OfficeId == officeId)
                .Count();
        }

        public void Add(Office office)
        {
            _context.Offices.Add(office);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
