using System.Collections.Generic;
using System.Net.Security;

namespace Domain.Models
{
    public class Department
    {
        public Department()
        {
            this.Employees = new List<Employee>();
            this.Projects = new List<Project>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Project> Projects { get; set; }
        public Employee DepartmentManager { get; set; }
        public Office Office { get; set; }
    }
}
