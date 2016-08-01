using Domain.Enums;
using System;
namespace Manager.InfoModels
{
    public class MemberInfo
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime EmploymentDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int TotalAllocation { get; set; }

        public JobType JobType { get; set; }

        public PositionType PositionType { get; set; }
    }
}
