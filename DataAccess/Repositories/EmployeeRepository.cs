using Contracts;
using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly DbContext _context;
        public EmployeeRepository(DbContext context)
        {
            _context = context;
        }
        public IEnumerable<EmployeeProject> GetProjectByEmployeeId(int employeeId)
        {
            return _context.EmployeeProjects.Where(e => e.EmployeeId == employeeId).ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void ReleaseEmployee(int employeeId)
        {
            var employees=_context.EmployeeProjects.Where(e => e.EmployeeId == employeeId).ToList();


            if (employees.Count != 0)
            {

                foreach (var ep in employees)
                {
                    _context.EmployeeProjects.Remove(ep);
                }
            }

            var employee = _context.Employees.SingleOrDefault(e => e.Id == employeeId);
            employee.ReleaseDate= DateTime.Now;

            Save();

        }
    }
}
