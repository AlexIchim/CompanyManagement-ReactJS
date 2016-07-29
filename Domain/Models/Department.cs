using System.Collections.Generic;

namespace Domain.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Employee DepartmentManager { get; set; }

        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
