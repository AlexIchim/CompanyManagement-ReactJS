using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Contracts;
using DataAccess.Context;
using Domain.Models;
using System;
using DataAccess.Extensions;

namespace DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DbContext _context;

        public DepartmentRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToArray();
        }

        public Department GetById(int Id)
        {
            return _context.Departments.SingleOrDefault(d => d.Id == Id);
        }
        public Department GetByName(string name, int officeId)
        {
            return _context.Departments.SingleOrDefault(d => d.Name == name && d.OfficeId == officeId);
        }

        public IEnumerable<Project> GetProjectsByDepartmentId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "", string statusFilter = "")
        {
            var dept = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return new Project[0];
            }
            else
            {
                string nameString = searchString.ToLower();

                return dept.Projects.Where(
                    p => p.Name.ToLower().Contains(nameString) &&
                         (statusFilter == "" || p.Status == statusFilter)
                ).Paginate(pageSize, pageNumber).ToArray();
            }
        }

        public int GetProjectCountByDepartmentId(int id, string searchString = "", string statusFilter = "")
        {
            Department dept = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return 0;
            }

            string nameString = searchString.ToLower();

            return dept.Projects.Count(
                p => p.Name.ToLower().Contains(nameString) &&
                     (statusFilter == "" || p.Status == statusFilter)
            );
        }

        public IEnumerable<Employee> GetEmployeesByDepartmentId(int id, int? pageSize = null, int? pageNumber = null, string searchString = "",
                                                                int? positionIdFilter = null, int? employmentFilter = null,
                                                                int? allocationFromFilter = null, int? allocationToFilter = null)
        {
            var dept = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return new Employee[0];
            }
            else
            {
                return dept.Employees.Where(
                    e => e.Name.ToLower().Contains(searchString.ToLower()) &&
                         (positionIdFilter == null || e.PositionId == positionIdFilter) &&
                         (employmentFilter == null || e.EmploymentHours == employmentFilter.Value) &&
                         (
                            allocationFromFilter == null ||
                            e.ProjectAllocations.Select(a => a.AllocationPercentage).Sum() >= allocationFromFilter.Value
                         ) &&
                         (
                            allocationToFilter == null ||
                            e.ProjectAllocations.Select(a => a.AllocationPercentage).Sum() <= allocationToFilter.Value
                         )
                ).Paginate(pageSize, pageNumber).ToArray();
            }
        }

        public int GetEmployeeCountByDepartmentId(int id, string searchString = "",
                                                  int? positionIdFilter = null, int? employmentFilter = null,
                                                  int? allocationFromFilter = null, int? allocationToFilter = null)
        {
            var dept = _context.Departments.SingleOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return 0;
            }
            else
            {
                return dept.Employees.Count(
                    e => e.Name.ToLower().Contains(searchString.ToLower()) &&
                         (positionIdFilter == null || e.PositionId == positionIdFilter.Value) &&
                         (employmentFilter == null || e.EmploymentHours == employmentFilter.Value) &&
                         (
                            allocationFromFilter == null ||
                            e.ProjectAllocations.Select(a => a.AllocationPercentage).Sum() >= allocationFromFilter.Value
                         ) &&
                         (
                            allocationToFilter == null ||
                            e.ProjectAllocations.Select(a => a.AllocationPercentage).Sum() <= allocationToFilter.Value
                         )
                );
            }
        }

        public void Add(Department department, int? departmentManagerId)
        {
            if (departmentManagerId != null)
            {
                Employee departmentManager = _context.Employees.First(e => e.Id == departmentManagerId);
                department.DepartmentManager = departmentManager;
            }
            else
            {
                department.DepartmentManagerId = null;
            }
            _context.Departments.Add(department);
            Save();
        }
        public void Update(Department department, int? departmentManagerId)
        {
            if (departmentManagerId != null)
            {
                Employee departmentManager = _context.Employees.First(e => e.Id == departmentManagerId);
                department.DepartmentManager = departmentManager;
                _context.Departments.First(d => d.Id == department.Id).DepartmentManager = departmentManager;
            }
            else
            {
                department.DepartmentManagerId = null;
            }
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
