using System.Collections.Generic;
using Domain.Models;

namespace Contracts
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        IEnumerable<Project> GetByDepartmentId(int id);

    }
}
