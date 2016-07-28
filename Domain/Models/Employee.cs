using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

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

        //public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }


    }
}
