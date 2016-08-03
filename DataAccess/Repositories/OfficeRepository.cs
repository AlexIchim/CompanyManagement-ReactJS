using Contracts;
using DataAccess.Context;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Department> GetAllDepartmentsOfAnOffice(int officeId)
        {
            var office = _context.Offices.SingleOrDefault(d => d.Id == officeId);

            return office.Departments;

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
