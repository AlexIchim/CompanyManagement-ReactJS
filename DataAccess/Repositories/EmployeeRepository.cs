using Contracts;
using System.Collections.Generic;
using Domain.Models;
using DataAccess.Context;
using System.Linq;
using System;

namespace DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbContext _context;

        public EmployeeRepository(DbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToArray();
        }

        public IEnumerable<Employee> GetAvailable(int? departmentId, int? positionId)
        {
            return _context.Employees.Where(
                e => (
                    (
                        e.ProjectAllocations.Count == 0 ||
                        e.ProjectAllocations.Select(a => a.AllocationPercentage).Sum() < 100
                    ) &&
                    (departmentId == null || e.DepartmentId == departmentId) &&
                    (positionId == null || e.PositionId == positionId)
                )
            ).ToArray();
        }

        public IEnumerable<Employee> GetAllDepartmentManagers()
        {
            return _context.Employees.Where(e => e.Position.Id == 1).ToArray();
        }

        public IEnumerable<Tuple<string, int>> GetAllocationsByEmployeeId(int id)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return new Tuple<string, int>[0];
            }
            else
            {
                var query = employee.ProjectAllocations.Select(a => new Tuple<string, int>(
                    a.Project.Name,
                    a.AllocationPercentage
                ));
                return query.ToArray();
            }
        }

        public Employee GetById(int id)
        {
            return _context.Employees.SingleOrDefault(e => e.Id == id);
        }

        public void Delete(int id)
        {
            _context.Employees.Find(id).ReleaseDate = DateTime.Now;
            _context.ProjectAllocations.RemoveRange(_context.ProjectAllocations.Where(x => x.EmployeeId == id));
            Save();
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

       
    }
}
