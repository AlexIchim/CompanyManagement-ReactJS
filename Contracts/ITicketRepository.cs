using System.Collections.Generic; 
using Domain.Models;

namespace Contracts
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        void Add(Ticket ticket);

        Ticket GetById(int Id);

        void Save();
    }
}
