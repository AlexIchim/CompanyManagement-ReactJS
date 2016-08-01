using System.Collections.Generic;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly DbContext _context;

        public PositionRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Position> GetAll()
        {
            return _context.Positions.ToArray();
        }

        public Position GetById(int id)
        {
            return _context.Positions.SingleOrDefault(d => d.Id == id);
        }

        public void Add(Position position)
        {
            _context.Positions.Add(position);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}