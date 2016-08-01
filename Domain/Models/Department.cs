using System.Collections.Generic;

namespace Domain.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Employee DepartmentManager { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
