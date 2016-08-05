using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.InfoModels
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public DateTime EmploymentDate { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public int EmploymentHours { get; set; }

        public int PositionId { get; set; }

        // custom:
        public string PositionName { get; set; }
        public int TotalAllocation { get; set; }
    }
}
