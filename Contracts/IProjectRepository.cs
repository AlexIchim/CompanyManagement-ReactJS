using System.Collections.Generic;
using Domain.Models;
using System;

namespace Contracts
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        IEnumerable<Tuple<Employee, int>> GetEmployeesByProjectId(int id);
        void Delete(Project project);
        void Save();
        void Add(Project project);
    }
}
