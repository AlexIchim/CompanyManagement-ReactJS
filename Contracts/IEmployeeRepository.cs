using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetAllDepartmentManagers();


        Employee GetById(int id);

        void Save();

    }
}
