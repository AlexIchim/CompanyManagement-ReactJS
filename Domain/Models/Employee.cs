using Domain.Enums;
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

        public DateTime ReleaseDate { get; set; }

        public int TotalAllocation { get; set; }

        public JobType JobType { get; set; }

        public PositionType PositionType { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public Department ManagedDepartment { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }


    }
}
