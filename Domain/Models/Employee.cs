using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int EmploymentHours { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public Department ManagedDepartment { get; set; }

        public ICollection<ProjectAllocation> ProjectAllocations { get; set; }
    }
}
