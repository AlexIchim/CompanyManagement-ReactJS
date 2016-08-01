using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using System.Runtime.InteropServices;

namespace Contracts
{
    public interface IProjectRepository
    {
        void Add(Project project);
        void Delete(Project project);
        Boolean editAllocation(int projectId, int employeeId, int newAllocation);
        void Save();
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        IEnumerable<Assignment> GetAllAssignmentsFromProject(int projectId);
        //IEnumerable<Employee> GetAllAvailableEmployes([Optional]string department);
        int GetAllocationOfEmployeeFromProject(int projectId, int employeeId);
        int GetNrTeamMembers(int projectId);

    }
}
