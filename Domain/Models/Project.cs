using System.Collections.Generic;

namespace Domain.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public int? Duration { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
