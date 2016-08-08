using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.InfoModels
{
    public class NotFullyAllocatedEmployeesInfo
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Role { get; set; }

        public int RemainingAllocation { get; set; }
    }
}
