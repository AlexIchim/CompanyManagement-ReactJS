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

        public Office GetById(int id)
        {
            return _context.Offices.SingleOrDefault(o => o.Id == id);
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
