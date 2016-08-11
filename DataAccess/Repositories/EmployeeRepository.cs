using Contracts;
using System.Collections.Generic;
using Domain.Models;
using DataAccess.Context;
using System.Linq;
using System;
using DataAccess.Extensions;

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

        public IEnumerable<Employee> GetAvailable(int? departmentId, int? positionId, int? projectId, int? pageSize = null, int? pageNumber = null)
        {
            return _context.Employees.Where(
                e => (
                    (
                        e.ProjectAllocations.Count == 0 ||
                        e.ProjectAllocations.Select(a => a.AllocationPercentage).Sum() < 100
                    ) &&
                    (departmentId == null || e.DepartmentId == departmentId) &&
                    (positionId == null || e.PositionId == positionId) &&
                    (e.ProjectAllocations.Count(a => a.ProjectId == projectId) == 0) &&
                    (!e.Position.Name.Equals("Department Manager"))

                )
            )
            .OrderBy(e => e.Name)
            .Paginate(pageSize, pageNumber).ToArray();
        }

        public int GetAvailableEmployeesCount(int? departmentId, int? positionId, int? projectId)
        {
            return _context.Employees.Count(
                e => (
                     (
                        e.ProjectAllocations.Count == 0 ||
                        e.ProjectAllocations.Select(a => a.AllocationPercentage).Sum() < 100
                     ) &&
                     (departmentId == null || e.DepartmentId == departmentId) &&
                     (positionId == null || e.PositionId == positionId) &&
                     (e.ProjectAllocations.Count(a => a.ProjectId == projectId) == 0) &&
                     (!e.Position.Name.Equals("Department Manager"))

                )
            );
        }

        public IEnumerable<Employee> GetAllDepartmentManagers()
        {
            return _context.Employees.Where(e => e.Position.Id == 1).OrderBy(e => e.Name).ToArray();
        }


        public IEnumerable<ProjectAllocation> GetAllocationsByEmployeeId(int id)
        {
            return _context.ProjectAllocations.Where(a => a.EmployeeId == id).ToArray();
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
