using Domain.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IOfficeRepository
    {
        IEnumerable<Office> GetAll();

        IEnumerable<Department> GetAllDepartmentsOfAnOffice(int officeId, int? pageSize, int? pageNr);

        void AddOffice(Office office);

        void Save();

        Office GetOfficeById(int officeId);


      


    }
}
