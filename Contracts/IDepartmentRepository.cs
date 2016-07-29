using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        IEnumerable<Department> GetByOfficeId(int officeId);
        void Add(Department department);
        void Save();
    }
}
