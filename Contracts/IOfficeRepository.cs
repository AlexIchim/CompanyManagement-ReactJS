using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IOfficeRepository
    {
        IEnumerable<Office> GetAll();
        Office GetById(int id);
        IEnumerable<Department> GetDepartmentsByOfficeId(int officeId);
        void Add(Office department);
        void Save();
    }
}
