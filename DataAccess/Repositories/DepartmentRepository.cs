using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Contracts;
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

        public IEnumerable<Employee> GetAllMembersOfADepartment(int departmentId)
        {
            var department = GetDepartmentById(departmentId);
            return department.Employees;
        }

        public IEnumerable<Employee> GetMembersOfDepartmentByJobType(int departmentId, string jobType)
        {
            return _context.Employees.Where(d => d.Id == departmentId && d.JobType.ToString().Equals(jobType));
        }

        //public IEnumerable<Employee> FilterMembersOfDepartment(int departmentId, string jobType = "", string position = "")
        //{
            /*if (!jobType.Equals(""))
            {
                if (!position.Equals(""))
                {
                    return
                        _context.Employees.Where(
                            d =>
                                d.Id == departmentId && d.JobType.ToString().Equals(jobType) &&
                                d.Position.ToString().Equals(position));
                }
                else
                {
                    return _context.Employees.Where(
                        d =>
                            d.Id == departmentId && d.JobType.ToString().Equals(jobType));
                }
            }
            else
            {
                
            }*/

            /*IQueryable groupedAssignments = _context.Assignments.GroupBy(id => id.EmployeeId)
                .Select(group => group.Sum(item => item.Allocation));


            var employees = _context.Employees.Join(
                groupedAssignments, e => e.Id, a => a,
                (e,a) => new
                {
                    AllocationSum = 
                })

            return _context.Employees.Where(d => d.Id == departmentId && d.JobType.ToString().Equals(jobType));*/
        //}

        public IEnumerable<Project> GetAllProjectsOfADepartment(int departmentId) {
            var department = GetDepartmentById(departmentId);
            return department.Projects;
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
