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

        IEnumerable<Department> GetAllDepartmentsOfAnOffice(int officeId);

        void AddOffice(Office office);
    }
}
