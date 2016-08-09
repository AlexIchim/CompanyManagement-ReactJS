using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Manager.InputInfoModels
{
    public class UpdateEmployeeInputInfo
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime EmploymentDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public JobType JobType { get; set; }

        public PositionType PositionType { get; set; }

        public int DepartmentId { get; set; }

    }
}
