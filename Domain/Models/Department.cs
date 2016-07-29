using System.Collections.Generic;
using System.Net.Security;

namespace Domain.Models
{
    public class Department
    {
        Department()
        {
            this.Employees = new List<Employee>();
            this.Projects = new List<Project>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Project> Projects { get; set; }
        public Employee DepartmentManager { get; set; }
        public Office Office { get; set; }
    }
}
