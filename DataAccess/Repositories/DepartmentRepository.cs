using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Contracts;
using DataAccess.Extensions;
using Domain.Models;
using DbContext = DataAccess.Context.DbContext;

namespace DataAccess.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly DbContext _context;

        public DepartmentRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToArray();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return _context.Departments.SingleOrDefault(d=>d.Id == departmentId);
        }

        public IEnumerable<Employee> GetMembersOfDepartment(int departmentId, int pageSize, int pageNumber, string name = "", int? jobType = null, int? position = null, int? allocation = null)
        {
            var department = GetDepartmentById(departmentId);
            var employees = department.Employees;

            return employees.OrderBy(x => x.Id);
            /*return _context.Employees.
                Join(_context.Assignments,
                    employee => employee.Id, assignment => assignment.EmployeeId,
                    (employee, assignment) => new
                    {
                        employee,
                        assignment
                    }
                )
                .GroupBy(x => x.employee)
                .Select(g => new
                {
                    Employee = g.Key,
                    Allocation = g.Sum(x => x.assignment.Allocation)
                })
                .Where(
                    x =>
                        ((x.Employee.Department.Id == departmentId) &&
                         ((!name.Equals("") && x.Employee.Name.Contains(name)) || name.Equals("")) &&
                         ((jobType.HasValue && (int) x.Employee.JobType == jobType) || !jobType.HasValue) &&
                         ((position.HasValue && (int) x.Employee.Position == position) || !position.HasValue) &&
                         ((allocation.HasValue && (int) x.Allocation == allocation) || !allocation.HasValue)))
                .OrderBy(x => x.Employee.Id)
                .Paginate(pageSize, pageNumber)
                .ToArray().Select(x => x.Employee);*/
        }

        public IEnumerable<Project> GetProjectsOfDepartment(int departmentId, int pageSize, int pageNumber, int? status = null) {
            return
                _context.Projects.Where(
                    p =>
                        (p.Department.Id == departmentId) &&
                        ((status.HasValue && (int) p.Status == status) || !status.HasValue))
                    .OrderBy(p => p.Id)
                    .Paginate(pageSize, pageNumber)
                    .ToArray();
        }

        public IEnumerable<Project> FilterProjectsOfADepartmentByStatus(int departmentId, string status)
        {
            return _context.Projects.Where(p => p.Department.Id == departmentId && p.Status.ToString() == status).ToArray();
        }
        public void AddDepartment(Department department)
        {
            _context.Departments.Add(department);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
