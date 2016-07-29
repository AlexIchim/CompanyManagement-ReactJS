using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int EmploymentHours { get; set; }

        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual Department ManagedDepartment { get; set; }

        public virtual ICollection<ProjectAllocation> ProjectAllocations { get; set; }
    }
}
