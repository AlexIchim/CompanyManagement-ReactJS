using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Contracts
{
    public interface IOfficeRepository
    {
        IEnumerable<Office> GetAll();
        Office GetById(int id);
        IEnumerable<Department> GetAllDepartmentsOfAnOffice(int officeId, int pageSize, int pageNumber);
        IEnumerable<Employee> GetAllAvailableEmployeesOfAnOffice(int officeId, int pageSize, int pageNumber,
            int? department = null, int? position = null);
        void Add(Office office);
        void Save();
    }
}
