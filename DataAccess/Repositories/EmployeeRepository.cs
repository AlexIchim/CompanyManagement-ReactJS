using Contracts;
using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
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
    }
}
