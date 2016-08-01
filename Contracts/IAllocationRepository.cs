using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IAllocationRepository
    {
        ProjectAllocation GetById(int id);
        void Add(ProjectAllocation position);
        void Delete(ProjectAllocation allocation);
        void Save();
    }
}