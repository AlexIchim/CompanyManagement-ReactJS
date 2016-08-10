using System.Collections.Generic;
using Domain.Models;
using System;

namespace Contracts
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        IEnumerable<ProjectAllocation> GetEmployeesByProjectId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "", int? positionIdFilter = null);
        int GetProjectMembersCount(int id, string searchString = "", int? positionIdFilter = null);
        void Delete(Project project);
        void Save();
        void Add(Project project);
    }
}
