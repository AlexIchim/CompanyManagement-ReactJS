using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataAccess.Context;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class OfficeRepository: IOfficeRepository
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
            var array = _context.Offices.SingleOrDefault(d => d.Id == officeId);

            return array.Departments;

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
    }
}
