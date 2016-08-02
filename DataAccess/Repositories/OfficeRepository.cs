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
            var office = _context.Offices.SingleOrDefault(d => d.Id == officeId);

            if (office == null)
            {
                return new Department[0];
            }
            else
            {
                return office.Departments.Paginate(pageSize, pageNumber).ToArray();
            }
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
