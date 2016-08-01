using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetAll();
        Position GetById(int id);
        void Add(Position position);

        void Save();
    }
}