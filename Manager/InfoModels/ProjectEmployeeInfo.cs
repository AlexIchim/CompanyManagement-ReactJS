using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.InfoModels
{
    public class ProjectEmployeeInfo
    {
        public int AllocationId { get; set; }
        public EmployeeInfo Employee { get; set; }
        public int Allocation { get; set; }
    }
}
