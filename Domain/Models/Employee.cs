using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Employee
    {
        public Employee()
        {

        }
        public Employee(int id, string name, string address, DateTime employmentDate, DateTime releaseDate, int totalAllocation, JobType jobType, PositionType positionType, int departmentId)
        {
            Id = id;
            Name = name;
            Address = address;
            EmploymentDate = employmentDate;
            ReleaseDate = releaseDate;
            TotalAllocation = totalAllocation;
            JobType = jobType;
            PositionType = positionType;
            DepartmentId = departmentId;

        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime EmploymentDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int TotalAllocation { get; set; }

        public JobType JobType { get; set; }

        public PositionType PositionType { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Department ManagedDepartment { get; set; }

        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }


    }
}
