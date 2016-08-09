using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataAccess.Context;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public readonly DbContext _DbContext;

        public TicketRepository(DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _DbContext.Tickets.ToArray();
        }

        public void Add(Ticket ticket)
        {
            _DbContext.Tickets.Add(ticket);
            Save();
        }

        public void Update(Ticket ticket)
        {
                
        }

        public Ticket GetById(int Id)
        {
            return _DbContext.Tickets.SingleOrDefault(x => x.Id == Id);
        }


        public void Save()
        {
            _DbContext.SaveChanges();
        }




    }
}
