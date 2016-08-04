using System;

namespace Manager.InputInfoModels
{
    public class AddEmployeeInputInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public DateTime EmploymentDate { get; set; }

        public int EmploymentHours { get; set; }

        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
    }
}