using Contracts;
using DataAccess.Context;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Department> GetAllDepartmentsOfAnOffice(int officeId, int? pageSize, int? pageNr)
        {
            return _context.Departments.Where(d => d.OfficeId==officeId).OrderBy(d=> d.Name).Paginate(pageSize,pageNr).ToArray();
        }

        public void AddOffice(Office office)
        {
            _context.Offices.Add(office);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Office GetOfficeById(int officeId)
        {
            return _context.Offices.SingleOrDefault(o => o.Id == officeId);
        }


    }
}
