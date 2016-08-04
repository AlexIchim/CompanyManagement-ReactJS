using Domain.Enums;
using System;

namespace Manager.InputInfoModels
{
    public class AddEmployeeToDepartmentInputInfo
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime EmploymentDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public JobType JobType { get; set; }

        public PositionType PositionType { get; set; }

    }
}
