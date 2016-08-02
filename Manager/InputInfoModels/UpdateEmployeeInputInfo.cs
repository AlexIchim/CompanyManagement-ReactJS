using System;

namespace Manager.InputInfoModels
{
    public class UpdateEmployeeInputInfo
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int PositionId { get; set; }
        public int DepartmentId { get; set; }

        public DateTime EmploymentDate { get; set; }

        public int EmploymentHours { get; set; }

        // custom:
        public int TotalAllocation { get; set; }
    }
}